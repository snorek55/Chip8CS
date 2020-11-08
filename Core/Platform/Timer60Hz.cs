using System.Timers;

namespace Core.Platform
{
	internal class Timer60Hz
	{
		public const double Fps = 60d;
		private int ticks;

		public int Ticks
		{
			get { return ticks; }
			set { ticks = value; if (ticks < 0) ticks = 0; }
		}

		private readonly Timer timer = new Timer((1d / Fps) * 1000);

		public Timer60Hz()
		{
			timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Enabled = true;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (ticks > 0)
				ticks--;
		}
	}
}