using System;

namespace Core.Opcodes
{
	public class OpFx29 : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx29(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} LD F, V[{Vx.ToString(ByteFormat)}]";
		}

		internal override void Execute(Cpu cpu)
		{
			byte value = cpu.VRegisters[Vx];
			byte offset = (byte)(Memory.LetterSize * value);

			cpu.IndexRegister = Convert.ToUInt16(Memory.FonsetStartAddress + offset);
		}
	}
}