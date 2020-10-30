using Core;
using Core.Opcodes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class DecoderTests
	{
		private readonly OpcodeDecoder decoder = new OpcodeDecoder();

		[TestMethod]
		public void Op_00E0_Ok()
		{
			byte[] gameBytes = { 0x00, 0xE0 };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var op = opcodes[0];
			op.Should().BeOfType<Op00E0>();
		}

		[TestMethod]
		public void Op_00EE_Ok()
		{
			byte[] gameBytes = { 0x00, 0xEE };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var op = opcodes[0];
			op.Should().BeOfType<Op00EE>();
		}

		[TestMethod]
		public void Op_1nnn_Ok()
		{
			byte[] gameBytes = { 0x13, 0x45 };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var op = opcodes[0];
			op.Should().BeOfType<Op1nnn>();
			var op1nnn = (Op1nnn)op;
			op1nnn.Address.Should().Be(0x345);
		}

		[TestMethod]
		public void Op_2nnn_Ok()
		{
			byte[] gameBytes = { 0x23, 0x45 };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var op = opcodes[0];
			op.Should().BeOfType<Op2nnn>();
			var op1nnn = (Op2nnn)op;
			op1nnn.Address.Should().Be(0x345);
		}
	}
}