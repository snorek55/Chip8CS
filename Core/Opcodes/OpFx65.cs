using System;

namespace Core.Opcodes
{
	public class OpFx65 : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx65(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} LD V[{Vx.ToString(ByteFormat)}], [I]";
		}

		internal override void Execute(Cpu cpu)
		{
			for (byte i = 0; i <= Vx; i++)
			{
				cpu.VRegisters[i] = cpu.Memory.GetByte((ushort)(cpu.IndexRegister + i));
			}
		}
	}
}