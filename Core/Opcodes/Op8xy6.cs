using System;

namespace Core.Opcodes
{
	public class Op8xy6 : BaseOp
	{
		public byte Vx { get; set; }
		public byte Vy { get; set; }

		public Op8xy6(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Vy = Convert.ToByte((op & 0x00F0u) >> 4);
		}

		public override string ToString()
		{
			return $"SHR  V[{Vx.ToString(ByteFormat)}], V[{Vy.ToString(ByteFormat)}]";
		}

		public override void Execute(Cpu cpu)
		{
			// Save LSB in VF
			cpu.VRegisters[0xF] = Convert.ToByte(cpu.VRegisters[Vx] & 0x1);

			cpu.VRegisters[Vx] >>= 1;
		}
	}
}