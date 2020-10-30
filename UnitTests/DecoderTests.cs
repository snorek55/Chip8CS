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
			var baseOp = opcodes[0];
			baseOp.Should().BeOfType<Op00E0>();
		}

		[TestMethod]
		public void Op_00EE_Ok()
		{
			byte[] gameBytes = { 0x00, 0xEE };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var baseOp = opcodes[0];
			baseOp.Should().BeOfType<Op00EE>();
		}

		[TestMethod]
		public void Op_1nnn_Ok()
		{
			byte[] gameBytes = { 0x13, 0x45 };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var baseOp = opcodes[0];
			baseOp.Should().BeOfType<Op1nnn>();
			var op = (Op1nnn)baseOp;
			op.Address.Should().Be(0x345);
		}

		[TestMethod]
		public void Op_2nnn_Ok()
		{
			byte[] gameBytes = { 0x23, 0x45 };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var baseOp = opcodes[0];
			baseOp.Should().BeOfType<Op2nnn>();
			var op = (Op2nnn)baseOp;
			op.Address.Should().Be(0x345);
		}

		[TestMethod]
		public void Op_3xkk_Ok()
		{
			byte[] gameBytes = { 0x33, 0x45 };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var baseOp = opcodes[0];
			baseOp.Should().BeOfType<Op3xkk>();
			var op = (Op3xkk)baseOp;
			op.Vx.Should().Be(0x3);
			op.Value.Should().Be(0x45);
		}

		[TestMethod]
		public void Op_4xkk_Ok()
		{
			byte[] gameBytes = { 0x43, 0x45 };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var baseOp = opcodes[0];
			baseOp.Should().BeOfType<Op4xkk>();
			var op = (Op4xkk)baseOp;
			op.Vx.Should().Be(0x3);
			op.Value.Should().Be(0x45);
		}

		[TestMethod]
		public void Op_5xy0_Ok()
		{
			byte[] gameBytes = { 0x53, 0x50 };
			var opcodes = decoder.Decode(gameBytes);
			opcodes.Should().HaveCount(1);
			var baseOp = opcodes[0];
			baseOp.Should().BeOfType<Op5xy0>();
			var op = (Op5xy0)baseOp;
			op.Vx.Should().Be(0x3);
			op.Vy.Should().Be(0x5);
		}
	}
}