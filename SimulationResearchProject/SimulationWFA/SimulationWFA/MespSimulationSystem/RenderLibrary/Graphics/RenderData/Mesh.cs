using System;
using System.Numerics;
using RenderLibrary.DLL;
using SimulationWFA.MespUtils;

namespace RenderLibrary.Graphics.RenderData
{
    public class Mesh : IAssetSerialization
    {
        private IntPtr meshAdress;
        private int sizeOfVertices;
        
        public Mesh()
        {
            meshAdress = RenderProgramDLL.CreateMesh();
        }
        

        public void SetVerticesPos(Vector3[] pos)
        {
            float[] posF = new float[pos.Length * 3];
            sizeOfVertices = pos.Length;
            int v = 0;
            
            for (int i = 0; i < pos.Length; i++)
            {
                posF[v++] = pos[i].X;
                posF[v++] = pos[i].Y;
                posF[v++] = pos[i].Z;
            }
            RenderProgramDLL.MeshSetVerticesPos(meshAdress,posF,sizeOfVertices);
        }
        
        public void SetVerticesNormal( Vector3[] normal)
        {
            if (sizeOfVertices != normal.Length)
            {
                Console.WriteLine("The vertices and normals sizes of mesh must be equal!");
                Console.WriteLine("Vertices lenght: " + sizeOfVertices + "\nNormal Lenght: " + normal.Length);
                return;
            }
            
            
            float[] posF = new float[sizeOfVertices * 3];
            int v = 0;
            
            for (int i = 0; i < normal.Length; i++)
            {
                posF[v++] = normal[i].X;
                posF[v++] = normal[i].Y;
                posF[v++] = normal[i].Z;
            }
            RenderProgramDLL.MeshSetVerticesNormal(meshAdress,posF);
        }
        
        public void SetVerticesTexCoord(Vector2[] texCoord)
        {
            float[] posF = new float[sizeOfVertices* 2];
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
            if (sizeOfVertices != indices.Length)
            {
                Console.WriteLine("The vertices and indices sizes of mesh must be equal!");
                Console.WriteLine("Vertices lenght: " + sizeOfVertices + "\nIndices Lenght: " + indices.Length);
                return;
            }
            RenderProgramDLL.MeshSetIndices(meshAdress,indices);
        }

        public void SetMeshAdress(IntPtr adress)
        {
            meshAdress = adress;
        }

        public IntPtr GetMeshAdress()
        {
            return meshAdress;
        }
        
        //TODO:mesh get fonksiyonlarını ekle

        public Vector3[] GetVerticesPos()
        {
            float[] posF = new float[sizeOfVertices * 3];
            RenderProgramDLL.MeshGetVerticesPos(meshAdress, posF);
            Vector3[] pos = new Vector3[sizeOfVertices];
            for (int i = 0; i < sizeOfVertices; i++)
            {
                pos[i].X = posF[i * 3 + 0];
                pos[i].Y = posF[i * 3 + 1];
                pos[i].Z = posF[i * 3 + 2];
            }
            return pos;
        }

        public Vector3[] GetVerticesNormal()
        {
            float[] posF = new float[sizeOfVertices * 3];
            RenderProgramDLL.MeshGetVerticesNormal(meshAdress, posF);
            Vector3[] pos = new Vector3[sizeOfVertices];
            for (int i = 0; i < sizeOfVertices; i++)
            {
                pos[i].X = posF[i * 3 + 0];
                pos[i].Y = posF[i * 3 + 1];
                pos[i].Z = posF[i * 3 + 2];
            }
            return pos;
        }

        public Vector2[] GetVerticesTexCoord()
        {
            float[] posF = new float[sizeOfVertices * 2];
            RenderProgramDLL.MeshGetVerticesTexCoord(meshAdress, posF);
            Vector2[] pos = new Vector2[sizeOfVertices];
            for (int i = 0; i < sizeOfVertices; i++)
            {
                pos[i].X = posF[i * 2 + 0];
                pos[i].Y = posF[i * 2 + 1];
            }
            return pos;
        }

        public object Serialization()
        {
            MeshSerializationData meshData = new MeshSerializationData();
            meshData.verticesPos = GetVerticesPos();
            meshData.normalPos = GetVerticesNormal();
            meshData.texCoord = GetVerticesTexCoord();

            return meshData;
        }

        public T Deserialization<T>(object data)
        {
            throw new NotImplementedException();
        }

        public object Deserialization(object data)
        {
            throw new NotImplementedException();
        }
    }
}