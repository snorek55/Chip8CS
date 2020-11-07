using System;

namespace Core.Opcodes
{
	public class Op2nnn : BaseOp
	{
		public ushort Address { get; set; }

		public Op2nnn(ushort op) : base(op)
		{
			Address = Convert.ToUInt16(op & 0x0FFFu);
		}

		public override string ToString()
		{
			return $"{base.ToString()} CALL {Address.ToString(_3ByteFormat)}";
		}

		internal override void Execute(Cpu cpu)
		{
			cpu.Stack.Push(cpu.Pc);
			cpu.Pc = Address;
		}
	}
}