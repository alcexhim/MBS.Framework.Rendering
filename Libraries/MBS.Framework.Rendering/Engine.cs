//
//  Engine.cs
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
using System.Collections.Generic;

namespace MBS.Framework.Rendering
{
	public abstract class Engine
	{
		protected abstract Canvas CreateCanvasInternal();
		public Canvas CreateCanvas()
		{
			return CreateCanvasInternal();
		}

		private static Engine[] _Engines = null;
		public static Engine[] Get()
		{
			if (_Engines == null)
			{
				List<Engine> list = new List<Engine>();
				Type[] types = Reflection.GetAvailableTypes(new Type[] { typeof(Engine) });
				for (int i = 0; i < types.Length; i++)
				{
					Engine engine = (Engine)types[i].Assembly.CreateInstance(types[i].FullName);
					list.Add(engine);
				}
				_Engines = list.ToArray();
			}
			return _Engines;
		}

		protected abstract void LoadShaderInternal(Shader shader, string code);
		internal void LoadShader(Shader shader, string code)
		{
			LoadShaderInternal(shader, code);
		}

		protected abstract void CompileShaderInternal(Shader shader);
		internal void CompileShader(Shader shader)
		{
			CompileShaderInternal(shader);
		}

		internal void SetProgramUniform(ShaderProgram program, string name, bool value)
		{
			SetProgramUniform(program, name, (value ? 1 : 0));
		}
		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, int value);
		internal void SetProgramUniform(ShaderProgram program, string name, int value)
		{
			SetProgramUniformInternal(program, name, value);
		}

		protected abstract uint GetProgramAttributeLocationInternal(ShaderProgram program, string name);
		internal uint GetProgramAttributeLocation(ShaderProgram program, string name)
		{
			return GetProgramAttributeLocationInternal(program, name);
		}

		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, float value);

		internal void SetProgramUniformMatrix(ShaderProgram program, string name, int count, bool transpose, float[] value)
		{
			SetProgramUniformMatrixInternal(program, name, count, transpose, value);
		}
		protected abstract void SetProgramUniformMatrixInternal(ShaderProgram program, string name, int count, bool transpose, float[] value);

		protected abstract void DeleteShaderInternal(Shader shader);
		internal void DeleteShader(Shader shader)
		{
			DeleteShaderInternal(shader);
		}

		internal void SetProgramUniform(ShaderProgram program, string name, float value)
		{
			SetProgramUniformInternal(program, name, value);
		}
		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, float value1, float value2);

		protected abstract VertexArray[] CreateVertexArraysInternal(int count);
		public VertexArray[] CreateVertexArrays(int count)
		{
			return CreateVertexArraysInternal(count);
		}
		public VertexArray CreateVertexArray()
		{
			return CreateVertexArrays(1)[0];
		}
		protected abstract void DeleteVertexArraysInternal(VertexArray[] arrays);
		public void DeleteVertexArrays(VertexArray[] arrays)
		{
			DeleteVertexArraysInternal(arrays);
		}
		public void DeleteVertexArray(VertexArray array)
		{
			DeleteVertexArrays(new VertexArray[] { array });
		}

		internal void SetProgramUniform(ShaderProgram program, string name, float value1, float value2)
		{
			SetProgramUniformInternal(program, name, value1, value2);
		}
		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, float value1, float value2, float value3);
		internal void SetProgramUniform(ShaderProgram program, string name, float value1, float value2, float value3)
		{
			SetProgramUniformInternal(program, name, value1, value2, value3);
		}
		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, double value1, double value2, double value3);
		internal void SetProgramUniform(ShaderProgram program, string name, double value1, double value2, double value3)
		{
			SetProgramUniformInternal(program, name, value1, value2, value3);
		}

		protected abstract void LinkProgramInternal(ShaderProgram program);
		internal void LinkProgram(ShaderProgram program)
		{
			LinkProgramInternal(program);
		}

		protected abstract RenderBuffer[] CreateBuffersInternal(int count);
		public RenderBuffer[] CreateBuffers(int count)
		{
			return CreateBuffersInternal(count);
		}
		public RenderBuffer CreateBuffer()
		{
			RenderBuffer[] buffers = CreateBuffers(1);
			return buffers[0];
		}

		protected abstract void AttachShaderToProgramInternal(ShaderProgram program, Shader shader);
		internal void AttachShaderToProgram(ShaderProgram program, Shader shader)
		{
			AttachShaderToProgramInternal(program, shader);
		}

		protected abstract void BindTextureInternal(TextureTarget target, uint id);

		protected abstract void SetTextureParameterInternal(TextureParameterTarget target, TextureParameterName name, float value);
		public void SetTextureParameter(TextureParameterTarget target, TextureParameterName name, float value)
		{
			SetTextureParameterInternal(target, name, value);
		}
		public void SetTextureParameter(TextureParameterTarget target, TextureParameterName name, TextureWrap value)
		{
			float value_f = TranslateValue(value);
			SetTextureParameterInternal(target, name, value_f);
		}
		public void SetTextureParameter(TextureParameterTarget target, TextureParameterName name, TextureFilter value)
		{
			float value_f = TranslateValue(value);
			SetTextureParameterInternal(target, name, value_f);
		}

		protected abstract float TranslateValueInternal(TextureWrap value);
		private float TranslateValue(TextureWrap value)
		{
			return TranslateValueInternal(value);
		}
		protected abstract float TranslateValueInternal(TextureFilter value);
		private float TranslateValue(TextureFilter value)
		{
			return TranslateValueInternal(value);
		}

		public void SetTextureParameter(TextureParameterTarget target, TextureParameterName name, int value)
		{
			SetTextureParameter(target, name, (float)value);
		}

		protected abstract void CreateShaderProgramInternal(ShaderProgram program);
		public ShaderProgram CreateShaderProgram()
		{
			ShaderProgram program = new ShaderProgram(this);
			CreateShaderProgramInternal(program);
			return program;
		}

		public void BindTexture(TextureTarget target, uint id)
		{
			BindTextureInternal(target, id);
		}

		public static Engine GetDefault()
		{
			Engine[] engines = Get();
			if (engines.Length == 0)
				return null;

			return engines[0];
		}

		protected abstract uint[] GenerateTextureIDsInternal(int count);
		public uint[] GenerateTextureIDs(int count)
		{
			return GenerateTextureIDsInternal(count);
		}
		public uint GenerateTextureID()
		{
			return GenerateTextureIDs(1)[0];
		}

		protected abstract void UseProgramInternal(ShaderProgram program);
		internal void UseProgram(ShaderProgram program)
		{
			UseProgramInternal(program);
		}

		protected abstract void CreateShaderInternal(Shader shader);
		internal void CreateShader(Shader shader)
		{
			CreateShaderInternal(shader);
		}

		public Shader CreateShaderFromResource(ShaderType type, Type resourceType, string resourceName)
		{
			System.IO.Stream st = resourceType.Assembly.GetManifestResourceStream(resourceName);
			System.IO.StreamReader sr = new System.IO.StreamReader(st);
			string text = sr.ReadToEnd();
			return CreateShaderFromString(type, text);
		}
		public Shader CreateShaderFromString(ShaderType type, string code)
		{
			Shader shader = new Shader(this, type);
			shader.LoadCode(code);
			return shader;
		}

		public Shader CreateShaderFromFile(ShaderType type, string fileName)
		{
			Shader shader = new Shader(this, type);
			shader.LoadFile(fileName);
			return shader;
		}

		protected abstract void FlushInternal();
		public void Flush()
		{
			FlushInternal();
		}

		/// <summary>
		/// Sets the two-dimensional texture image.
		/// </summary>
		/// <param name="target">Specifies the target texture.</param>
		/// <param name="level">Specifies the level-of-detail number.  Level 0 is the base image level. Level n is the nth mipmap reduction image. If target is GL_TEXTURE_RECTANGLE or GL_PROXY_TEXTURE_RECTANGLE, level must be 0.</param>
		/// <param name="internalFormat">Specifies the number of color components in the texture. Must be one of base internal formats given in Table 1, one of the sized internal formats given in Table 2, or one of the compressed internal formats given in Table 3, below.</param>
		/// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
		/// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array, in the case of the GL_TEXTURE_1D_ARRAY and GL_PROXY_TEXTURE_1D_ARRAY targets. All implementations support 2D texture images that are at least 1024 texels high, and texture arrays that are at least 256 layers deep.</param>
		/// <param name="border">This value must be 0.</param>
		/// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: GL_RED, GL_RG, GL_RGB, GL_BGR, GL_RGBA, GL_BGRA, GL_RED_INTEGER, GL_RG_INTEGER, GL_RGB_INTEGER, GL_BGR_INTEGER, GL_RGBA_INTEGER, GL_BGRA_INTEGER, GL_STENCIL_INDEX, GL_DEPTH_COMPONENT, GL_DEPTH_STENCIL.</param>
		/// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_HALF_FLOAT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, and GL_UNSIGNED_INT_2_10_10_10_REV.</param>
		/// <param name="data">Specifies a pointer to the image data in memory.</param>
		public void SetTextureImage(TextureTarget target, int level, TextureFormat internalFormat, int width, int height, int border, TextureFormat format, ElementType type, byte[] data)
		{
			throw new NotImplementedException();
		}
	}
}
