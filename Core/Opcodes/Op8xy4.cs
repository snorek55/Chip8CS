using System;

namespace Core.Opcodes
{
	public class Op8xy4 : BaseOp
	{
		public byte Vx { get; set; }
		public byte Vy { get; set; }

		public Op8xy4(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Vy = Convert.ToByte((op & 0x00F0u) >> 4);
		}

		public override string ToString()
		{
			return $"{base.ToString()} ADD V[{Vx.ToString(ByteFormat)}], V[{Vy.ToString(ByteFormat)}]";
		}

		public override void Execute(Cpu cpu)
		{
			ushort sum = Convert.ToUInt16(cpu.VRegisters[Vx] + cpu.VRegisters[Vy]);
			if (sum > 255)
				cpu.VRegisters[0xF] = 1;
			else
				cpu.VRegisters[0xF] = 0;

			cpu.VRegisters[Vx] = Convert.ToByte(sum & 0xFF);
		}
	}
}