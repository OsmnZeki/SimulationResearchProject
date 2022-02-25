#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoord;

out vec3 fragPos;
out vec3 normal;
out vec2 texCoord;

out vec3 nearPoint;
out vec3 farPoint;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;



void main(){
    
    fragPos = aPos.xyz;
    vec3 p = aPos.xyz;
    nearPoint = vec3(p.x,p.y,0.1);
    farPoint = vec3(p.x,p.y,100.0);
    
    gl_Position = vec4(aPos,1.0f);
}