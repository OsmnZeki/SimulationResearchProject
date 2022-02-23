#version 330 core

out vec4 FragColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform int noTex;
uniform int transparent;
uniform vec3 color;

void main(){

	vec4 currentColor;

	if(noTex==1){
		currentColor = vec4(color,1.0f);
	}
	else{
		currentColor = texture(texture0,texCoord);
	}

	if(transparent != 1) currentColor.a = 1.0f;

	FragColor = currentColor;

}