using System;

namespace Core.Opcodes
{
	public class Op3xkk : BaseOp
	{
		public byte Vx { get; set; }
		public byte Value { get; set; }

		public Op3xkk(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Value = Convert.ToByte(op & 0x00FFu);
		}

		public override string ToString()
		{
			return $"{base.ToString()} SE V[{Vx.ToString(ByteFormat)}], {Value.ToString(ByteFormat)}";
		}

		internal override void Execute(Cpu cpu)
		{
			if (cpu.VRegisters[Vx] == Value)
				cpu.Pc += 2;
		}
	}
}