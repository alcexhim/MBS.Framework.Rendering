//
//  VertexArray.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
namespace MBS.Framework.Rendering
{
	public abstract class VertexArray
	{
		protected abstract void BindInternal();
		public void Bind()
		{
			BindInternal();
		}
		protected abstract void UnbindInternal();
		public void Unbind()
		{
			UnbindInternal();
		}

		protected abstract void EnableInternal();
		public void Enable()
		{
			EnableInternal();
		}

		protected abstract void SetVertexAttributePointerInternal(int size, ElementType type, bool normalized, int stride, object pointer);
		/// <summary>
		/// Sets the vertex attribute pointer.
		/// </summary>
		/// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA is accepted by glVertexAttribPointer. The initial value is 4.</param>
		/// <param name="type">Specifies the data type of each component in the array. The symbolic constants GL_BYTE, GL_UNSIGNED_BYTE, GL_SHORT, GL_UNSIGNED_SHORT, GL_INT, and GL_UNSIGNED_INT are accepted by glVertexAttribPointer and glVertexAttribIPointer. Additionally GL_HALF_FLOAT, GL_FLOAT, GL_DOUBLE, GL_FIXED, GL_INT_2_10_10_10_REV, GL_UNSIGNED_INT_2_10_10_10_REV and GL_UNSIGNED_INT_10F_11F_11F_REV are accepted by glVertexAttribPointer. GL_DOUBLE is also accepted by glVertexAttribLPointer and is the only token accepted by the type parameter for that function. The initial value is GL_FLOAT.</param>
		/// <param name="normalized">If set to <c>true</c>, specifies that fixed-point data values should be normalized; otherwise, converted directly as fixed-point values when they are accessed.</param>
		/// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. The initial value is 0.</param>
		/// <param name="pointer">Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER target. The initial value is 0.</param>
		public void SetVertexAttributePointer(int size = 4, ElementType type = ElementType.Float, bool normalized = false, int stride = 0, object pointer = null)
		{
			if (size < 1 || size > 4)
			{
				throw new ArgumentException("must be one of 1, 2, 3, or 4", nameof(size));
			}
			SetVertexAttributePointerInternal(size, type, normalized, stride, pointer);
		}
		public void SetVertexAttributePointer<T>(int size, bool normalized = false, int stride = 0, T[] pointer = default(T[]))
		{
			if (typeof(T) == typeof(sbyte))
			{
				SetVertexAttributePointer(size, ElementType.Byte, normalized, stride, pointer);
			}
			else if (typeof(T) == typeof(byte))
			{
				SetVertexAttributePointer(size, ElementType.UnsignedByte, normalized, stride, pointer);
			}
			else if (typeof(T) == typeof(short))
			{
				SetVertexAttributePointer(size, ElementType.Short, normalized, stride, pointer);
			}
			else if (typeof(T) == typeof(ushort))
			{
				SetVertexAttributePointer(size, ElementType.UnsignedShort, normalized, stride, pointer);
			}
			else if (typeof(T) == typeof(int))
			{
				SetVertexAttributePointer(size, ElementType.Int, normalized, stride, pointer);
			}
			else if (typeof(T) == typeof(uint))
			{
				SetVertexAttributePointer(size, ElementType.UnsignedInt, normalized, stride, pointer);
			}

			else if (typeof(T) == typeof(float))
			{
				SetVertexAttributePointer(size, ElementType.Float, normalized, stride, pointer);
			}
			else if (typeof(T) == typeof(double))
			{
				SetVertexAttributePointer(size, ElementType.Double, normalized, stride, pointer);
			}
		}
	}
}
