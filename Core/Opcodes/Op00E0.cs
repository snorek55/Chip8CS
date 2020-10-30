namespace Core.Opcodes
{
	public class Op00E0 : BaseOp
	{
		public Op00E0(ushort op) : base(op)
		{
		}

		public override string ToString()
		{
			return $"CLS";
		}

		public override void Execute(Cpu cpu)
		{
			cpu.VideoPixels = new bool[Cpu.VideoWidth, Cpu.VideoHeight];
		}
	}
}