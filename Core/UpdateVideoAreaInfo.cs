namespace Core
{
	public class UpdateVideoAreaInfo
	{
		public int XPos { get; private set; }
		public int YPos { get; private set; }
		public int Height { get; private set; }
		public const int Width = Cpu.SpriteColumns;

		public UpdateVideoAreaInfo(int xPos, int yPos, int height)
		{
			XPos = xPos;
			YPos = yPos;
			Height = height;
		}
	}
}