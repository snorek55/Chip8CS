using System;

namespace Core.Opcodes
{
	public class OpFx15 : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx15(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} LD DT,  V[{Vx.ToString(ByteFormat)}]";
		}

		internal override void Execute(Cpu cpu)
		{
			cpu.DelayTimer = cpu.VRegisters[Vx];
		}
	}
}