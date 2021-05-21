# version 440

in vec3 color;
in vec2 texture;

out vec4 Col;

uniform sampler2D tex;

void main()
{
	Col = texture2D(tex, texture);
}