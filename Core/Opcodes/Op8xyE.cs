using System;

namespace Core.Opcodes
{
	public class Op8xyE : BaseOp
	{
		public byte Vx { get; set; }
		public byte Vy { get; set; }

		public Op8xyE(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Vy = Convert.ToByte((op & 0x00F0u) >> 4);
		}

		public override string ToString()
		{
			return $"SHL V[{Vx.ToString(ByteFormat)}]{{, V[{Vy.ToString(ByteFormat)}]}}";
		}

		public override void Execute(Cpu cpu)
		{
			// Save MSB in VF
			cpu.VRegisters[0xF] = Convert.ToByte((cpu.VRegisters[Vx] & 0x80) >> 7);

			cpu.VRegisters[Vx] <<= 1;
		}
	}
}