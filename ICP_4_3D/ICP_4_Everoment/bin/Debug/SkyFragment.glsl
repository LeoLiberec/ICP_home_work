# version 440
# extension GL_NV_shadow_samplers_cube : enable

in vec3 texture;

out vec4 Col;

uniform samplerCube tex;

void main()
{
	Col = textureCube(tex, texture);
}
