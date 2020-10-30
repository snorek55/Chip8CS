using System;

namespace Core.Opcodes
{
	public class OpDxyn : BaseOp
	{
		public byte Vx { get; set; }
		public byte Vy { get; set; }
		public byte Height { get; set; }

		public OpDxyn(ushort op) : base(op)
		{
			Vx = Convert.ToByte((op & 0x0F00u) >> 8);
			Vy = Convert.ToByte(op & 0x00FFu);
			Height = Convert.ToByte(op & 0x000F);
		}

		public override string ToString()
		{
			return $"DRW V[{Vx.ToString(ByteFormat)}], V[{Vy.ToString(ByteFormat)}], {Height.ToString(ByteFormat)}";
		}

		public override void Execute(Cpu cpu)
		{
			// Wrap if going beyond screen boundaries
			byte xPos = Convert.ToByte(cpu.VRegisters[Vx] % Cpu.VideoWidth);
			byte yPos = Convert.ToByte(cpu.VRegisters[Vy] % Cpu.VideoHeight);

			cpu.VRegisters[0xF] = 0;

			for (byte row = 0; row < Height; row++)
			{
				byte spriteByte = cpu.Memory.GetByte((ushort)(cpu.IndexRegister + row));

				for (byte col = 0; col < Cpu.SpriteColumns; col++)
				{
					bool spritePixel = Convert.ToBoolean(spriteByte & (0x80u >> col));
					var screenXPos = Convert.ToByte((xPos + col) % Cpu.VideoWidth);
					var screenYPos = Convert.ToByte((yPos + row) % Cpu.VideoHeight);
					bool isScreenPixelOn = cpu.VideoPixels[screenXPos, screenYPos];
					if (spritePixel)
					{
						if (isScreenPixelOn)
							cpu.VRegisters[0xF] = 1;

						//Switch pixel
						cpu.VideoPixels[xPos + col, yPos + row] = !cpu.VideoPixels[xPos + col, yPos + row];
					}
				}
			}
		}
	}
}