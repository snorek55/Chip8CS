using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Core.Games
{
	public class GameLoader
	{
		public List<FileInfo> GamesFullPaths { get; private set; } = new List<FileInfo>();

		public GameLoader()
		{
			LoadDefaultGames();
		}

		private void LoadDefaultGames()
		{
			var defaultGamesDir = AppDomain.CurrentDomain.BaseDirectory + @"Games\";
			var dirInfo = new DirectoryInfo(defaultGamesDir);
			foreach (var file in dirInfo.GetFiles())
			{
				GamesFullPaths.Add(file);
				Debug.WriteLine(file.FullName);
				Debug.WriteLine(file.Name);
				Debug.WriteLine(file.Extension);
			}
		}
	}
}