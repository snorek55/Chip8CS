using Core;
using Core.Games;
using Core.ViewModels;

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGUI
{
	public partial class Form1 : Form
	{
		private Disassembler disassembler = new Disassembler();

		private GameLoader loader = new GameLoader();
		private MainViewModel mainViewModel = new MainViewModel();
		private bool requestedStop;

		private readonly SynchronizationContext synchronizationContext;

		public Form1()
		{
			InitializeComponent();
			bsMainView.DataSource = mainViewModel;

			pbGame.Image = disassembler.Info.VideoBitmap;
			synchronizationContext = SynchronizationContext.Current;
			lstbGames.DataSource = loader.GamesFullPaths;
			lstbGames.DisplayMember = nameof(FileInfo.Name);
		}

		private void Initialize()
		{
			lstbOpcodes.DataSource = null;
			lstbStack.Items.Clear();
			lstbVRegisters.Items.Clear();

			for (int i = 0; i < 16; i++)
			{
				lstbStack.Items.Add(i.ToString("X") + " - " + 0);
				lstbVRegisters.Items.Add(i.ToString("X") + " - " + 0);
			}

			UpdateGuiInfo();
		}

		private void UpdateGuiInfo()
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

			//lstbOpcodes.ClearSelected();

			//if (info.Opcode != null)
			//{
			//	var index = lstbOpcodes.Items.IndexOf(info.Opcode);
			//	lstbOpcodes.SetSelected(index, true);
			//}

			if (info.DrawingRequired)
			{
				pbGame.Image = info.VideoBitmap;
				pbGame.Size = info.VideoBitmap.Size;
			}
		}

		private void Run()
		{
			Stopwatch stopwatch = new Stopwatch();
			var clockTickMs = 0;
			while (!requestedStop)
			{
				stopwatch.Reset();
				stopwatch.Start();
				disassembler.Cycle();
				synchronizationContext.Send(new SendOrPostCallback(_ => { UpdateGuiInfo(); }),
				new object());
				var difTime = clockTickMs - (int)stopwatch.ElapsedMilliseconds;
				if (difTime > 0)
					Thread.Sleep(difTime);

				Debug.WriteLine($"Cpu+GUI took {stopwatch.ElapsedMilliseconds}ms");
			}
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

		#region Events

		private void btCycle_Click(object sender, EventArgs e)
		{
			disassembler.Cycle();
			UpdateGuiInfo();
		}

		private async void btRun_Click(object sender, EventArgs e)
		{
			requestedStop = false;
			await Task.Run(() => { Run(); });
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			requestedStop = true;
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

		private void lstbGames_SelectedIndexChanged(object sender, EventArgs e)
		{
			requestedStop = true;

			if (lstbGames.SelectedIndex == -1)
				return;

			var gameInfo = (FileInfo)lstbGames.Items[lstbGames.SelectedIndex];
			disassembler.LoadRom(gameInfo.FullName);
			Initialize();
		}

		private void btReset_Click(object sender, EventArgs e)
		{
			requestedStop = true;
			disassembler.Reset();
			UpdateGuiInfo();
		}

		private void btLoadRom_Click(object sender, EventArgs e)
		{
			if (ofdLoadRom.ShowDialog() == DialogResult.OK)
			{
				var fileName = ofdLoadRom.FileName;
				var fileInfo = new FileInfo(fileName);
				loader.GamesFullPaths.Add(fileInfo);
				lstbGames.DataSource = null;
				lstbGames.DataSource = loader.GamesFullPaths;
				lstbGames.DisplayMember = nameof(FileInfo.Name);
			}
		}

		#endregion Events
	}
}