//
//  CharacterGlyph.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using MBS.Framework.Drawing;

namespace MBS.Framework.Rendering
{
	public struct CharacterGlyph
	{
		public uint TextureID;  // ID handle of the glyph texture
		public Dimension2D Size;       // Size of glyph
		public Vector2D Bearing;    // Offset from baseline to left/top of glyph
		public uint Advance;    // Offset to advance to next glyph
	}
}
