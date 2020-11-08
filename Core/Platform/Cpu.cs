using Core.Opcodes;
using Core.Platform;

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace Core
{
	//Info from https://austinmorlan.com/posts/chip8_emulator/
	internal class Cpu
	{
		internal const int VideoWidth = 65;
		internal const int VideoHeight = 33;
		internal const byte SpriteColumns = 8;
		private const ushort StartAddress = 0x200;

		internal ushort IndexRegister { get; set; }
		internal Memory Memory { get; private set; }
		internal Stack16Levels Stack { get; private set; }

		internal byte[] VRegisters { get; private set; }
		internal ushort Pc { get; set; }
		internal BaseOp Opcode { get; private set; }
		internal bool[,] VideoPixels { get; set; } = new bool[VideoWidth, VideoHeight];
		internal bool[] KeyState = new bool[16];

		internal byte DelayTimer
		{
			get => Convert.ToByte(delayTimer.Ticks);
			set => delayTimer.Ticks = value;
		}

		internal byte SoundTimer
		{
			get => Convert.ToByte(soundTimer.Ticks);
			set => soundTimer.Ticks = value;
		}

		internal bool DrawingRequired { get; set; }
		private readonly Timer60Hz delayTimer = new Timer60Hz();
		private readonly Timer60Hz soundTimer = new Timer60Hz();

		private readonly OpcodeDecoder decoder = new OpcodeDecoder();

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