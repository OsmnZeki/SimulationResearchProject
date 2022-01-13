using System;
using System.Collections.Generic;
using System.Numerics;
using MESPSimulation.DLL;
using MESPSimulation.Graphics.Rendering;

namespace MESPSimulation.Graphics.Model
{
    public class Mesh
    {
        public enum MeshSetupConfiguration {
            PosSetup = 0,
            PosNormalSetup = 1,
            PosNormalTexCoordSetup = 2
        };

        public IntPtr meshAdress;
        
        Vector3[] pos;
        Vector3[] normal;
        Vector2[] texCoord;
        private int[] indices;
        
        Vector4 diffuse;
        Vector4 specular;

        private List<Texture> textures;
        private bool noTex;

        public Mesh()
        {
            noTex = true;
            meshAdress = RenderProgramDLL.CreateMesh();
        }

        public void Setup(MeshSetupConfiguration setupConfiguration)
        {
            RenderProgramDLL.MeshSetup(meshAdress,(int)setupConfiguration);
        }

        public void CleanUp()
        {
            RenderProgramDLL.MeshCleanUp(meshAdress);
        }

        public void SetVerticesPos(Vector3[] pos)
        {
            this.pos = pos;

            float[] posF = new float[pos.Length * 3];
            int v = 0;
            
            for (int i = 0; i < pos.Length; i++)
            {
                posF[v++] = pos[i].X;
                posF[v++] = pos[i].Y;
                posF[v++] = pos[i].Z;
            }
            
            RenderProgramDLL.MeshSetVerticesPos(meshAdress,posF,pos.Length);
        }
        
        public void SetVerticesNormal( Vector3[] normal)
        {
            if (pos.Length != normal.Length)
            {
                Console.WriteLine("The vertices and normals sizes of mesh must be equal!");
                Console.WriteLine("Vertices lenght: " + pos.Length + "\nNormal Lenght: " + normal.Length);
                return;
            }
            
            this.normal = normal;
            
            float[] posF = new float[pos.Length * 3];
            int v = 0;
            
            for (int i = 0; i < normal.Length; i++)
            {
                posF[v++] = normal[i].X;
                posF[v++] = normal[i].Y;
                posF[v++] = normal[i].Z;
            }
            
            RenderProgramDLL.MeshSetVerticesNormal(meshAdress,posF);
        }
        
        public void SetVerticesTexCoordl(Vector2[] texCoord)
        {
            this.texCoord = texCoord;
                
            float[] posF = new float[pos.Length * 2];
            int v = 0;
            
            for (int i = 0; i < texCoord.Length; i++)
            {
                posF[v++] = texCoord[i].X;
                posF[v++] = texCoord[i].Y;
            }
            
            RenderProgramDLL.MeshSetVerticesTexCoord(meshAdress,posF);
        }

        public void SetIndices(int[] indices)
        {
            if (pos.Length != indices.Length)
            {
                Console.WriteLine("The vertices and indices sizes of mesh must be equal!");
                Console.WriteLine("Vertices lenght: " + pos.Length + "\nIndices Lenght: " + indices.Length);
                return;
            }
            
            this.indices = indices;
            RenderProgramDLL.MeshSetIndices(meshAdress,indices);
        }

        public void AddTexture(Texture texture, bool flip)
        {
            if(noTex) noTex = false;
            texture.Load(flip);
            textures.Add(texture);
            RenderProgramDLL.AddTextureToMesh(meshAdress, texture.textureAdress);
        }

        public void SetDiffuse(Vector4 diffuse)
        {
            this.diffuse = diffuse;
            RenderProgramDLL.MeshSetDiffuse(meshAdress, new float[]{diffuse.X,diffuse.Y,diffuse.Z,diffuse.W});
        }

        public void SetSpecular(Vector4 specular)
        {
            this.specular = specular;
            RenderProgramDLL.MeshSetSpecular(meshAdress, new float[]{specular.X,specular.Y,specular.Z,specular.W});
        }


        
    }
}