namespace Core.Opcodes
{
	public abstract class BaseOp
	{
		public ushort Op { get; set; }
		public int Pos { get; internal set; }

		public const string WordFormat = "X3";

		public const string ByteFormat = "X1";

		protected BaseOp(ushort op)
		{
			Op = op;
		}

		public abstract void Execute(Cpu cpu);

		public override string ToString()
		{
			return $"${Pos.ToString(WordFormat)} -";
		}
	}
}