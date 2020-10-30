using System;

namespace Core.Opcodes
{
	public class Op8xy7 : BaseOp
	{
		public byte Vx { get; set; }
		public byte Vy { get; set; }

		public Op8xy7(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Vy = Convert.ToByte((op & 0x00F0u) >> 4);
		}

		public override string ToString()
		{
			return $"{base.ToString()} SUBN V[{Vx.ToString(ByteFormat)}], V[{Vy.ToString(ByteFormat)}]";
		}

		public override void Execute(Cpu cpu)
		{
			if (cpu.VRegisters[Vy] > cpu.VRegisters[Vx])
				cpu.VRegisters[0xF] = 1;
			else
				cpu.VRegisters[0xF] = 0;

			cpu.VRegisters[Vx] = (byte)(cpu.VRegisters[Vy] - cpu.VRegisters[Vx]);
		}
	}
}