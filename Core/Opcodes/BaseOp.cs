namespace Core.Opcodes
{
	public abstract class BaseOp
	{
		public ushort Op { get; set; }

		public const string OpFormat = "X4";

		protected BaseOp(ushort op)
		{
			Op = op;
		}

		public abstract void Execute(Cpu cpu);
	}
}