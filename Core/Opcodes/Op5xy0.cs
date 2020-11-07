using System;

namespace Core.Opcodes
{
	public class Op5xy0 : BaseOp
	{
		public bool IsValid { get; private set; } = true;
		public byte Vx { get; set; }
		public byte Vy { get; set; }

		public Op5xy0(ushort op) : base(op)
		{
			byte firstByte = Convert.ToByte(op & 0x000Fu);
			if (firstByte != 0)
				IsValid = false;

			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Vy = Convert.ToByte((op & 0x00F0u) >> 4);
		}

		public override string ToString()
		{
			return $"{base.ToString()} SE V[{Vx.ToString(ByteFormat)}], V[{Vy.ToString(ByteFormat)}]";
		}

		internal override void Execute(Cpu cpu)
		{
			if (!IsValid)
				throw new InvalidOperationException("Opcode does not exist");

			if (cpu.VRegisters[Vx] == cpu.VRegisters[Vy])
				cpu.Pc += 2;
		}
	}
}