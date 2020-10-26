using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace Core
{
	public class CPU
	{
		private const ushort StartAddress = 0x200;
		private const int VideoWidth = 64;
		private const int VideoHeight = 32;
		private readonly Memory memory;
		private readonly Stack16Levels stack;
		private readonly Dictionary<ushort, Delegate> generalFunctions = new Dictionary<ushort, Delegate>();
		private readonly Dictionary<ushort, Delegate> functions0 = new Dictionary<ushort, Delegate>();

		private delegate void execute();

		private ushort indexRegister;

		internal byte[] VRegisters { get; private set; } = new byte[16];
		internal ushort Pc { get; private set; }
		internal ushort Opcode { get; private set; }
		internal bool[,] VideoPixels { get; private set; } = new bool[VideoWidth, VideoHeight];

		public CPU(Memory memory, Stack16Levels stack)
		{
			this.memory = memory;
			this.stack = stack;
			InitializeFunctions();
			Initialize();
		}

		private void InitializeFunctions()
		{
			generalFunctions.Add(0x0, new execute(Op_0nnn));
			generalFunctions.Add(0x1, new execute(Op_1nnn));
			generalFunctions.Add(0x2, new execute(Op_2nnn));
			generalFunctions.Add(0x3, new execute(Op_3xkk));

			functions0.Add(0x0, new execute(Op_00E0));
			functions0.Add(0xE, new execute(Op_00EE));
		}

		public void Initialize()
		{
			memory.Initialize();
			stack.Clear();
			VRegisters = new byte[16];
			VideoPixels = new bool[64, 32];
			indexRegister = 0;
			Pc = StartAddress;
			Opcode = 0;
		}

		public void Cycle()
		{
			Fetch();

			var function = Decode();
			Execute(function);
		}

		private void Fetch()
		{
			var msb = memory.GetByte(Pc);
			Pc++;
			var lsb = memory.GetByte(Pc);
			Pc++;
			uint op = msb;
			op = op << 8;
			op = op | lsb;
			Opcode = Convert.ToUInt16(op);
		}

		private Delegate Decode()
		{
			var generalOpcode = Convert.ToUInt16((Opcode & 0xF000u) >> 12);
			if (generalFunctions.ContainsKey(generalOpcode))
				return generalFunctions[generalOpcode];
			else
				throw new ArgumentException($"Not found such function {Opcode:X}");
		}

		private void Execute(Delegate function)
		{
			function.DynamicInvoke();
		}

		#region Functions

		private void Op_0nnn()
		{
			var specialCode = Convert.ToUInt16(Opcode & 0x000Fu);
			if (functions0.ContainsKey(specialCode))
				functions0[specialCode].DynamicInvoke();
			else
				throw new ArgumentException($"Not found such function0 {Opcode:X}");
		}

		private void Op_00E0()
		{
			VideoPixels = new bool[64, 32];
		}

		private void Op_00EE()
		{
			Pc = stack.Pop();
		}

		private void Op_1nnn()
		{
			var address = Convert.ToUInt16(Opcode & 0x0FFFu);

			Pc = address;
		}

		private void Op_2nnn()
		{
			var address = Convert.ToUInt16(Opcode & 0x0FFFu);

			stack.Push(Pc);
			stack.Skip();
			Pc = address;
		}

		private void Op_3xkk()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte kk = Convert.ToByte(Opcode & 0x00FFu);

			if (VRegisters[Vx] == kk)
			{
				Pc += 2;
			}
		}

		#endregion Functions
	}
}