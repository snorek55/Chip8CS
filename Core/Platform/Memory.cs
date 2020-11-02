using Core.Opcodes;

using System;
using System.Collections.Generic;

namespace Core
{
	internal class Memory
	{
		public static readonly byte[] Fontset =
		{
			 0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
			 0x20, 0x60, 0x20, 0x20, 0x70, // 1
			 0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
			 0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
			 0x90, 0x90, 0xF0, 0x10, 0x10, // 4
			 0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
			 0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
			 0xF0, 0x10, 0x20, 0x40, 0x40, // 7
			 0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
			 0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
			 0xF0, 0x90, 0xF0, 0x90, 0x90, // A
			 0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
			 0xF0, 0x80, 0x80, 0x80, 0xF0, // C
			 0xE0, 0x90, 0x90, 0x90, 0xE0, // D
			 0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
			 0xF0, 0x80, 0xF0, 0x80, 0x80  // F
		};

		public static readonly byte LetterSize = 5;

		public static readonly ushort FonsetStartAddress = 0x50;

		public static readonly ushort GameStartAddress = 0x200;

		private const ushort MaxBytes = 4096;

		public IList<BaseOp> GameOps { get; private set; } = new List<BaseOp>();

		private byte[] bytes = new byte[MaxBytes];
		private OpcodeDecoder decoder;

		internal Memory(OpcodeDecoder decoder)
		{
			this.decoder = decoder;
			Initialize();
		}

		internal void Initialize()
		{
			bytes = new byte[MaxBytes];
			GameOps.Clear();

			for (int i = 0; i < 80; ++i)
				bytes[FonsetStartAddress + i] = Fontset[i];
		}

		public void LoadGame(byte[] gameBytes)
		{
			Initialize();
			for (int i = 0; i < gameBytes.Length;)
			{
				var msb = gameBytes[i];
				var pos = GameStartAddress + i;
				bytes[GameStartAddress + i] = msb;
				i++;
				var lsb = gameBytes[i];
				bytes[GameStartAddress + i] = lsb;
				i++;
				var op = decoder.DecodeOp(msb, lsb);
				op.Pos = pos;
				GameOps.Add(op);
			}
		}

		public BaseOp GetOpCode(ushort pc)
		{
			var rva = pc - GameStartAddress;
			if (rva % 2 != 0)
				rva--;

			var opIndex = rva / 2;
			return GameOps[opIndex];
		}

		public byte GetByte(ushort pos)
		{
			if (IsValidReadPos(pos))
				return bytes[pos];

			throw new ArgumentException($"Not a valid read pos {pos}");
		}

		public void SetByte(ushort pos, byte value)
		{
			if (IsValidWritePos(pos))
				bytes[pos] = value;
			else
				throw new ArgumentException($"Not a valid write pos {pos}");
		}

		private bool IsValidWritePos(ushort pos)
		{
			return pos >= 0x200 && pos <= 0xFFF;
		}

		private bool IsValidReadPos(ushort pos)
		{
			return IsValidWritePos(pos) || (pos >= FonsetStartAddress && pos <= 0xA0);
		}
	}
}