using Core.Opcodes;

using System;
using System.Collections.Generic;
using System.IO;

namespace Core
{
	public class Disassembler
	{
		public List<BaseOp> Opcodes { get; set; } = new List<BaseOp>();

		private readonly OpcodeDecoder decoder = new OpcodeDecoder();

		public void LoadRom(string path)
		{
			if (!File.Exists(path))
				throw new InvalidOperationException("Path does not exist");

			var gameBytes = File.ReadAllBytes(path);

			Opcodes.Clear();
			Opcodes.AddRange(decoder.Decode(gameBytes));
		}
	}
}