using System;

namespace Core.Opcodes
{
	public class OpFx1E : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx1E(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} ADD I, V[{Vx.ToString(ByteFormat)}]";
		}

		internal override void Execute(Cpu cpu)
		{
			cpu.IndexRegister += cpu.VRegisters[Vx];
		}
	}
}