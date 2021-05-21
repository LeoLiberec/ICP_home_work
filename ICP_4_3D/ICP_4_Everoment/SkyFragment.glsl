# version 440

in vec3 texture;

out vec4 Col;

uniform samplerCube tex;

void main()
{
	Col = textureCube(tex, texture);
}