using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using ObjectTK.Textures;
using ObjectTK.Shaders;
using ObjectTK.Buffers;

using ComputeShaderTemplate.Shaders;

namespace ComputeShaderTemplate
{
    public class Simulation
    {
        RenderProgram _renderProgram;
        AgentShader _agentShader;
        Texture2D _texture;
        Agent[] _agents;
        VertexArray _screen;

        public void Load()
        {
            _texture = new Texture2D(SizedInternalFormat.Rgba8, 100, 100);
            _texture.SetFilter(TextureMinFilter.Nearest, TextureMagFilter.Nearest);
            _texture.Bind(TextureUnit.Texture0);

            _agents = Agent.Create(10, new Vector2(50,50), 10);
            _agentShader = AgentShader.Create(_agents, 100, 100, _texture);

            _renderProgram = RenderProgram.Create();

            Vector3[] vertices = new Vector3[] { new Vector3(-1, -1, 0), new Vector3(-1, 1, 0), new Vector3(1, -1, 0), new Vector3(1, 1, 0) };
            Buffer<Vector3> vertexBuffer = new Buffer<Vector3>();
            vertexBuffer.Init(BufferTarget.ArrayBuffer, vertices);

            Vector2[] uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };
            Buffer<Vector2> uvBuffer = new Buffer<Vector2>();
            uvBuffer.Init(BufferTarget.ArrayBuffer, uv);

            Buffer<int> indexBuffer = new Buffer<int>();
            indexBuffer.Init(BufferTarget.ElementArrayBuffer, new int[] { 0, 1, 2, 3, 2, 1 });


            _screen = new VertexArray();
            _screen.Bind();
            _screen.BindAttribute(_renderProgram.InPosition, vertexBuffer);
            _screen.BindAttribute(_renderProgram.InUV, uvBuffer);
            _screen.BindElementBuffer(indexBuffer);

        }

        public void Render()
        {
            _agentShader.Use();
            _agentShader.DispatchInvocations(1);

            _renderProgram.Use();
            _screen.DrawElements(PrimitiveType.Triangles, 6);

        }

        public void Update()
        {

        }
    }
}
