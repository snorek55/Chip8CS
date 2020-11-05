using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Disassembler = Core.Disassembler;

namespace WinFormsUI
{
	public partial class DebugWindow : Form
	{
		private Disassembler disassembler = new Disassembler();

		private bool requestedStop;
		private readonly SynchronizationContext synchronizationContext;

		public DebugWindow()
		{
			InitializeComponent();
			disassembler.ScaleFactor = 6;
			pbGame.Width = disassembler.CurrentWidth + 40;//Must take into account the border
			pbGame.Height = disassembler.CurrentHeight + 40;
			synchronizationContext = SynchronizationContext.Current;
		}

		private void Initialize()
		{
			disassembler.LoadRom(Application.StartupPath + "PONG");
			lstbOpcodes.DataSource = disassembler.Opcodes;
			for (int i = 0; i < 16; i++)
			{
				lstbStack.Items.Add(i.ToString("X") + " - " + 0);
				lstbVRegisters.Items.Add(i.ToString("X") + " - " + 0);
			}

			UpdateDebugInfo();
		}

		private void UpdateDebugInfo()
		{
			var info = disassembler.Info;
			lblIndexRegister.Text = info.IndexRegister.ToString("X");
			lblOpcode.Text = info.Opcode?.ToString();
			lblPc.Text = info.Pc.ToString("X");

			var i = 0;
			foreach (var level in info.StackLevels)
			{
				lstbStack.Items[i] = i.ToString("X") + " - " + level.ToString("X");
				i++;
			}

			i = 0;
			foreach (var reg in info.VRegisters)
			{
				lstbVRegisters.Items[i] = i.ToString("X") + " - " + reg.ToString("X");

				i++;
			}

			lstbOpcodes.ClearSelected();

			if (info.Opcode != null)
			{
				var index = lstbOpcodes.Items.IndexOf(info.Opcode);
				lstbOpcodes.SetSelected(index, true);
			}

			if (info.DrawingRequired)
				pbGame.Refresh();
		}

		private void btInitialize_Click(object sender, EventArgs e)
		{
			Initialize();
		}

		private void btCycle_Click(object sender, EventArgs e)
		{
			disassembler.Cycle();
			UpdateDebugInfo();
		}

		private async void btRun_Click(object sender, EventArgs e)
		{
			requestedStop = false;
			await Task.Run(() => { Run(); });
		}

		private void Run()
		{
			Stopwatch stopwatch = new Stopwatch();
			while (!requestedStop)
			{
				stopwatch.Reset();
				stopwatch.Start();
				disassembler.Cycle();

				synchronizationContext.Send(new SendOrPostCallback(_ => { UpdateDebugInfo(); }),
				new object());
				stopwatch.Stop();
				var measuredTime = stopwatch.ElapsedMilliseconds;
				Debug.WriteLine($"It took {measuredTime}ms");
			}
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			requestedStop = true;
		}

		private void pbGame_Paint(object sender, PaintEventArgs e)
		{
			var info = disassembler.Info;
			var bound1 = disassembler.CurrentWidth;
			var bound2 = disassembler.CurrentHeight;
			for (int i = 0; i < bound1; i++)
			{
				for (int j = 0; j < bound2; j++)
				{
					Brush brush;
					if (info.VideoPixels[i][j])
						brush = Brushes.White;
					else
						brush = Brushes.Black;

					e.Graphics.FillRectangle(brush, i, j, 1, 1);
				}
			}
		}

		private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			requestedStop = true;
		}

		private void DebugWindow_KeyUp(object sender, KeyEventArgs e)
		{
			var key = e.KeyCode;
			int num = ParseKeyNum(key);
			if (num < 0)
				return;

			disassembler.OnKeyChanged(num, false);
		}

		private void DebugWindow_KeyDown(object sender, KeyEventArgs e)
		{
			var key = e.KeyCode;
			int num = ParseKeyNum(key);
			if (num < 0)
				return;

			disassembler.OnKeyChanged(num, true);
		}

		private int ParseKeyNum(Keys key)
		{
			switch (key)
			{
				case Keys.D1:
					return 0;

				case Keys.D2:
					return 1;

				case Keys.D3:
					return 2;

				case Keys.D4:
					return 3;

				case Keys.Q:
					return 4;

				case Keys.W:
					return 5;

				case Keys.E:
					return 6;

				case Keys.R:
					return 7;

				case Keys.A:
					return 8;

				case Keys.S:
					return 9;

				case Keys.D:
					return 10;

				case Keys.F:
					return 11;

				case Keys.Z:
					return 12;

				case Keys.X:
					return 13;

				case Keys.C:
					return 14;

				case Keys.V:
					return 15;

				default:
					return -1;
			}
		}
	}
}