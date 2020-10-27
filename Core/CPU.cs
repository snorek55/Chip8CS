using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace Core
{
	//Info from https://austinmorlan.com/posts/chip8_emulator/
	public class CPU
	{
		private const ushort StartAddress = 0x200;
		private const int VideoWidth = 64;
		private const int VideoHeight = 32;
		private readonly Memory memory;
		private readonly Stack16Levels stack;
		private readonly Dictionary<ushort, ExecuteDel> generalFunctions = new Dictionary<ushort, ExecuteDel>();
		private readonly Dictionary<ushort, ExecuteDel> functions0 = new Dictionary<ushort, ExecuteDel>();
		private readonly Dictionary<ushort, ExecuteDel> functions8 = new Dictionary<ushort, ExecuteDel>();
		private readonly Dictionary<ushort, ExecuteDel> functionsF = new Dictionary<ushort, ExecuteDel>();

		private delegate void ExecuteDel();

		internal ushort IndexRegister { get; private set; }

		internal byte[] VRegisters { get; private set; } = new byte[16];
		internal ushort Pc { get; private set; }
		internal ushort Opcode { get; private set; }
		internal bool[,] VideoPixels { get; private set; } = new bool[VideoWidth, VideoHeight];
		internal bool[] KeyState = new bool[0xF];
		internal byte delayTimer { get; set; }
		internal byte soundTimer { get; set; }

		public CPU(Memory memory, Stack16Levels stack)
		{
			this.memory = memory;
			this.stack = stack;
			InitializeFunctions();
			Initialize();
		}

		private void InitializeFunctions()
		{
			generalFunctions.Add(0x0, new ExecuteDel(Op_0nnn));
			generalFunctions.Add(0x1, new ExecuteDel(Op_1nnn));
			generalFunctions.Add(0x2, new ExecuteDel(Op_2nnn));
			generalFunctions.Add(0x3, new ExecuteDel(Op_3xkk));
			generalFunctions.Add(0x4, new ExecuteDel(Op_4xkk));
			generalFunctions.Add(0x5, new ExecuteDel(Op_5xy0));
			generalFunctions.Add(0x6, new ExecuteDel(Op_6xkk));
			generalFunctions.Add(0x7, new ExecuteDel(Op_7xkk));
			generalFunctions.Add(0x8, new ExecuteDel(Op_8xyn));
			generalFunctions.Add(0x9, new ExecuteDel(Op_9xy0));
			generalFunctions.Add(0xA, new ExecuteDel(Op_Annn));
			generalFunctions.Add(0xB, new ExecuteDel(Op_Bnnn));
			generalFunctions.Add(0xC, new ExecuteDel(Op_Cxkk));
			generalFunctions.Add(0xD, new ExecuteDel(Op_Dxyn));
			generalFunctions.Add(0xE, new ExecuteDel(Op_Exyn));
			generalFunctions.Add(0xF, new ExecuteDel(Op_Fxyn));

			functions0.Add(0x0, new ExecuteDel(Op_00E0));
			functions0.Add(0xE, new ExecuteDel(Op_00EE));

			functions8.Add(0x0, new ExecuteDel(Op_8xy0));
			functions8.Add(0x1, new ExecuteDel(Op_8xy1));
			functions8.Add(0x2, new ExecuteDel(Op_8xy2));
			functions8.Add(0x3, new ExecuteDel(Op_8xy3));
			functions8.Add(0x4, new ExecuteDel(Op_8xy4));
			functions8.Add(0x5, new ExecuteDel(Op_8xy5));
			functions8.Add(0x6, new ExecuteDel(Op_8xy6));
			functions8.Add(0x7, new ExecuteDel(Op_8xy7));
			functions8.Add(0xE, new ExecuteDel(Op_8xyE));

			functionsF.Add(0x7, new ExecuteDel(Op_Fx07));
			functionsF.Add(0xA, new ExecuteDel(Op_Fx0A));
			functionsF.Add(0x15, new ExecuteDel(Op_Fx15));
			functionsF.Add(0x18, new ExecuteDel(Op_Fx18));
			functionsF.Add(0x1E, new ExecuteDel(Op_Fx1E));
			functionsF.Add(0x29, new ExecuteDel(Op_Fx29));
		}

		public void Initialize()
		{
			memory.Initialize();
			stack.Clear();
			VRegisters = new byte[16];
			VideoPixels = new bool[64, 32];
			IndexRegister = 0;
			Pc = StartAddress;
			Opcode = 0;
			delayTimer = 0;
			soundTimer = 0;
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

		private ExecuteDel Decode()
		{
			var generalOpcode = Convert.ToUInt16((Opcode & 0xF000u) >> 12);
			if (generalFunctions.ContainsKey(generalOpcode))
				return generalFunctions[generalOpcode];
			else
				throw new ArgumentException($"Such function bot found {Opcode:X}");
		}

		private void Execute(ExecuteDel function)
		{
			function.Invoke();
		}

		#region Functions

		private void Op_0nnn()
		{
			var specialCode = Convert.ToUInt16(Opcode & 0x000Fu);
			if (functions0.ContainsKey(specialCode))
				functions0[specialCode].Invoke();
			else
				throw new ArgumentException($"Such function8 not found {Opcode:X}");
		}

		private void Op_8xyn()
		{
			var specialCode = Convert.ToUInt16(Opcode & 0x000Fu);
			if (functions8.ContainsKey(specialCode))
				functions8[specialCode].Invoke();
			else
				throw new ArgumentException($"Such function8 not found {Opcode:X}");
		}

		private void Op_Exyn()
		{
			var lsb = Convert.ToByte(Opcode & 0xFF);

			if (lsb == 0xE9)
				Op_Ex9E();
			else if (lsb == 0xA1)
				Op_ExA1();
			else
				throw new ArgumentException($"Such function8 not found {Opcode:X}");
		}

		private void Op_Fxyn()
		{
			var specialCode = Convert.ToUInt16(Opcode & 0x00FFu);
			if (functionsF.ContainsKey(specialCode))
				functionsF[specialCode].Invoke();
			else
				throw new ArgumentException($"Such function8 not found {Opcode:X}");
		}

		/// <summary>
		/// CLS. Clear display
		/// </summary>
		private void Op_00E0()
		{
			VideoPixels = new bool[64, 32];
		}

		/// <summary>
		/// RET. Return from a subroutine
		/// </summary>
		private void Op_00EE()
		{
			Pc = stack.Pop();
		}

		/// <summary>
		/// JMP. Jump to location nnn
		/// </summary>
		private void Op_1nnn()
		{
			var address = Convert.ToUInt16(Opcode & 0x0FFFu);

			Pc = address;
		}

		/// <summary>
		/// CALL. Call subroutine at nnn
		/// </summary>
		private void Op_2nnn()
		{
			var address = Convert.ToUInt16(Opcode & 0x0FFFu);

			stack.Push(Pc);
			stack.Skip();
			Pc = address;
		}

		/// <summary>
		/// SE Vx, byte. Skip next instruction if Vx == kk
		/// </summary>
		private void Op_3xkk()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte kk = Convert.ToByte(Opcode & 0x00FFu);

			if (VRegisters[Vx] == kk)
				Pc += 2;
		}

		/// <summary>
		/// SNE Vx, byte. Skip next instruction if Vx != kk
		/// </summary>
		private void Op_4xkk()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte kk = Convert.ToByte(Opcode & 0x00FFu);

			if (VRegisters[Vx] != kk)
				Pc += 2;
		}

		/// <summary>
		/// SE Vx, Vy. Skip next instruction if Vx = Vy
		/// </summary>
		private void Op_5xy0()
		{
			byte firstByte = Convert.ToByte(Opcode & 0x000Fu);
			if (firstByte != 0)
				throw new ArgumentException($"Such function not found: {Opcode}");

			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			if (VRegisters[Vx] == VRegisters[Vy])
				Pc += 2;
		}

		/// <summary>
		/// LD Vx, byte. Set Vx = kk
		/// </summary>
		private void Op_6xkk()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte kk = Convert.ToByte(Opcode & 0x00FFu);

			VRegisters[Vx] = kk;
		}

		/// <summary>
		/// ADD Vx, byte. Set Vx += kk.
		/// </summary>
		private void Op_7xkk()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte kk = Convert.ToByte(Opcode & 0x00FFu);

			VRegisters[Vx] += kk;
		}

		/// <summary>
		/// LD Vx, Vy. Set Vx = Vy
		/// </summary>
		private void Op_8xy0()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			VRegisters[Vx] = VRegisters[Vy];
		}

		/// <summary>
		/// OR Vx, Vy. Set Vx = Vx OR Vy.
		/// </summary>
		private void Op_8xy1()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			VRegisters[Vx] |= VRegisters[Vy];
		}

		/// <summary>
		/// AND Vx, Vy. Set Vx = Vx AND Vy.
		/// </summary>
		private void Op_8xy2()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			VRegisters[Vx] &= VRegisters[Vy];
		}

		/// <summary>
		/// XOR Vx, Vy. Set Vx = Vx XOR Vy.
		/// </summary>
		private void Op_8xy3()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			VRegisters[Vx] ^= VRegisters[Vy];
		}

		/// <summary>
		/// ADD Vx, Vy. Set Vx += Vy, Vf=Carry.
		/// </summary>
		private void Op_8xy4()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			ushort sum = Convert.ToUInt16(VRegisters[Vx] + VRegisters[Vy]);
			if (sum > 255)
				VRegisters[0xF] = 1;
			else
				VRegisters[0xF] = 0;

			VRegisters[Vx] = Convert.ToByte(sum & 0xFF);
		}

		/// <summary>
		/// SUB Vx, Vy. Set Vx = Vx - Vy, set VF = NOT borrow.
		/// </summary>
		private void Op_8xy5()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			if (VRegisters[Vx] > VRegisters[Vy])
				VRegisters[0xF] = 1;
			else
				VRegisters[0xF] = 0;

			VRegisters[Vx] -= VRegisters[Vy];
		}

		/// <summary>
		/// SHR Vx, Vy. Set Vx = Vx SHR 1.
		/// </summary>
		private void Op_8xy6()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00) >> 8);

			// Save LSB in VF
			VRegisters[0xF] = Convert.ToByte(VRegisters[Vx] & 0x1);

			VRegisters[Vx] >>= 1;
		}

		/// <summary>
		/// SUBN Vx, Vy. Set Vx = Vy - Vx, set VF = NOT borrow.
		/// </summary>
		private void Op_8xy7()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			if (VRegisters[Vy] > VRegisters[Vx])
				VRegisters[0xF] = 1;
			else
				VRegisters[0xF] = 0;

			VRegisters[Vx] = (byte)(VRegisters[Vy] - VRegisters[Vx]);
		}

		/// <summary>
		/// SHL Vx, Vy. Set Vx = Vx SHL 1.
		/// </summary>
		private void Op_8xyE()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00) >> 8);

			// Save MSB in VF
			VRegisters[0xF] = Convert.ToByte((VRegisters[Vx] & 0x80) >> 7);

			VRegisters[Vx] <<= 1;
		}

		/// <summary>
		/// SNE Vx, Vy. Skip next instruction if Vx != Vy
		/// </summary>
		private void Op_9xy0()
		{
			byte firstByte = Convert.ToByte(Opcode & 0x000Fu);
			if (firstByte != 0)
				throw new ArgumentException($"Such function not found: {Opcode}");

			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

			if (VRegisters[Vx] != VRegisters[Vy])
				Pc += 2;
		}

		/// <summary>
		/// LD I,addr. Set I = nnn
		/// </summary>
		private void Op_Annn()
		{
			var address = Convert.ToUInt16(Opcode & 0x0FFFu);

			IndexRegister = address;
		}

		/// <summary>
		/// JP V0, addr. Jump to location nnn + V0.
		/// </summary>
		private void Op_Bnnn()
		{
			var address = Convert.ToUInt16(Opcode & 0x0FFFu);

			Pc = Convert.ToUInt16(VRegisters[0] + address);
		}

		/// <summary>
		/// RND Vx, byte. Set Vx = random byte AND kk
		/// </summary>
		private void Op_Cxkk()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00) >> 8);
			byte kk = Convert.ToByte(Opcode & 0x00FF);

			VRegisters[Vx] = Convert.ToByte(new Random().Next(0, 0xFF) & kk);
		}

		/// <summary>
		/// DRW Vx, Vy, nibble. Display n-byte sprite starting at memory location I at (Vx, Vy), set VF = collision.
		/// </summary>
		private void Op_Dxyn()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// SKP Vx. Skip next instruction if key with the value of Vx is pressed.
		/// </summary>
		private void Op_Ex9E()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

			byte key = VRegisters[Vx];

			if (KeyState[key])
				Pc += 2;
		}

		/// <summary>
		///  SKNP Vx. Skip next instruction if key with the value of Vx is not pressed.
		/// </summary>
		private void Op_ExA1()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

			byte key = VRegisters[Vx];

			if (!KeyState[key])
				Pc += 2;
		}

		/// <summary>
		/// LD Vx, DT. Set Vx = delay timer value
		/// </summary>
		private void Op_Fx07()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

			VRegisters[Vx] = delayTimer;
		}

		/// <summary>
		/// LD Vx, K. Wait for a key press, store the value of the key in Vx.
		/// </summary>
		private void Op_Fx0A()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

			for (int i = 0; i < KeyState.Length; i++)
			{
				if (KeyState[i])
				{
					VRegisters[Vx] = Convert.ToByte(i);
					return;
				}
			}
			//Else wait
			Pc -= 2;
		}

		/// <summary>
		/// LD DT, Vx. Set delay timer = Vx
		/// </summary>
		private void Op_Fx15()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

			delayTimer = VRegisters[Vx];
		}

		/// <summary>
		/// LD ST, Vx. Set sound timer = Vx
		/// </summary>
		private void Op_Fx18()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

			soundTimer = VRegisters[Vx];
		}

		/// <summary>
		/// ADD I, Vx. Set I = I + Vx.
		/// </summary>
		private void Op_Fx1E()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

			IndexRegister += VRegisters[Vx];
		}

		/// <summary>
		/// LD F, Vx. Set I = location of sprite for digit Vx.
		/// </summary>
		private void Op_Fx29()
		{
			byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
			byte digit = VRegisters[Vx];

			IndexRegister = Convert.ToUInt16(Memory.FonsetStartAddress + (Memory.LetterSize * digit));
		}

		#endregion Functions
	}
}