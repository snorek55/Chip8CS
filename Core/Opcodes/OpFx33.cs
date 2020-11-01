using System;

namespace Core.Opcodes
{
	public class OpFx33 : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx33(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} LD B, V[{Vx.ToString(ByteFormat)}]";
		}

		internal override void Execute(Cpu cpu)
		{
			byte value = cpu.VRegisters[Vx];

			// Ones
			cpu.Memory.SetByte((ushort)(cpu.IndexRegister + 2), (byte)(value % 10));
			value /= 10;

			// Tens
			cpu.Memory.SetByte((ushort)(cpu.IndexRegister + 1), (byte)(value % 10));
			value /= 10;

			// Hundreds
			cpu.Memory.SetByte((cpu.IndexRegister), (byte)(value % 10));
		}
	}
}