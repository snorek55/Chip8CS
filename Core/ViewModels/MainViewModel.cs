using Core.Games;

using System.Collections.Generic;

namespace Core.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		public List<GameListItemViewModel> Games { get; set; } = new List<GameListItemViewModel>();
		public GameListItemViewModel SelectedGame { get; set; }

		private GameLoader loader = new GameLoader();

		public MainViewModel()
		{
			foreach (var gameFileInfo in loader.GamesFullPaths)
			{
				Games.Add(new GameListItemViewModel(gameFileInfo.Name, gameFileInfo.FullName));
			}
		}
	}
}