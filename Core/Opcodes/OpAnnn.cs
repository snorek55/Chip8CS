﻿using System;

namespace Core.Opcodes
{
	public class OpAnnn : BaseOp
	{
		public ushort Address { get; set; }

		public OpAnnn(ushort op) : base(op)
		{
			Address = Convert.ToUInt16(op & 0x0FFFu);
		}

		public override string ToString()
		{
			return $"{base.ToString()} LD  I, {Address.ToString(_3ByteFormat)}";
		}

		internal override void Execute(Cpu cpu)
		{
			cpu.IndexRegister = Address;
		}
	}
}