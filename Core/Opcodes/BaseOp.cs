namespace Core.Opcodes
{
	public abstract class BaseOp
	{
		public ushort Op { get; set; }

		public const string WordFormat = "X4";

		public const string ByteFormat = "X1";

		protected BaseOp(ushort op)
		{
			Op = op;
		}

		public abstract void Execute(Cpu cpu);
	}
}