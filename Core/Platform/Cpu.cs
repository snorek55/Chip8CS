using Core.Opcodes;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace Core
{
	//Info from https://austinmorlan.com/posts/chip8_emulator/
	internal class Cpu
	{
		private const ushort StartAddress = 0x200;
		internal const int VideoWidth = 64;
		internal const int VideoHeight = 32;
		internal const byte SpriteColumns = 8;
		internal Memory Memory { get; private set; }

		internal Stack16Levels Stack { get; private set; }
		public ushort IndexRegister { get; internal set; }

		public byte[] VRegisters { get; private set; }
		public ushort Pc { get; internal set; }
		public BaseOp Opcode { get; private set; }
		public bool[,] VideoPixels { get; internal set; } = new bool[VideoWidth, VideoHeight];
		internal bool[] KeyState = new bool[16];
		public byte DelayTimer { get; internal set; }
		public byte SoundTimer { get; internal set; }
		internal bool DrawingRequired { get; set; }

		private OpcodeDecoder decoder = new OpcodeDecoder();

		internal Cpu(Memory memory, Stack16Levels stack)
		{
			Memory = memory;
			Stack = stack;
			Initialize();
		}

		public void Initialize()
		{
			Stack.Clear();
			VRegisters = new byte[16];
			VideoPixels = new bool[VideoWidth, VideoHeight];
			IndexRegister = 0;
			Pc = StartAddress;
			Opcode = null;
			DelayTimer = 0;
			SoundTimer = 0;
			DrawingRequired = false;
		}

		public void Cycle()
		{
			DrawingRequired = false;

			Fetch();
			Execute();

			if (DelayTimer > 0)
				DelayTimer--;

			if (SoundTimer > 0)
				SoundTimer--;
		}

		private void Fetch()
		{
			var pos = Pc;
			var msb = Memory.GetByte(Pc);
			Pc++;
			var lsb = Memory.GetByte(Pc);
			Opcode = decoder.DecodeOp(msb, lsb);
			Pc++;
			Opcode.Pos = pos;
		}

		private void Execute()
		{
			Opcode.Execute(this);
		}
	}
}