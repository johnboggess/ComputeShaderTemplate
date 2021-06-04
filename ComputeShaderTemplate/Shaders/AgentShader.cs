using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ObjectTK.Buffers;
using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;

using OpenTK.Graphics.OpenGL4;

namespace ComputeShaderTemplate.Shaders
{
    [ComputeShaderSource("AgentShader.Agent")]
    public class AgentShader : ComputeProgram
    {
        public ImageUniform Texture { get; set; }
        public ShaderStorage Agents { get; set; }

        internal Buffer<Agent> AgentBuffer; 

        public static AgentShader Create(Agent[] agents, Texture2D texture)
        {
            ShaderWriter shaderWriter = new ShaderWriter();
            shaderWriter.Load("Data\\Shaders\\AgentShader.glsl");
            shaderWriter.Write("AgentCount", agents.Length);
            shaderWriter.Save("Data\\Shaders\\AgentShader.glsl");

            AgentShader agentShader = ProgramFactory.Create<AgentShader>();
            agentShader.Use();
            agentShader.Texture.Bind(0, texture, TextureAccess.ReadWrite);

            agentShader.AgentBuffer = new Buffer<Agent>();
            agentShader.AgentBuffer.Init(BufferTarget.ArrayBuffer, agents);

            agentShader.Agents.BindBuffer(agentShader.AgentBuffer);

            return agentShader;
        }
    }
}
