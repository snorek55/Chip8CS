using System;

namespace Core.Opcodes
{
	public class Op9xy0 : BaseOp
	{
		public byte Vx { get; set; }
		public byte Vy { get; set; }

		public bool IsValid { get; private set; } = true;

		public Op9xy0(ushort op) : base(op)
		{
			byte firstByte = Convert.ToByte(op & 0x000Fu);
			if (firstByte != 0)
				IsValid = false;

			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Vy = Convert.ToByte((op & 0x00F0u) >> 4);
		}

		public override string ToString()
		{
			return $"{base.ToString()} SNE V[{Vx.ToString(ByteFormat)}], V[{Vy.ToString(ByteFormat)}]";
		}

		internal override void Execute(Cpu cpu)
		{
			if (!IsValid)
				throw new InvalidOperationException("Not a valid opcode");

			if (cpu.VRegisters[Vx] != cpu.VRegisters[Vy])
				cpu.Pc += 2;
		}
	}
}