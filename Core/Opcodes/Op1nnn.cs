﻿using System;

namespace Core.Opcodes
{
	public class Op1nnn : BaseOp
	{
		public ushort Address { get; set; }

		public Op1nnn(ushort op) : base(op)
		{
			Address = Convert.ToUInt16(op & 0x0FFFu);
		}

		public override string ToString()
		{
			return $"JP {Address.ToString(OpFormat)}";
		}

		public override void Execute(Cpu cpu)
		{
			cpu.Pc = Address;
		}
	}
}