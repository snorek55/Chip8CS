using Core;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace UnitTests
{
	[TestClass]
	public class MemoryTests
	{
		private Memory mem;

		[TestInitialize]
		public void Initialize()
		{
			mem = new Memory();
		}

		[TestMethod]
		public void WhenAccessingValidReadPos_ShouldGetMemoryValue()
		{
			byte expectedValue = 0xD;
			mem.SetByte(0x200, expectedValue);

			var actualValue = mem.GetByte(0x200);
			actualValue.Should().Be(expectedValue);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void WhenAccessingInvalidReadPos_ShouldThrow()
		{
			mem.GetByte(0x199);
		}

		[TestMethod]
		public void WhenAccessingValidWritePos_ShouldChangeMemoryValue()
		{
			byte expectedValue = 0xD;
			mem.SetByte(0x250, expectedValue);

			var actualValue = mem.GetByte(0x250);
			actualValue.Should().Be(expectedValue);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void WhenAccessingInvalidWritePos_ShouldThrow()
		{
			mem.SetByte(0, 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void WhenAccessingFontPos_ShouldReadValueButNotSet()
		{
			byte firstFontByte = mem.GetByte(0x50);
			firstFontByte.Should().Be(0xF0);

			mem.SetByte(0x50, 0);
		}

		[TestMethod]
		public void WhenInitializing_MemoryShouldBeRestarted()
		{
			mem.Initialize();
			byte oldValue = 0x20;
			mem.SetByte(0x200, oldValue);
			mem.Initialize();
			var actualValue = mem.GetByte(0x200);
			actualValue.Should().Be(0);
		}
	}
}