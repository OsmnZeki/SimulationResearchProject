#version 330 core

struct Material{
	vec4 diffuse;
	vec4 specular;
	float shininess;
};

out vec4 FragColor;

uniform vec3 lightColor;

void main(){
	FragColor = vec4(lightColor,1.0f);

}