# version 440

in vec3 color;
in vec2 texture;

out vec4 Col;

void main()
{
Col = vec4(color, 1);
}