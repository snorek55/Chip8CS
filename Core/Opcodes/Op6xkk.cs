using System;

namespace Core.Opcodes
{
	public class Op6xkk : BaseOp
	{
		public byte Vx { get; set; }
		public byte Value { get; set; }

		public Op6xkk(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Value = Convert.ToByte(op & 0x00FFu);
		}

		public override string ToString()
		{
			return $"LD  V[{Vx.ToString(ByteFormat)}], {Value.ToString(ByteFormat)}";
		}

		public override void Execute(Cpu cpu)
		{
			cpu.VRegisters[Vx] = Value;
		}
	}
}