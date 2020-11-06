using Core.Opcodes;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Core
{
	public class Disassembler
	{
		public IList<BaseOp> Opcodes { get => mem.GameOps; }
		public DissasemblerInfo Info { get; internal set; }
		public int Height { get => Cpu.VideoHeight; }
		public int Width { get => Cpu.VideoWidth; }

		private Bitmap OriginalBitmap;
		internal readonly Cpu cpu;
		internal readonly Memory mem;
		internal readonly Stack16Levels stack;

		public Disassembler()
		{
			mem = new Memory(new OpcodeDecoder());
			stack = new Stack16Levels();
			cpu = new Cpu(mem, stack);
			Info = new DissasemblerInfo(Width, Height);
			OriginalBitmap = new Bitmap(Width, Height);
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

		public void OnKeyChanged(int keyNum, bool isDown)
		{
			cpu.KeyState[keyNum] = isDown;
		}

		internal void UpdateInfo()
		{
			Info.IndexRegister = cpu.IndexRegister;
			Info.Opcode = cpu.Opcode;
			Info.Pc = cpu.Pc;
			Info.StackLevels = cpu.Stack.Levels;
			Info.VRegisters = cpu.VRegisters;
			Info.DrawingRequired = cpu.DrawingRequired;

			if (cpu.DrawingRequired)
				UpdateVideoBitmap(cpu.VideoPixels);
		}

		private void UpdateVideoBitmap(bool[,] originalPixels)
		{
			OriginalBitmap = new Bitmap(Width, Height);
			//Based on: https://ideone.com/rTctxV
			for (int i = 0; i < OriginalBitmap.Width; i++)
			{
				for (int j = 0; j < OriginalBitmap.Height; j++)
				{
					if (originalPixels[i, j])
						OriginalBitmap.SetPixel(i, j, Color.White);
					else
						OriginalBitmap.SetPixel(i, j, Color.Black);
				}
			}

			var resized = new Bitmap(OriginalBitmap.Width * 6, OriginalBitmap.Height * 6);
			using var g = Graphics.FromImage(resized);
			g.InterpolationMode = InterpolationMode.NearestNeighbor;
			g.DrawImage(OriginalBitmap, 0, 0, resized.Width, resized.Height);
			Info.VideoBitmap = resized;
		}
	}
}