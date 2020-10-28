using Core;

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsUI
{
	public partial class DebugWindow : Form
	{
		private Cpu Cpu;
		private Memory mem;
		private Stack16Levels stack;

		private bool requestedStop;

		public DebugWindow()
		{
			InitializeComponent();
			mem = new Memory();
			stack = new Stack16Levels();
			Cpu = new Cpu(mem, stack);
			InitializeGameInstructions();
		}

		private void Initialize()
		{
			Cpu.Initialize();
			mem.LoadRom(Application.StartupPath + @"PONG");
			UpdateDebugInfo();
		}

		private void InitializeGameInstructions()
		{
			var bytes = File.ReadAllBytes(Application.StartupPath + @"PONG");
			string line = string.Empty;
			for (int i = 0; i < bytes.Length; i++)
			{
				if (i % 2 != 0)
				{
					line += " " + bytes[i].ToString("X4");
					lstbOpcodes.Items.Add(line);
					line = string.Empty;
				}
				else
				{
					line = bytes[i].ToString("X4");
				}
			}
		}

		private void UpdateDebugInfo()
		{
			lblIndexRegister.Text = Cpu.IndexRegister.ToString("X4");
			lblOpcode.Text = Cpu.Opcode.ToString("X4");
			lblPc.Text = Cpu.Pc.ToString("X4");

			lstbStack.Items.Clear();
			var i = 0;
			foreach (var level in stack.Levels)
			{
				lstbStack.Items.Add(i.ToString("X4") + " - " + level.ToString("X4"));
				i++;
			}

			lstbVRegisters.Items.Clear();
			i = 0;
			foreach (var reg in Cpu.VRegisters)
			{
				lstbVRegisters.Items.Add(i.ToString("X4") + " - " + reg.ToString("X4"));

				i++;
			}

			var pc = Cpu.Pc - Memory.GameStartAddress;
			if (pc % 2 != 0)
				pc--;

			var pos = pc / 2;
			lstbOpcodes.ClearSelected();
			lstbOpcodes.SetSelected(pos, true);

			pbGame.Refresh();
		}

		private void btInitialize_Click(object sender, System.EventArgs e)
		{
			Initialize();
		}

		private void btCycle_Click(object sender, System.EventArgs e)
		{
			Cpu.Cycle();
			UpdateDebugInfo();
		}

		private void btRun_Click(object sender, System.EventArgs e)
		{
			requestedStop = false;
			try
			{
				while (!requestedStop)
				{
					Cpu.Cycle();
					UpdateDebugInfo();
					Application.DoEvents();
					Thread.Sleep(1);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			requestedStop = true;
		}

		private void pbGame_Paint(object sender, PaintEventArgs e)
		{
			Brush brush = null;
			var bound1 = Cpu.VideoPixels.GetUpperBound(0);
			var bound2 = Cpu.VideoPixels.GetUpperBound(1);
			for (int i = 0; i < bound1; i++)
			{
				for (int j = 0; j < bound2; j++)
				{
					if (Cpu.VideoPixels[i, j])
						brush = Brushes.Black;
					else
						brush = Brushes.White;

					e.Graphics.FillRectangle(brush, i, j, 1, 1);
				}
			}
		}
	}
}