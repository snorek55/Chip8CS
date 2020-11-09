using Core.Games;

using System.ComponentModel;

namespace Core.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		public BindingList<GameListItemViewModel> Games { get; } = new BindingList<GameListItemViewModel>();

		public string SelectedGameFullPath
		{
			get;
			set;
		}

		public DisassemblerInfoViewModel Info { get; }

		//TODO: unnecessary?
		private readonly GameLoader loader;

		private readonly Disassembler disassembler;

		public MainViewModel(GameLoader loader, Disassembler disassembler)
		{
			this.loader = loader;
			this.disassembler = disassembler;
			Info = new DisassemblerInfoViewModel();

			foreach (var gameFileInfo in loader.GamesFullPaths)
				LoadNewGame(gameFileInfo.Name, gameFileInfo.FullName);
		}

		public void LoadNewGame(string name, string fullPath)
		{
			Games.Add(new GameListItemViewModel(name, fullPath));
		}

		public void Update()
		{
			var info = disassembler.Info;
			Info.IndexRegister = "$" + info.IndexRegister.ToString("X");
			Info.Opcode = info.Opcode?.ToString();
			Info.Pc = "$" + info.Pc.ToString("X");

			var i = 0;
			foreach (var level in info.StackLevels)
			{
				Info.StackLevels[i] = "$" + i.ToString("X") + " - " + "$" + level.ToString("X");
				i++;
			}

			i = 0;
			foreach (var reg in info.VRegisters)
			{
				Info.VRegisters[i] = "$" + i.ToString("X") + " - " + "$" + reg.ToString("X");

				i++;
			}
		}

		protected void OnSelectedGameFullPathChanged()
		{
			if (SelectedGameFullPath == null)
				return;

			//requestedStop = true;
			disassembler.LoadRom(SelectedGameFullPath);
			Initialize();
		}

		private void Initialize()
		{
			Info.Reset();
		}
	}
}