using System;

namespace Core.Opcodes
{
	public class OpFx0A : BaseOp
	{
		public byte Vx { get; set; }

		public OpFx0A(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
		}

		public override string ToString()
		{
			return $"{base.ToString()} LD V[{Vx.ToString(ByteFormat)}], K";
		}

		internal override void Execute(Cpu cpu)
		{
			for (int i = 0; i < cpu.KeyState.Length; i++)
			{
				if (cpu.KeyState[i])
				{
					cpu.VRegisters[Vx] = Convert.ToByte(i);
					return;
				}
			}
			//Else wait
			cpu.Pc -= 2;
		}
	}
}