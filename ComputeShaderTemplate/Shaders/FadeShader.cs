using System;
using System.Collections.Generic;
using System.Text;

using ObjectTK;
using ObjectTK.Buffers;
using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ComputeShaderTemplate.Shaders
{
    [ComputeShaderSource("FadeShader.Fade")]
    class FadeShader : ComputeProgram
    {
        public ImageUniform Texture { get; set; }

        public static FadeShader Create(Vector2i localGroupSize , Texture2D texture)
        {
            ShaderWriter shaderWriter = new ShaderWriter();
            shaderWriter.Load("Data\\Shaders\\FadeShader.glsl");
            shaderWriter.Write("SizeX", localGroupSize.X);
            shaderWriter.Write("SizeY", localGroupSize.Y);
            shaderWriter.Save("Data\\Shaders\\FadeShader.glsl");


            FadeShader fadeShader = ProgramFactory.Create<FadeShader>();
            fadeShader.Use();
            fadeShader.Texture.Bind(0, texture, TextureAccess.WriteOnly);
            return fadeShader;
        }
    }
}
