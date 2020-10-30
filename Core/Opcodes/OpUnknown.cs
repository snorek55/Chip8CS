namespace Core.Opcodes
{
	public class OpUnknown : BaseOp
	{
		public ushort Op { get; set; }

		public OpUnknown(ushort op) : base(op)
		{
			Op = op;
		}

		public override void Execute(Cpu cpu)
		{
			//Do nothing
		}

		public override string ToString()
		{
			return $"Unkown opcode = {Op.ToString(OpFormat)}";
		}
	}
}