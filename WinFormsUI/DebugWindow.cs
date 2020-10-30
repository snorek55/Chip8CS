using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Disassembler = Core.Disassembler;

namespace WinFormsUI
{
	public partial class DebugWindow : Form
	{
		private Disassembler disassembler = new Disassembler();

		private bool requestedStop;

		public DebugWindow()
		{
			InitializeComponent();
			disassembler.LoadRom(Application.StartupPath + "PONG");
			lstbOpcodes.DataSource = disassembler.Opcodes;
		}

		private void Initialize()
		{
			disassembler.cpu.Initialize();
			disassembler.mem.LoadRom(Application.StartupPath + @"PONG");
			UpdateDebugInfo();
		}

		private void UpdateDebugInfo()
		{
			lblIndexRegister.Text = disassembler.cpu.IndexRegister.ToString("X4");
			lblOpcode.Text = disassembler.cpu.Opcode.ToString("X4");
			lblPc.Text = disassembler.cpu.Pc.ToString("X4");

			lstbStack.Items.Clear();
			var i = 0;
			foreach (var level in disassembler.stack.Levels)
			{
				lstbStack.Items.Add(i.ToString("X4") + " - " + level.ToString("X4"));
				i++;
			}

			lstbVRegisters.Items.Clear();
			i = 0;
			foreach (var reg in disassembler.cpu.VRegisters)
			{
				lstbVRegisters.Items.Add(i.ToString("X4") + " - " + reg.ToString("X4"));

				i++;
			}

			//var pc = disassembler.cpu.Pc - disassembler.mem.GameStartAddress;
			//if (pc % 2 != 0)
			//	pc--;

			//var pos = pc / 2;
			//lstbOpcodes.ClearSelected();
			//lstbOpcodes.SetSelected(pos, true);

			pbGame.Refresh();
		}

		private void btInitialize_Click(object sender, EventArgs e)
		{
			Initialize();
		}

		private void btCycle_Click(object sender, EventArgs e)
		{
			disassembler.cpu.Cycle();
			UpdateDebugInfo();
		}

		private void btRun_Click(object sender, EventArgs e)
		{
			requestedStop = false;
			try
			{
				while (!requestedStop)
				{
					disassembler.cpu.Cycle();
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
			var bound1 = disassembler.cpu.VideoPixels.GetUpperBound(0);
			var bound2 = disassembler.cpu.VideoPixels.GetUpperBound(1);
			for (int i = 0; i < bound1; i++)
			{
				for (int j = 0; j < bound2; j++)
				{
					Brush brush;
					if (disassembler.cpu.VideoPixels[i, j])
						brush = Brushes.Black;
					else
						brush = Brushes.White;

					e.Graphics.FillRectangle(brush, i, j, 1, 1);
				}
			}
		}

		private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			requestedStop = true;
		}
	}
}