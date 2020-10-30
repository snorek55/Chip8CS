using System;

namespace Core.Opcodes
{
	public class OpCxkk : BaseOp
	{
		public byte Vx { get; set; }
		public byte Value { get; set; }

		public OpCxkk(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Value = Convert.ToByte(op & 0x00FFu);
		}

		public override string ToString()
		{
			return $"RND V[{Vx.ToString(ByteFormat)}], {Value.ToString(ByteFormat)}";
		}

		public override void Execute(Cpu cpu)
		{
			cpu.VRegisters[Vx] = Convert.ToByte(new Random().Next(0, 0xFF) & Value);
		}
	}
}