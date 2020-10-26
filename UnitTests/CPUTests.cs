using Core;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class CPUTests
	{
		private CPU cpu;
		private Memory mem;
		private Stack16Levels stack;

		[TestInitialize]
		public void Initialize()
		{
			mem = new Memory();
			stack = new Stack16Levels();
			cpu = new CPU(mem, stack);
		}

		[TestMethod]
		public void WhenReadingFirstOpcode_OpcodeIsOk()
		{
			mem.SetByte(0x200, 0X10);
			mem.SetByte(0x201, 0x00);
			cpu.Cycle();
			cpu.Opcode.Should().Be(0x1000);
		}

		[TestMethod]
		public void Op_1nnn_Ok()
		{
			mem.SetByte(0x200, 0X10);
			mem.SetByte(0x201, 0x01);
			cpu.Cycle();
			cpu.Opcode.Should().Be(0x1001);
			cpu.Pc.Should().Be(1);
		}

		[TestMethod]
		public void Op_00E0_Ok()
		{
			mem.SetByte(0x200, 0X00);
			mem.SetByte(0x201, 0xE0);
			cpu.VideoPixels[3, 5] = true;
			cpu.Cycle();
			cpu.VideoPixels[3, 5].Should().BeFalse();
		}

		[TestMethod]
		public void Op_00EE_Ok()
		{
			stack.Push(0x40);
			stack.Push(0x80);
			mem.SetByte(0x200, 0X00);
			mem.SetByte(0x201, 0xEE);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X80);
		}

		[TestMethod]
		public void Op_2nnn_Ok()
		{
			mem.SetByte(0x200, 0X20);
			mem.SetByte(0x201, 0x01);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X01);

			stack.Peek().Should().Be(0);
		}

		[TestMethod]
		public void Op_3xkk_Ok()
		{
			mem.SetByte(0x200, 0X30);
			mem.SetByte(0x201, 0x10);
			cpu.VRegisters[0] = 0x10;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X204);
		}
	}
}