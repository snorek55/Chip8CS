using Core;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class VideoTests
	{
		private readonly Disassembler disassembler = new Disassembler();

		[TestMethod]
		public void ShouldPixelsBeAmplifiedx1_ReturnSamePixels()
		{
			disassembler.cpu.VideoPixels[1, 1] = true;
			disassembler.cpu.DrawingRequired = true;
			disassembler.UpdateInfo();
			var info = disassembler.Info;
			info.VideoPixels[1, 1].Should().BeTrue();
			info.VideoPixels[0, 1].Should().BeFalse();
			info.VideoPixels[1, 0].Should().BeFalse();
			info.VideoPixels[2, 1].Should().BeFalse();
			info.VideoPixels[1, 2].Should().BeFalse();
		}

		[TestMethod]
		public void ShouldPixelsBeAmplifiedx2_ReturnPixelsx2()
		{
			disassembler.cpu.VideoPixels[1, 1] = true;
			disassembler.cpu.DrawingRequired = true;
			disassembler.ScaleFactor = 2;
			disassembler.UpdateInfo();
			var info = disassembler.Info;
			info.VideoPixels[2, 2].Should().BeTrue();
			info.VideoPixels[3, 2].Should().BeTrue();
			info.VideoPixels[2, 3].Should().BeTrue();
			info.VideoPixels[3, 3].Should().BeTrue();
			info.VideoPixels[1, 1].Should().BeFalse();
			info.VideoPixels[1, 0].Should().BeFalse();
			info.VideoPixels[0, 1].Should().BeFalse();
			info.VideoPixels[0, 0].Should().BeFalse();
		}
	}
}