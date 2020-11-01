using System;

namespace Core.Opcodes
{
	public class Op8xy2 : BaseOp
	{
		public byte Vx { get; set; }
		public byte Vy { get; set; }

		public Op8xy2(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Vy = Convert.ToByte((op & 0x00F0u) >> 4);
		}

		public override string ToString()
		{
			return $"{base.ToString()} AND V[{Vx.ToString(ByteFormat)}], V[{Vy.ToString(ByteFormat)}]";
		}

		internal override void Execute(Cpu cpu)
		{
			cpu.VRegisters[Vx] &= cpu.VRegisters[Vy];
		}
	}
}