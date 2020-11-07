namespace Core.Opcodes
{
	public class OpUnknown : BaseOp
	{
		public OpUnknown(ushort op) : base(op)
		{
			Op = op;
		}

		internal override void Execute(Cpu cpu)
		{
			//Do nothing
		}

		public override string ToString()
		{
			return $"{base.ToString()} UNKOWN: {Op.ToString(_3ByteFormat)}";
		}
	}
}