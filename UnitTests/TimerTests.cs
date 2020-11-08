using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading;

using Timer60Hz = Core.Platform.Timer60Hz;

namespace UnitTests
{
	[TestClass]
	public class TimerTests
	{
		private Timer60Hz timer;

		[TestInitialize]
		public void Initialize()
		{
			timer = new Timer60Hz();
		}

		[TestMethod]
		public void WhenFirstTimerUpdate_TicksAreTheSame()
		{
			timer.Ticks = 100;
			timer.Ticks.Should().Be(100);
		}

		[TestMethod]
		public void WhenTicksAre60_After1Sec_ShouldBeNear0()
		{
			timer.Ticks = 60;
			Thread.Sleep(1000);
			timer.Ticks.Should().BeCloseTo(0, 5);
		}

		[TestMethod]
		public void WhenTicksAre60_AfterHalfSec_ShouldBe30()
		{
			timer.Ticks = 60;
			Thread.Sleep(500);
			timer.Ticks.Should().BeCloseTo(30, 5);
		}
	}
}