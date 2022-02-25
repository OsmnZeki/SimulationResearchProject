#version 330 core
out vec4 outColor;

in vec3 nearPoint;
in vec3 farPoint;

in vec3 fragPos;

void main() {

    float t = -nearPoint.y / (farPoint.y - nearPoint.y);
    vec3 R = nearPoint + t * (farPoint-nearPoint);

    float c = (
        int(round(R.x * 5.0)) +
        int(round(R.y * 5.0))
    ) % 2;

    outColor = vec4(vec3(c/2.0 + 0.3), 1);

    outColor = outColor * float(t>0);

}