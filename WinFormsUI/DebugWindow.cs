using System;
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
			while (!requestedStop)
			{
				disassembler.Cycle();

				synchronizationContext.Send(new SendOrPostCallback(_ => { UpdateDebugInfo(); }), new object());
			}
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			requestedStop = true;
		}

		private void pbGame_Paint(object sender, PaintEventArgs e)
		{
			var info = disassembler.Info;
			var bound1 = info.VideoPixels.GetUpperBound(0) + 1;
			var bound2 = info.VideoPixels.GetUpperBound(1) + 1;
			for (int i = 0; i < bound1; i++)
			{
				for (int j = 0; j < bound2; j++)
				{
					Brush brush;
					if (info.VideoPixels[i, j])
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
	}
}