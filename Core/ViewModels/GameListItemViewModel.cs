using System;

namespace Core.ViewModels
{
	public class GameListItemViewModel : BaseViewModel
	{
		public string Name { get; set; }
		public string FullPath { get; set; }

		public GameListItemViewModel(string name, string fullPath)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			FullPath = fullPath ?? throw new ArgumentNullException(nameof(fullPath));
		}

		public override string ToString()
		{
			return $"{Name} - {FullPath}";
		}
	}
}