# version 440

layout(location = 0) in vec3 Pos;
layout(location = 1) in vec3 Col;
layout(location = 2) in vec2 Tex;

out vec3 color;
out vec2 texture;

void main()
{
color = Col;
texture = Tex;

gl_Position = vec4(Pos, 1); 
}