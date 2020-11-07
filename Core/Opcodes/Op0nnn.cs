using System;

namespace Core.Opcodes
{
	public class Op0nnn : BaseOp
	{
		public ushort Address { get; set; }

		public Op0nnn(ushort op) : base(op)
		{
			Address = Convert.ToUInt16(op & 0x0FFFu);
		}

		public override string ToString()
		{
			return $"{base.ToString()} SYS {Address.ToString(_3ByteFormat)}";
		}

		internal override void Execute(Cpu cpu)
		{
			//Do nothing;
		}
	}
}