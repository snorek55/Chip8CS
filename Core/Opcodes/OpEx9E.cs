using System;

namespace Core.Opcodes
{
	public class OpEx9E : BaseOp
	{
		public byte Vx { get; set; }

		public OpEx9E(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"SKP V[{Vx.ToString(ByteFormat)}]";
		}

		public override void Execute(Cpu cpu)
		{
			byte key = cpu.VRegisters[Vx];

			if (cpu.KeyState[key])
				cpu.Pc += 2;
		}
	}
}