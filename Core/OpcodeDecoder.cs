using Core.Opcodes;

using System;
using System.Collections.Generic;

namespace Core
{
	internal class OpcodeDecoder
	{
		private delegate BaseOp ExecuteDel();

		private readonly Dictionary<byte, ExecuteDel> generalFunctions = new Dictionary<byte, ExecuteDel>();
		private readonly Dictionary<byte, BaseOp> functions0 = new Dictionary<byte, BaseOp>();
		private readonly Dictionary<byte, ExecuteDel> functions8 = new Dictionary<byte, ExecuteDel>();
		private readonly Dictionary<byte, ExecuteDel> functionsF = new Dictionary<byte, ExecuteDel>();

		private ushort CurrentOp;

		internal OpcodeDecoder()
		{
			InitializeFunctions();
		}

		internal BaseOp[] Decode(byte[] gameBytes)
		{
			var list = new List<BaseOp>();
			for (int i = 0; i < gameBytes.Length; i++)
			{
				var msb = gameBytes[i];
				var lsb = gameBytes[++i];
				CurrentOp = Convert.ToUInt16((msb << 8) | lsb);

				var generalOp = (byte)((msb & 0xF0) >> 4);

				if (generalFunctions.ContainsKey(generalOp))
					list.Add(generalFunctions[generalOp].Invoke());
				else
					list.Add(new OpUnknown(CurrentOp));
			}

			return list.ToArray();
		}

		private void InitializeFunctions()
		{
			generalFunctions.Add(0x0, new ExecuteDel(Op_0nnn));
			generalFunctions.Add(0x1, new ExecuteDel(() => { return new Op1nnn(CurrentOp); }));
			generalFunctions.Add(0x2, new ExecuteDel(() => { return new Op2nnn(CurrentOp); }));
			generalFunctions.Add(0x3, new ExecuteDel(() => { return new Op3xkk(CurrentOp); }));
			generalFunctions.Add(0x4, new ExecuteDel(() => { return new Op4xkk(CurrentOp); }));
			generalFunctions.Add(0x5, new ExecuteDel(() => { return new Op5xy0(CurrentOp); }));
			generalFunctions.Add(0x6, new ExecuteDel(() => { return new Op6xkk(CurrentOp); }));
			generalFunctions.Add(0x7, new ExecuteDel(() => { return new Op7xkk(CurrentOp); }));
			generalFunctions.Add(0x8, new ExecuteDel(Op_8xyn));
			generalFunctions.Add(0x9, new ExecuteDel(() => { return new Op9xy0(CurrentOp); }));
			generalFunctions.Add(0xA, new ExecuteDel(() => { return new OpAnnn(CurrentOp); }));
			generalFunctions.Add(0xB, new ExecuteDel(() => { return new OpBnnn(CurrentOp); }));
			generalFunctions.Add(0xC, new ExecuteDel(() => { return new OpCxkk(CurrentOp); }));
			generalFunctions.Add(0xD, new ExecuteDel(() => { return new OpDxyn(CurrentOp); }));
			generalFunctions.Add(0xE, new ExecuteDel(Op_Exyn));
			generalFunctions.Add(0xF, new ExecuteDel(Op_Fxyn));

			functions0.Add(0x0, new Op00E0(CurrentOp));
			functions0.Add(0xE, new Op00EE(CurrentOp));

			functions8.Add(0x0, new ExecuteDel(() => { return new Op8xy0(CurrentOp); }));
			functions8.Add(0x1, new ExecuteDel(() => { return new Op8xy1(CurrentOp); }));
			functions8.Add(0x2, new ExecuteDel(() => { return new Op8xy2(CurrentOp); }));
			functions8.Add(0x3, new ExecuteDel(() => { return new Op8xy3(CurrentOp); }));
			functions8.Add(0x4, new ExecuteDel(() => { return new Op8xy4(CurrentOp); }));
			functions8.Add(0x5, new ExecuteDel(() => { return new Op8xy5(CurrentOp); }));
			functions8.Add(0x6, new ExecuteDel(() => { return new Op8xy6(CurrentOp); }));
			functions8.Add(0x7, new ExecuteDel(() => { return new Op8xy7(CurrentOp); }));
			functions8.Add(0xE, new ExecuteDel(() => { return new Op8xyE(CurrentOp); }));

			functionsF.Add(0x7, new ExecuteDel(() => { return new OpFx07(CurrentOp); }));
			functionsF.Add(0xA, new ExecuteDel(() => { return new OpFx0A(CurrentOp); }));
			functionsF.Add(0x15, new ExecuteDel(() => { return new OpFx15(CurrentOp); }));
			functionsF.Add(0x18, new ExecuteDel(() => { return new OpFx18(CurrentOp); }));
			functionsF.Add(0x1E, new ExecuteDel(() => { return new OpFx1E(CurrentOp); }));
			functionsF.Add(0x29, new ExecuteDel(() => { return new OpFx29(CurrentOp); }));
			functionsF.Add(0x33, new ExecuteDel(() => { return new OpFx33(CurrentOp); }));
			functionsF.Add(0x55, new ExecuteDel(() => { return new OpFx55(CurrentOp); }));
			functionsF.Add(0x65, new ExecuteDel(() => { return new OpFx65(CurrentOp); }));
		}

		#region Special Functions

		private BaseOp Op_0nnn()
		{
			var specialCode = Convert.ToByte(CurrentOp & 0x000Fu);
			if (functions0.ContainsKey(specialCode))
				return functions0[specialCode];
			else
				return new OpUnknown(CurrentOp);
		}

		private BaseOp Op_8xyn()
		{
			var specialCode = Convert.ToByte(CurrentOp & 0x000Fu);
			if (functions8.ContainsKey(specialCode))
				return functions8[specialCode].Invoke();
			else
				return new OpUnknown(CurrentOp);
		}

		private BaseOp Op_Exyn()
		{
			var lsb = Convert.ToByte(CurrentOp & 0xFF);

			if (lsb == 0x9E)
				return new OpEx9E(CurrentOp);
			else if (lsb == 0xA1)
				return new OpExA1(CurrentOp);
			else
				return new OpUnknown(CurrentOp);
		}

		private BaseOp Op_Fxyn()
		{
			var specialCode = Convert.ToByte(CurrentOp & 0x00FFu);
			if (functionsF.ContainsKey(specialCode))
				return functionsF[specialCode].Invoke();
			else
				return new OpUnknown(CurrentOp);
		}

		#endregion Special Functions
	}
}