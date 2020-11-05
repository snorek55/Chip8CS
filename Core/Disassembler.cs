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

		private int scaleFactor = 1;
		public int ScaleFactor { get => scaleFactor; set { scaleFactor = value; UpdateInfo(true); } }

		public int CurrentHeight { get => Cpu.VideoHeight * scaleFactor; }
		public int CurrentWidth { get => Cpu.VideoWidth * scaleFactor; }

		internal readonly Cpu cpu;
		internal readonly Memory mem;
		internal readonly Stack16Levels stack;

		public Disassembler()
		{
			mem = new Memory(new OpcodeDecoder());
			stack = new Stack16Levels();
			cpu = new Cpu(mem, stack);
			Info.InitializeVideoPixels(Cpu.VideoWidth * ScaleFactor, Cpu.VideoHeight * ScaleFactor);
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
			UpdateInfo(cpu.DrawingRequired);
		}

		public void OnKeyChanged(int keyNum, bool isDown)
		{
			cpu.KeyState[keyNum] = isDown;
		}

		internal void UpdateInfo(bool drawingRequired)
		{
			Info.IndexRegister = cpu.IndexRegister;
			Info.Opcode = cpu.Opcode;
			Info.Pc = cpu.Pc;
			Info.StackLevels = cpu.Stack.Levels;
			Info.VRegisters = cpu.VRegisters;
			Info.DrawingRequired = cpu.DrawingRequired;

			if (drawingRequired)
			{
				Info.VideoPixels = ReplicateVideoPixels(cpu.VideoPixels);
			}
		}

		/// <summary>
		/// Grows an image using replication method.
		/// </summary>
		/// <param name="originalPixels"></param>
		/// <returns></returns>
		private bool[][] ReplicateVideoPixels(bool[,] originalPixels)
		{
			//Based on: https://ideone.com/rTctxV
			var newWidth = Cpu.VideoWidth * ScaleFactor;
			var newHeight = Cpu.VideoHeight * ScaleFactor;

			var amplifiedPixels = new bool[newWidth][];
			for (int i = 0; i < newWidth; i++)
			{
				amplifiedPixels[i] = new bool[newHeight];
			}

			for (int i = 0; i < newWidth; i++)
			{
				var iUnscaled = i / ScaleFactor;
				for (int j = 0; j < newHeight; j++)
				{
					var jUnscaled = j / ScaleFactor;
					amplifiedPixels[i][j] = originalPixels[iUnscaled, jUnscaled];
				}
			}
			return amplifiedPixels;
		}
	}
}