using Core;
using Core.Opcodes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace UnitTests
{
	[TestClass]
	public class CPUTests
	{
		private Cpu cpu;
		private Memory mem;
		private Stack16Levels stack;

		[TestInitialize]
		public void Initialize()
		{
			mem = new Memory(new OpcodeDecoder());
			stack = new Stack16Levels();
			cpu = new Cpu(mem, stack);
		}

		[TestMethod]
		public void WhenReadingFirstOpcode_OpcodeIsOk()
		{
			mem.GameOps.Add(new Op1nnn(0x1000));
			mem.SetByte(0x200, 0X10);
			mem.SetByte(0x201, 0x00);
			cpu.Cycle();
			cpu.Opcode.Op.Should().Be(0x1000);
		}

		[TestMethod]
		public void Op_1nnn_Ok()
		{
			mem.GameOps.Add(new Op1nnn(0x1001));
			cpu.Cycle();
			cpu.Opcode.Op.Should().Be(0x1001);
			cpu.Pc.Should().Be(1);
		}

		[TestMethod]
		public void Op_00E0_Ok()
		{
			mem.GameOps.Add(new Op00E0(0));
			cpu.VideoPixels[3, 5] = true;
			cpu.Cycle();
			cpu.VideoPixels[3, 5].Should().BeFalse();
		}

		[TestMethod]
		public void Op_00EE_Ok()
		{
			stack.Push(0x40);
			stack.Push(0x80);
			mem.GameOps.Add(new Op00EE(0));
			cpu.Cycle();
			cpu.Pc.Should().Be(0X80);
		}

		[TestMethod]
		public void Op_2nnn_Ok()
		{
			mem.GameOps.Add(new Op2nnn(0x2001));
			cpu.Cycle();
			cpu.Pc.Should().Be(0X01);

			stack.Peek().Should().Be(0x202);
		}

		[TestMethod]
		public void Op_3xkk_WhenVRegAndByte_AreEqual()
		{
			mem.GameOps.Add(new Op3xkk(0x3010));
			cpu.VRegisters[0] = 0x10;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X204);
		}

		[TestMethod]
		public void Op_3xkk_WhenVRegAndByte_AreNotEqual()
		{
			mem.GameOps.Add(new Op3xkk(0x3011));
			cpu.VRegisters[0] = 0x10;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);
		}

		[TestMethod]
		public void Op_4xkk__WhenVRegAndByte_AreEqual()
		{
			mem.GameOps.Add(new Op4xkk(0x4011));
			cpu.VRegisters[0] = 0x11;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);
		}

		[TestMethod]
		public void Op_4xkk_WhenVRegAndByte_AreNotEqual()
		{
			mem.GameOps.Add(new Op4xkk(0x4010));
			cpu.VRegisters[0] = 0x11;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X204);
		}

		[TestMethod]
		public void Op_5xy0_WhenVRegisters_AreEqual()
		{
			mem.GameOps.Add(new Op5xy0(0x5120));

			cpu.VRegisters[1] = 0x12;
			cpu.VRegisters[2] = 0x12;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X204);
		}

		[TestMethod]
		public void Op_5xy0_WhenVRegisters_AreNotEqual()
		{
			mem.GameOps.Add(new Op5xy0(0x5120));
			cpu.VRegisters[1] = 0x12;
			cpu.VRegisters[2] = 0x1;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Op_5xyn_Throws_WhenNIsNot0()
		{
			mem.GameOps.Add(new Op5xy0(0x5121));
			cpu.Cycle();
		}

		[TestMethod]
		public void Op_6xkk_Ok()
		{
			mem.GameOps.Add(new Op6xkk(0x6126));
			cpu.VRegisters[1] = 0x12;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);
			cpu.VRegisters[1].Should().Be(0x26);
		}

		[TestMethod]
		public void Op_7xkk_Ok()
		{
			mem.GameOps.Add(new Op7xkk(0x7126));
			cpu.VRegisters[1] = 0x12;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);
			cpu.VRegisters[1].Should().Be(0x38);
		}

		[TestMethod]
		public void Op_8xy0_Ok()
		{
			mem.GameOps.Add(new Op8xy0(0x8120));
			cpu.VRegisters[1] = 0x15;
			cpu.VRegisters[2] = 0x56;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);
			cpu.VRegisters[1].Should().Be(0x56);
		}

		[TestMethod]
		public void Op_8xy1_Ok()
		{
			mem.GameOps.Add(new Op8xy1(0x8121));
			cpu.VRegisters[1] = 0x15;
			cpu.VRegisters[2] = 0x56;
			var expected = Convert.ToByte(cpu.VRegisters[1] | cpu.VRegisters[2]);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expected);
		}

		[TestMethod]
		public void Op_8xy2_Ok()
		{
			mem.GameOps.Add(new Op8xy2(0x8122));
			cpu.VRegisters[1] = 0x15;
			cpu.VRegisters[2] = 0x56;
			var expected = Convert.ToByte(cpu.VRegisters[1] & cpu.VRegisters[2]);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expected);
		}

		[TestMethod]
		public void Op_8xy3_Ok()
		{
			mem.GameOps.Add(new Op8xy3(0x8123));
			cpu.VRegisters[1] = 0x15;
			cpu.VRegisters[2] = 0x56;
			var expected = Convert.ToByte(cpu.VRegisters[1] ^ cpu.VRegisters[2]);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expected);
		}

		[TestMethod]
		public void Op_8xy4_SumLessThan256_ShouldNotSetCarry_AndSumLowest8Bits()
		{
			mem.GameOps.Add(new Op8xy4(0x8124));
			cpu.VRegisters[1] = 0x8;
			cpu.VRegisters[2] = 0x3;
			cpu.VRegisters[0xF] = 2;//just to make sure it changes
			var expectedSum = Convert.ToByte(cpu.VRegisters[1] + cpu.VRegisters[2]);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expectedSum);
			cpu.VRegisters[0xF].Should().Be(0);
		}

		[TestMethod]
		public void Op_8xy4_SumGreaterThan256_ShouldSetCarry_AndSumLowest8Bits()
		{
			mem.GameOps.Add(new Op8xy4(0x8124));
			cpu.VRegisters[1] = 0xFF;
			cpu.VRegisters[2] = 0x68;
			cpu.VRegisters[0xF] = 2;//just to make sure it changes
			var word = cpu.VRegisters[1] + cpu.VRegisters[2];
			var expectedSum = Convert.ToByte(word & 0xFF);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expectedSum);
			cpu.VRegisters[0xF].Should().Be(1);
		}

		[TestMethod]
		public void Op_8xy5_WithNoBorrow()
		{
			mem.GameOps.Add(new Op8xy5(0x8125));
			cpu.VRegisters[1] = 0x8;
			cpu.VRegisters[2] = 0x3;
			cpu.VRegisters[0xF] = 2;//just to make sure it changes
			var expectedSub = Convert.ToByte(cpu.VRegisters[1] - cpu.VRegisters[2]);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expectedSub);
			cpu.VRegisters[0xF].Should().Be(1);
		}

		[TestMethod]
		public void Op_8xy5_WithBorrow()
		{
			mem.GameOps.Add(new Op8xy5(0x8125));
			cpu.VRegisters[1] = 0x3;
			cpu.VRegisters[2] = 0x54;
			cpu.VRegisters[0xF] = 2;//just to make sure it changes
			var word = cpu.VRegisters[1] - cpu.VRegisters[2];
			var expectedSub = Convert.ToByte(word & 0xFF);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expectedSub);
			cpu.VRegisters[0xF].Should().Be(0);
		}

		[TestMethod]
		public void Op_8xy6_CarryShouldBe1_SHR_Ok()
		{
			mem.GameOps.Add(new Op8xy6(0x8126));
			cpu.VRegisters[1] = 0x15;
			var expected = Convert.ToByte(cpu.VRegisters[1] >> 1);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expected);
			cpu.VRegisters[0xF].Should().Be(1);
		}

		[TestMethod]
		public void Op_8xy6_CarryShouldNotBe1_SHR_Ok()
		{
			mem.GameOps.Add(new Op8xy6(0x8126));
			cpu.VRegisters[1] = 0x14;
			var expected = Convert.ToByte(cpu.VRegisters[1] >> 1);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expected);
			cpu.VRegisters[0xF].Should().Be(0);
		}

		[TestMethod]
		public void Op_8xy7_WithNoBorrow()
		{
			mem.GameOps.Add(new Op8xy7(0x8127));
			cpu.VRegisters[1] = 0x2;
			cpu.VRegisters[2] = 0x8;
			cpu.VRegisters[0xF] = 2;//just to make sure it changes
			var expectedSub = Convert.ToByte(cpu.VRegisters[2] - cpu.VRegisters[1]);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expectedSub);
			cpu.VRegisters[0xF].Should().Be(1);
		}

		[TestMethod]
		public void Op_8xy7_WithBorrow()
		{
			mem.GameOps.Add(new Op8xy7(0x8127));
			cpu.VRegisters[1] = 0x10;
			cpu.VRegisters[2] = 0x6;
			cpu.VRegisters[0xF] = 2;//just to make sure it changes
			var word = cpu.VRegisters[2] - cpu.VRegisters[1];
			var expectedSub = Convert.ToByte(word & 0xFF);
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);

			cpu.VRegisters[1].Should().Be(expectedSub);
			cpu.VRegisters[0xF].Should().Be(0);
		}

		[TestMethod]
		public void Op_9xy0_WhenVRegisters_AreEqual()
		{
			mem.GameOps.Add(new Op9xy0(0x9120));
			cpu.VRegisters[1] = 0x12;
			cpu.VRegisters[2] = 0x12;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X202);
		}

		[TestMethod]
		public void Op_9xy0_WhenVRegisters_AreNotEqual()
		{
			mem.GameOps.Add(new Op9xy0(0x9120));
			cpu.VRegisters[1] = 0x12;
			cpu.VRegisters[2] = 0x11;
			cpu.Cycle();
			cpu.Pc.Should().Be(0X204);
		}

		[TestMethod]
		public void Op_Annn_Ok()
		{
			mem.GameOps.Add(new OpAnnn(0xA001));
			mem.SetByte(0x200, 0XA0);
			mem.SetByte(0x201, 0x01);
			cpu.Cycle();
			cpu.IndexRegister.Should().Be(1);
		}

		[TestMethod]
		public void Op_Bnnn_Ok()
		{
			mem.GameOps.Add(new OpBnnn(0xB001));
			mem.SetByte(0x200, 0XB0);
			mem.SetByte(0x201, 0x01);
			cpu.VRegisters[0] = 0x15;
			cpu.Cycle();
			cpu.Pc.Should().Be(0x16);
		}

		[TestMethod]
		public void Op_Ex9E_WhenKeyPressed_Skips()
		{
			mem.GameOps.Add(new OpEx9E(0xE09E));
			mem.SetByte(0x200, 0XE0);
			mem.SetByte(0x201, 0x9E);
			cpu.VRegisters[0] = 0x4;
			cpu.KeyState[4] = true;
			cpu.Cycle();
			cpu.Pc.Should().Be(0x204);
		}

		[TestMethod]
		public void Op_Ex9E_WhenKeyNotPressed_DoesNothing()
		{
			mem.GameOps.Add(new OpEx9E(0xE09E));
			mem.SetByte(0x200, 0XE0);
			mem.SetByte(0x201, 0x9E);
			cpu.VRegisters[0] = 0x4;
			cpu.KeyState[4] = false;
			cpu.Cycle();
			cpu.Pc.Should().Be(0x202);
		}

		[TestMethod]
		public void Op_ExA1_WhenKeyNotPressed_Skips()
		{
			mem.GameOps.Add(new OpExA1(0xE0A1));
			mem.SetByte(0x200, 0XE0);
			mem.SetByte(0x201, 0xA1);
			cpu.VRegisters[0] = 0x4;
			cpu.KeyState[4] = false;
			cpu.Cycle();
			cpu.Pc.Should().Be(0x204);
		}

		[TestMethod]
		public void Op_ExA1_WhenKeyPressed_DoesNothing()
		{
			mem.GameOps.Add(new OpExA1(0xE0A1));
			mem.SetByte(0x200, 0XE0);
			mem.SetByte(0x201, 0xA1);
			cpu.VRegisters[0] = 0x4;
			cpu.KeyState[4] = true;
			cpu.Cycle();
			cpu.Pc.Should().Be(0x202);
		}

		[TestMethod]
		public void Op_Fx07_Ok()
		{
			mem.GameOps.Add(new OpFx07(0xF007));
			mem.SetByte(0x200, 0XF0);
			mem.SetByte(0x201, 0x07);
			cpu.DelayTimer = 0x55;
			cpu.Cycle();
			cpu.VRegisters[0].Should().Be(0x55);
		}

		[TestMethod]
		public void Op_Fx0A_WhenKeyPressed_ReturnsKeyValueInVx()
		{
			mem.GameOps.Add(new OpFx0A(0xF00A));
			mem.SetByte(0x200, 0XF0);
			mem.SetByte(0x201, 0x0A);
			cpu.KeyState[0xE] = true;
			cpu.Cycle();
			cpu.VRegisters[0].Should().Be(0xE);
			cpu.Pc.Should().NotBe(0x200);
		}

		[TestMethod]
		public void Op_Fx0A_WhenKeyNotPressed_Waits()
		{
			mem.GameOps.Add(new OpFx0A(0xF00A));
			mem.SetByte(0x200, 0XF0);
			mem.SetByte(0x201, 0x0A);
			cpu.Cycle();
			cpu.Pc.Should().Be(0x200);
		}

		[TestMethod]
		public void Op_Fx15_Ok()
		{
			mem.GameOps.Add(new OpFx15(0xF015));
			mem.SetByte(0x200, 0XF0);
			mem.SetByte(0x201, 0x15);
			cpu.VRegisters[0] = 0x14;
			cpu.DelayTimer = 0x55;
			cpu.Cycle();
			cpu.DelayTimer.Should().Be(0x13);
		}

		[TestMethod]
		public void Op_Fx18_Ok()
		{
			mem.GameOps.Add(new OpFx18(0xF018));
			mem.SetByte(0x200, 0XF0);
			mem.SetByte(0x201, 0x18);
			cpu.VRegisters[0] = 0x14;
			cpu.SoundTimer = 0x55;
			cpu.Cycle();
			cpu.SoundTimer.Should().Be(0x13);
		}

		[TestMethod]
		public void Op_Fx1E_Ok()
		{
			mem.GameOps.Add(new OpFx1E(0xF01E));
			mem.SetByte(0x200, 0XF0);
			mem.SetByte(0x201, 0x1E);
			cpu.VRegisters[0] = 0x14;
			cpu.Cycle();
			cpu.IndexRegister.Should().Be(0x14);
		}

		[TestMethod]
		public void Op_Fx29_Ok()
		{
			mem.GameOps.Add(new OpFx29(0xF029));
			mem.SetByte(0x200, 0XF0);
			mem.SetByte(0x201, 0x29);
			cpu.VRegisters[0] = 0x14;
			cpu.Cycle();
			cpu.IndexRegister.Should().Be(0xB4);
		}

		[TestMethod]
		public void Op_Fx33_Ok()
		{
			mem.GameOps.Add(new OpFx33(0xF033));
			mem.SetByte(0x200, 0XF0);
			mem.SetByte(0x201, 0x33);
			cpu.VRegisters[0] = 0xF1;
			cpu.IndexRegister = 0x233;
			cpu.Cycle();
			cpu.IndexRegister.Should().Be(0x233);
			mem.GetByte(0x233).Should().Be(0x2);
			mem.GetByte(0x234).Should().Be(0x4);
			mem.GetByte(0x235).Should().Be(0x1);
		}

		[TestMethod]
		public void Op_Fx55_Ok()
		{
			mem.GameOps.Add(new OpFx55(0xF455));
			mem.SetByte(0x200, 0XF4);
			mem.SetByte(0x201, 0x55);
			cpu.VRegisters[0] = 0xF1;
			cpu.VRegisters[1] = 0xF2;
			cpu.VRegisters[2] = 0xF3;
			cpu.VRegisters[3] = 0xF4;
			cpu.VRegisters[4] = 0xF5;
			cpu.IndexRegister = 0x233;
			cpu.Cycle();
			cpu.IndexRegister.Should().Be(0x233);
			mem.GetByte(0x233).Should().Be(0xF1);
			mem.GetByte(0x234).Should().Be(0xF2);
			mem.GetByte(0x235).Should().Be(0xF3);
			mem.GetByte(0x236).Should().Be(0xF4);
			mem.GetByte(0x237).Should().Be(0xF5);
			for (ushort i = 0x238; i <= 0x242; i++)
			{
				mem.GetByte(i).Should().Be(0);
			}
		}

		[TestMethod]
		public void Op_Fx65_Ok()
		{
			mem.GameOps.Add(new OpFx65(0xF465));
			mem.SetByte(0x200, 0XF4);
			mem.SetByte(0x201, 0x65);
			mem.SetByte(0x233, 0xF1);
			mem.SetByte(0x234, 0xF2);
			mem.SetByte(0x235, 0xF3);
			mem.SetByte(0x236, 0xF4);
			mem.SetByte(0x237, 0xF5);

			cpu.IndexRegister = 0x233;
			cpu.Cycle();
			cpu.IndexRegister.Should().Be(0x233);
			cpu.VRegisters[0].Should().Be(0xF1);
			cpu.VRegisters[1].Should().Be(0xF2);
			cpu.VRegisters[2].Should().Be(0xF3);
			cpu.VRegisters[3].Should().Be(0xF4);
			cpu.VRegisters[4].Should().Be(0xF5);
			for (ushort i = 0x5; i < 0xF; i++)
			{
				cpu.VRegisters[i].Should().Be(0);
			}
		}
	}
}