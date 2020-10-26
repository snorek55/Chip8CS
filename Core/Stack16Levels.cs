using System;

namespace Core
{
	public class Stack16Levels
	{
		private const int MaxLevels = 16;
		private ushort[] levels = new ushort[MaxLevels];
		private int sp;

		public void Skip()
		{
			if (sp == MaxLevels - 1)
				throw new StackOverflowException();
			else
				++sp;
		}

		public void Push(ushort value)
		{
			if (sp == MaxLevels - 1)
				throw new StackOverflowException();
			else

				levels[++sp] = value;
		}

		public ushort Pop()
		{
			if (sp == -1)
				throw new InvalidOperationException("Stack is empty");
			else
				return levels[sp--];
		}

		public void Clear()
		{
			levels = new ushort[MaxLevels];
			sp = -1;
		}

		internal ushort Peek()
		{
			return levels[sp];
		}
	}
}