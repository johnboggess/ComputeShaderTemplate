-- Agent
#version 430

const float pi = 3.141592654;

layout (binding=0, rgba8) uniform image2D Texture;
uniform int Width;
uniform int Height;

struct Agent
{
	vec2 Position;
	vec2 Velocity;
};

layout(std430, binding = 1) buffer Agents
{
    Agent agents[];
};

layout (local_size_x = $AgentCount$, local_size_y = 1) in;
void main()
{
	Agent agent = agents[gl_GlobalInvocationID.x];

	agent.Position += agent.Velocity;
	imageStore(Texture, ivec2(agent.Position), vec4(1,1,1,1));

	agents[gl_GlobalInvocationID.x] = agent;
}