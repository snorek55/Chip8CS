using System.ComponentModel;

namespace Core.ViewModels
{
	public class DisassemblerInfoViewModel : BaseViewModel
	{
		public string IndexRegister { get; internal set; }
		public string Pc { get; internal set; }
		public string Opcode { get; internal set; }

		public BindingList<string> StackLevels { get; } = new BindingList<string>();
		public BindingList<string> VRegisters { get; } = new BindingList<string>();

		public DisassemblerInfoViewModel()
		{
			Reset();
		}

		internal void Reset()
		{
			StackLevels.Clear();
			VRegisters.Clear();
			IndexRegister = string.Empty;
			Pc = string.Empty;
			Opcode = string.Empty;

			for (int i = 0; i < 16; i++)
			{
				StackLevels.Add(i.ToString("X") + " - " + 0);
				VRegisters.Add(i.ToString("X") + " - " + 0);
			}
		}
	}
}