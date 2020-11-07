using System;

namespace Core.Opcodes
{
	public abstract class BaseOp
	{
		public ushort Op { get; set; }
		public int Pos { get; internal set; }

		public const string _3ByteFormat = "X3";
		public const string WordFormat = "X4";

		public const string ByteFormat = "X1";

		protected BaseOp(ushort op)
		{
			Op = op;
		}

		internal abstract void Execute(Cpu cpu);

		public byte[] ToBytes()
		{
			var msb = Convert.ToByte((Op & 0xFF00) >> 8);
			var lsb = Convert.ToByte(Op & 0x00FF);
			return new byte[] { msb, lsb };
		}

		public override string ToString()
		{
			return $"${Pos.ToString(_3ByteFormat)} ({Op.ToString(WordFormat)}) -";
		}
	}
}