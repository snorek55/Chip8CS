using Core.Opcodes;

using System;
using System.Collections.Generic;
using System.IO;

namespace Core
{
	public class Disassembler
	{
		public IList<BaseOp> Opcodes { get => mem.GameOps; }
		public DissasemblerInfo Info { get; internal set; } = new DissasemblerInfo();

		internal readonly Cpu cpu;
		internal readonly Memory mem;
		internal readonly Stack16Levels stack;
		public Disassembler()
		{
			mem = new Memory(new OpcodeDecoder());
			stack = new Stack16Levels();
			cpu = new Cpu(mem, stack);
		}

		public void LoadRom(string path)
		{
			if (!File.Exists(path))
				throw new InvalidOperationException("Path does not exist");

			var gameBytes = File.ReadAllBytes(path);
			mem.LoadGame(gameBytes);
		}
		public void Cycle()
		{
			cpu.Cycle();
			UpdateInfo();
		}

		private void UpdateInfo()
		{
			Info.IndexRegister = cpu.IndexRegister;
			Info.Opcode = cpu.Opcode;
			Info.Pc = cpu.Pc;
			Info.StackLevels = cpu.Stack.Levels;
			Info.VRegisters = cpu.VRegisters;
			Info.VideoPixels = cpu.VideoPixels;
		}
	}
}