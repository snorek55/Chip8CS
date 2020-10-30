using System;

namespace Core.Opcodes
{
	public class OpBnnn : BaseOp
	{
		public ushort Address { get; set; }

		public OpBnnn(ushort op) : base(op)
		{
			Address = Convert.ToUInt16(op & 0x0FFFu);
		}

		public override string ToString()
		{
			return $"{base.ToString()} JP V0, {Address.ToString(WordFormat)}";
		}

		public override void Execute(Cpu cpu)
		{
			cpu.Pc = Convert.ToUInt16(cpu.VRegisters[0] + Address);
		}
	}
}