﻿using System;

namespace Core.Opcodes
{
	public class Op7xkk : BaseOp
	{
		public byte Vx { get; set; }
		public byte Value { get; set; }

		public Op7xkk(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Value = Convert.ToByte(op & 0x00FFu);
		}

		public override string ToString()
		{
			return $"{base.ToString()} ADD  V[{Vx.ToString(ByteFormat)}], {Value.ToString(ByteFormat)}";
		}

		internal override void Execute(Cpu cpu)
		{
			cpu.VRegisters[Vx] += Value;
		}
	}
}