﻿namespace Core.Opcodes
{
	public class Op00EE : BaseOp
	{
		public Op00EE(ushort op) : base(op)
		{
		}

		public override string ToString()
		{
			return $"RET";
		}

		public override void Execute(Cpu cpu)
		{
			cpu.Pc = cpu.Stack.Pop();
		}
	}
}