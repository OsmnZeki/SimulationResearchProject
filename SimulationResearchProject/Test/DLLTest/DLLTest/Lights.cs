﻿using System.Numerics;

namespace DLLTest
{
    public class Lights
    {
        public struct PointLight {

	        public Vector3 position;

            // attenuation constants
            public float k0;
            public float k1;
            public float k2;
	
            public Vector4 ambient;
            public Vector4 diffuse;
            public Vector4 specular;

            void Render(Shader shader, int idx)
            {
                	string name = "pointLight[" + idx.ToString() + "]";
                
                	shader.Set3Float(name + ".position", position);
                	shader.SetFloat(name + ".k0", k0);
                	shader.SetFloat(name + ".k1", k1);
                	shader.SetFloat(name + ".k2", k2);
                	shader.Set4Float(name +  ".ambient", ambient);
                	shader.Set4Float(name + ".diffuse", diffuse);
                	shader.Set4Float(name + ".specular", specular);
            }
        };
        
        public struct DirectionalLight {
	
	        public Vector3 direction;

	        public Vector4 ambient;
	        public Vector4 diffuse;
	        public Vector4 specular;

	        void Render(Shader shader)
	        {
		        string name = "directionalLight";

		        shader.Set3Float(name + ".direction", direction);
		        shader.Set4Float(name + ".ambient", ambient);
		        shader.Set4Float(name + ".diffuse", diffuse);
		        shader.Set4Float(name + ".specular", specular);
	        }
        };


        public struct SpotLight {

	        public Vector3 position;
	        public Vector3 direction;
	        public float cutOff;
	        public float outerCutOff;

	        // attenuation constants
	        public float k0;
	        public float k1;
	        public float k2;

	        public Vector4 ambient;
	        public Vector4 diffuse;
	        public Vector4 specular;

	        void Render(Shader shader, int idx)
	        {
		        string name = "spotLight[" + idx.ToString() + "]";

		        shader.Set3Float(name + ".position", position);
		        shader.Set3Float(name + ".direction", direction);

		        shader.SetFloat(name + ".k0", k0);
		        shader.SetFloat(name + ".k1", k1);
		        shader.SetFloat(name + ".k2", k2);
		        shader.SetFloat(name + ".cutOff", cutOff);
		        shader.SetFloat(name + ".outerCutOff", outerCutOff);

		        shader.Set4Float(name + ".ambient", ambient);
		        shader.Set4Float(name + ".diffuse", diffuse);
		        shader.Set4Float(name + ".specular", specular);
	        }
        };

    }
}