﻿using Core.Opcodes;

using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	public class DissasemblerInfo
	{
		public ushort IndexRegister { get; internal set; }
		public ushort Pc { get; internal set; }
		public BaseOp Opcode { get; internal set; }

		public ushort[] StackLevels { get; internal set; } = new ushort[16];
		public byte[] VRegisters { get; internal set; } = new byte[16];

		public bool[,] VideoPixels { get; set; } = new bool[64, 32];

	}
}