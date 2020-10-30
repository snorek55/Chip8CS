using Core.Opcodes;

using System;
using System.Collections.Generic;

namespace Core
{
	internal class OpcodeDecoder
	{
		private delegate BaseOp ExecuteDel();

		private readonly Dictionary<byte, ExecuteDel> generalFunctions = new Dictionary<byte, ExecuteDel>();
		private readonly Dictionary<byte, ExecuteDel> functions0 = new Dictionary<byte, ExecuteDel>();
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
			generalFunctions.Add(0x1, new ExecuteDel(Op_1nnn));
			generalFunctions.Add(0x2, new ExecuteDel(Op_2nnn));
			//generalFunctions.Add(0x3, new ExecuteDel(Op_3xkk));
			//generalFunctions.Add(0x4, new ExecuteDel(Op_4xkk));
			//generalFunctions.Add(0x5, new ExecuteDel(Op_5xy0));
			//generalFunctions.Add(0x6, new ExecuteDel(Op_6xkk));
			//generalFunctions.Add(0x7, new ExecuteDel(Op_7xkk));
			//generalFunctions.Add(0x8, new ExecuteDel(Op_8xyn));
			//generalFunctions.Add(0x9, new ExecuteDel(Op_9xy0));
			//generalFunctions.Add(0xA, new ExecuteDel(Op_Annn));
			//generalFunctions.Add(0xB, new ExecuteDel(Op_Bnnn));
			//generalFunctions.Add(0xC, new ExecuteDel(Op_Cxkk));
			//generalFunctions.Add(0xD, new ExecuteDel(Op_Dxyn));
			//generalFunctions.Add(0xE, new ExecuteDel(Op_Exyn));
			//generalFunctions.Add(0xF, new ExecuteDel(Op_Fxyn));

			functions0.Add(0x0, new ExecuteDel(Op_00E0));
			functions0.Add(0xE, new ExecuteDel(Op_00EE));

			//functions8.Add(0x0, new ExecuteDel(Op_8xy0));
			//functions8.Add(0x1, new ExecuteDel(Op_8xy1));
			//functions8.Add(0x2, new ExecuteDel(Op_8xy2));
			//functions8.Add(0x3, new ExecuteDel(Op_8xy3));
			//functions8.Add(0x4, new ExecuteDel(Op_8xy4));
			//functions8.Add(0x5, new ExecuteDel(Op_8xy5));
			//functions8.Add(0x6, new ExecuteDel(Op_8xy6));
			//functions8.Add(0x7, new ExecuteDel(Op_8xy7));
			//functions8.Add(0xE, new ExecuteDel(Op_8xyE));

			//functionsF.Add(0x7, new ExecuteDel(Op_Fx07));
			//functionsF.Add(0xA, new ExecuteDel(Op_Fx0A));
			//functionsF.Add(0x15, new ExecuteDel(Op_Fx15));
			//functionsF.Add(0x18, new ExecuteDel(Op_Fx18));
			//functionsF.Add(0x1E, new ExecuteDel(Op_Fx1E));
			//functionsF.Add(0x29, new ExecuteDel(Op_Fx29));
			//functionsF.Add(0x33, new ExecuteDel(Op_Fx33));
			//functionsF.Add(0x55, new ExecuteDel(Op_Fx55));
			//functionsF.Add(0x65, new ExecuteDel(Op_Fx65));
		}

		#region Functions

		private BaseOp Op_0nnn()
		{
			var specialCode = Convert.ToByte(CurrentOp & 0x000Fu);
			if (functions0.ContainsKey(specialCode))
				return functions0[specialCode].Invoke();
			else
				return new OpUnknown(CurrentOp);
		}

		//private void Op_8xyn()
		//{
		//	var specialCode = Convert.ToByte(Opcode & 0x000Fu);
		//	if (functions8.ContainsKey(specialCode))
		//		functions8[specialCode].Invoke();
		//	else
		//		throw new ArgumentException($"Such function8 not found {Opcode:X}");
		//}

		//private void Op_Exyn()
		//{
		//	var lsb = Convert.ToByte(Opcode & 0xFF);

		//	if (lsb == 0x9E)
		//		Op_Ex9E();
		//	else if (lsb == 0xA1)
		//		Op_ExA1();
		//	else
		//		throw new ArgumentException($"Such functionE not found {Opcode:X}");
		//}

		//private void Op_Fxyn()
		//{
		//	var specialCode = Convert.ToByte(Opcode & 0x00FFu);
		//	if (functionsF.ContainsKey(specialCode))
		//		functionsF[specialCode].Invoke();
		//	else
		//		throw new ArgumentException($"Such function8 not found {Opcode:X}");
		//}

		///// <summary>
		///// CLS. Clear display
		///// </summary>
		private BaseOp Op_00E0()
		{
			return new Op00E0(CurrentOp);
		}

		///// <summary>
		///// RET. Return from a subroutine
		///// </summary>
		private BaseOp Op_00EE()
		{
			return new Op00EE(CurrentOp);
		}

		/// <summary>
		/// JMP. Jump to location nnn
		/// </summary>
		private BaseOp Op_1nnn()
		{
			return new Op1nnn(CurrentOp);
		}

		///// <summary>
		///// CALL. Call subroutine at nnn
		///// </summary>
		private BaseOp Op_2nnn()
		{
			return new Op2nnn(CurrentOp);
		}

		///// <summary>
		///// SE Vx, byte. Skip next instruction if Vx == kk
		///// </summary>
		//private void Op_3xkk()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte kk = Convert.ToByte(Opcode & 0x00FFu);

		//	if (VRegisters[Vx] == kk)
		//		Pc += 2;
		//}

		///// <summary>
		///// SNE Vx, byte. Skip next instruction if Vx != kk
		///// </summary>
		//private void Op_4xkk()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte kk = Convert.ToByte(Opcode & 0x00FFu);

		//	if (VRegisters[Vx] != kk)
		//		Pc += 2;
		//}

		///// <summary>
		///// SE Vx, Vy. Skip next instruction if Vx = Vy
		///// </summary>
		//private void Op_5xy0()
		//{
		//	byte firstByte = Convert.ToByte(Opcode & 0x000Fu);
		//	if (firstByte != 0)
		//		throw new ArgumentException($"Such function not found: {Opcode}");

		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	if (VRegisters[Vx] == VRegisters[Vy])
		//		Pc += 2;
		//}

		///// <summary>
		///// LD Vx, byte. Set Vx = kk
		///// </summary>
		//private void Op_6xkk()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte kk = Convert.ToByte(Opcode & 0x00FFu);

		//	VRegisters[Vx] = kk;
		//}

		///// <summary>
		///// ADD Vx, byte. Set Vx += kk.
		///// </summary>
		//private void Op_7xkk()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte kk = Convert.ToByte(Opcode & 0x00FFu);

		//	VRegisters[Vx] += kk;
		//}

		///// <summary>
		///// LD Vx, Vy. Set Vx = Vy
		///// </summary>
		//private void Op_8xy0()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	VRegisters[Vx] = VRegisters[Vy];
		//}

		///// <summary>
		///// OR Vx, Vy. Set Vx = Vx OR Vy.
		///// </summary>
		//private void Op_8xy1()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	VRegisters[Vx] |= VRegisters[Vy];
		//}

		///// <summary>
		///// AND Vx, Vy. Set Vx = Vx AND Vy.
		///// </summary>
		//private void Op_8xy2()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	VRegisters[Vx] &= VRegisters[Vy];
		//}

		///// <summary>
		///// XOR Vx, Vy. Set Vx = Vx XOR Vy.
		///// </summary>
		//private void Op_8xy3()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	VRegisters[Vx] ^= VRegisters[Vy];
		//}

		///// <summary>
		///// ADD Vx, Vy. Set Vx += Vy, Vf=Carry.
		///// </summary>
		//private void Op_8xy4()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	ushort sum = Convert.ToUInt16(VRegisters[Vx] + VRegisters[Vy]);
		//	if (sum > 255)
		//		VRegisters[0xF] = 1;
		//	else
		//		VRegisters[0xF] = 0;

		//	VRegisters[Vx] = Convert.ToByte(sum & 0xFF);
		//}

		///// <summary>
		///// SUB Vx, Vy. Set Vx = Vx - Vy, set VF = NOT borrow.
		///// </summary>
		//private void Op_8xy5()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	if (VRegisters[Vx] > VRegisters[Vy])
		//		VRegisters[0xF] = 1;
		//	else
		//		VRegisters[0xF] = 0;

		//	VRegisters[Vx] -= VRegisters[Vy];
		//}

		///// <summary>
		///// SHR Vx, Vy. Set Vx = Vx SHR 1.
		///// </summary>
		//private void Op_8xy6()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00) >> 8);

		//	// Save LSB in VF
		//	VRegisters[0xF] = Convert.ToByte(VRegisters[Vx] & 0x1);

		//	VRegisters[Vx] >>= 1;
		//}

		///// <summary>
		///// SUBN Vx, Vy. Set Vx = Vy - Vx, set VF = NOT borrow.
		///// </summary>
		//private void Op_8xy7()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	if (VRegisters[Vy] > VRegisters[Vx])
		//		VRegisters[0xF] = 1;
		//	else
		//		VRegisters[0xF] = 0;

		//	VRegisters[Vx] = (byte)(VRegisters[Vy] - VRegisters[Vx]);
		//}

		///// <summary>
		///// SHL Vx, Vy. Set Vx = Vx SHL 1.
		///// </summary>
		//private void Op_8xyE()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00) >> 8);

		//	// Save MSB in VF
		//	VRegisters[0xF] = Convert.ToByte((VRegisters[Vx] & 0x80) >> 7);

		//	VRegisters[Vx] <<= 1;
		//}

		///// <summary>
		///// SNE Vx, Vy. Skip next instruction if Vx != Vy
		///// </summary>
		//private void Op_9xy0()
		//{
		//	byte firstByte = Convert.ToByte(Opcode & 0x000Fu);
		//	if (firstByte != 0)
		//		throw new ArgumentException($"Such function not found: {Opcode}");

		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);

		//	if (VRegisters[Vx] != VRegisters[Vy])
		//		Pc += 2;
		//}

		///// <summary>
		///// LD I,addr. Set I = nnn
		///// </summary>
		//private void Op_Annn()
		//{
		//	var address = Convert.ToUInt16(Opcode & 0x0FFFu);

		//	IndexRegister = address;
		//}

		///// <summary>
		///// JP V0, addr. Jump to location nnn + V0.
		///// </summary>
		//private void Op_Bnnn()
		//{
		//	var address = Convert.ToUInt16(Opcode & 0x0FFFu);

		//	Pc = Convert.ToUInt16(VRegisters[0] + address);
		//}

		///// <summary>
		///// RND Vx, byte. Set Vx = random byte AND kk
		///// </summary>
		//private void Op_Cxkk()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00) >> 8);
		//	byte kk = Convert.ToByte(Opcode & 0x00FF);

		//	VRegisters[Vx] = Convert.ToByte(new Random().Next(0, 0xFF) & kk);
		//}

		///// <summary>
		///// DRW Vx, Vy, nibble. Display n-byte sprite starting at memory location I at (Vx, Vy), set VF = collision.
		///// </summary>
		//private void Op_Dxyn()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte Vy = Convert.ToByte((Opcode & 0x00F0u) >> 4);
		//	byte height = Convert.ToByte(Opcode & 0x000F);

		//	// Wrap if going beyond screen boundaries
		//	byte xPos = Convert.ToByte(VRegisters[Vx] % VideoWidth);
		//	byte yPos = Convert.ToByte(VRegisters[Vy] % VideoHeight);

		//	VRegisters[0xF] = 0;

		//	for (byte row = 0; row < height; row++)
		//	{
		//		byte spriteByte = memory.GetByte((ushort)(IndexRegister + row));

		//		for (byte col = 0; col < SpriteColumns; col++)
		//		{
		//			bool spritePixel = Convert.ToBoolean(spriteByte & (0x80u >> col));
		//			var screenXPos = Convert.ToByte((xPos + col) % VideoWidth);
		//			var screenYPos = Convert.ToByte((yPos + row) % VideoHeight);
		//			bool isScreenPixelOn = VideoPixels[screenXPos, screenYPos];
		//			if (spritePixel)
		//			{
		//				if (isScreenPixelOn)
		//					VRegisters[0xF] = 1;

		//				//Switch pixel
		//				VideoPixels[xPos + col, yPos + row] = !VideoPixels[xPos + col, yPos + row];
		//			}
		//		}
		//	}
		//}

		///// <summary>
		///// SKP Vx. Skip next instruction if key with the value of Vx is pressed.
		///// </summary>
		//private void Op_Ex9E()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

		//	byte key = VRegisters[Vx];

		//	if (KeyState[key])
		//		Pc += 2;
		//}

		///// <summary>
		/////  SKNP Vx. Skip next instruction if key with the value of Vx is not pressed.
		///// </summary>
		//private void Op_ExA1()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

		//	byte key = VRegisters[Vx];

		//	if (!KeyState[key])
		//		Pc += 2;
		//}

		///// <summary>
		///// LD Vx, DT. Set Vx = delay timer value
		///// </summary>
		//private void Op_Fx07()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

		//	VRegisters[Vx] = DelayTimer;
		//}

		///// <summary>
		///// LD Vx, K. Wait for a key press, store the value of the key in Vx.
		///// </summary>
		//private void Op_Fx0A()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

		//	for (int i = 0; i < KeyState.Length; i++)
		//	{
		//		if (KeyState[i])
		//		{
		//			VRegisters[Vx] = Convert.ToByte(i);
		//			return;
		//		}
		//	}
		//	//Else wait
		//	Pc -= 2;
		//}

		///// <summary>
		///// LD DT, Vx. Set delay timer = Vx
		///// </summary>
		//private void Op_Fx15()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

		//	DelayTimer = VRegisters[Vx];
		//}

		///// <summary>
		///// LD ST, Vx. Set sound timer = Vx
		///// </summary>
		//private void Op_Fx18()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

		//	SoundTimer = VRegisters[Vx];
		//}

		///// <summary>
		///// ADD I, Vx. Set I = I + Vx.
		///// </summary>
		//private void Op_Fx1E()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);

		//	IndexRegister += VRegisters[Vx];
		//}

		///// <summary>
		///// LD F, Vx. Set I = location of sprite for digit Vx.
		///// </summary>
		//private void Op_Fx29()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte value = VRegisters[Vx];

		//	IndexRegister = Convert.ToUInt16(Memory.FonsetStartAddress + (Memory.LetterSize * value));
		//}

		///// <summary>
		///// LD B, Vx. Store BCD representation of Vx in memory locations I, I+1, and I+2.
		///// </summary>
		//private void Op_Fx33()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	byte value = VRegisters[Vx];

		//	// Ones
		//	memory.SetByte((ushort)(IndexRegister + 2), (byte)(value % 10));
		//	value /= 10;

		//	// Tens
		//	memory.SetByte((ushort)(IndexRegister + 1), (byte)(value % 10));
		//	value /= 10;

		//	// Hundreds
		//	memory.SetByte((IndexRegister), (byte)(value % 10));
		//}

		///// <summary>
		///// LD [I], Vx. Store registers V0 through Vx in memory starting at location I.
		///// </summary>
		//private void Op_Fx55()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	for (byte i = 0; i <= Vx; i++)
		//	{
		//		memory.SetByte((ushort)(IndexRegister + i), VRegisters[i]);
		//	}
		//}

		///// <summary>
		///// LD Vx, [I]. Read registers V0 through Vx from memory starting at location I.
		///// </summary>
		//private void Op_Fx65()
		//{
		//	byte Vx = Convert.ToByte((Opcode & 0x0F00u) >> 8);
		//	for (byte i = 0; i <= Vx; i++)
		//	{
		//		VRegisters[i] = memory.GetByte((ushort)(IndexRegister + i));
		//	}
		//}

		#endregion Functions
	}
}