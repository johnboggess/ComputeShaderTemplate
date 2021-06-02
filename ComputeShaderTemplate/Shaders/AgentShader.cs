using System;
using System.Collections.Generic;
using System.Text;
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
        public Uniform<int> Width { get; set; }
        public Uniform<int> Height { get; set; }
        public ShaderStorage Agents { get; set; }

        internal Buffer<Agent> AgentBuffer; 

        public static AgentShader Create(Agent[] agents, int width, int height, Texture2D texture)
        {
            AgentShader agentShader = ProgramFactory.Create<AgentShader>();
            agentShader.Use();
            agentShader.Width.Set(width);
            agentShader.Height.Set(height);
            agentShader.Texture.Bind(0, texture, TextureAccess.WriteOnly);

            agentShader.AgentBuffer = new Buffer<Agent>();
            agentShader.AgentBuffer.Init(BufferTarget.ArrayBuffer, agents);

            agentShader.Agents.BindBuffer(agentShader.AgentBuffer);

            return agentShader;
        }
    }
}
