# version 440

layout(location = 0) in vec3 Pos;

out vec3 texture;

uniform mat4 WorldMat;

void main()
{
	gl_Position = WorldMat * vec4(Pos, 1.0); 
	texture = Pos;
}