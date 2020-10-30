using System;

namespace Core.Opcodes
{
	public class OpFx07 : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx07(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} LD V[{Vx.ToString(ByteFormat)}], DT";
		}

		public override void Execute(Cpu cpu)
		{
			cpu.VRegisters[Vx] = cpu.DelayTimer;
		}
	}
}