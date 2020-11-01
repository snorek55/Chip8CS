using System;

namespace Core.Opcodes
{
	public class OpFx18 : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx18(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} LD ST, V[{Vx.ToString(ByteFormat)}]";
		}

		internal override void Execute(Cpu cpu)
		{
			cpu.SoundTimer = cpu.VRegisters[Vx];
		}
	}
}