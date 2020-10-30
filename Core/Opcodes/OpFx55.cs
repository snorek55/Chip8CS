using System;

namespace Core.Opcodes
{
	public class OpFx55 : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx55(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"LD [I], V[{Vx.ToString(ByteFormat)}]";
		}

		public override void Execute(Cpu cpu)
		{
			for (byte i = 0; i <= Vx; i++)
			{
				cpu.Memory.SetByte((ushort)(cpu.IndexRegister + i), cpu.VRegisters[i]);
			}
		}
	}
}