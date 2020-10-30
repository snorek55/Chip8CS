using System;

namespace Core.Opcodes
{
	public class OpExA1 : BaseOp
	{
		public byte Vx { get; set; }

		public OpExA1(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} SKNP V[{Vx.ToString(ByteFormat)}]";
		}

		public override void Execute(Cpu cpu)
		{
			byte key = cpu.VRegisters[Vx];

			if (!cpu.KeyState[key])
				cpu.Pc += 2;
		}
	}
}