using System.Collections.Generic;
using System.Numerics;

namespace DLLTest
{
    public class GeneralVertexData {
        
        public static void SetVertices(List<float> vertices,out Vector3[] vertexPos,out Vector3[] vertexNormal, out Vector2[] texCoord)
        {
            SetVertices(vertices.ToArray(),out vertexPos,out vertexNormal,out texCoord);
        }
        
        public static void SetVertices(float[] vertices,out Vector3[] vertexPos,out Vector3[] vertexNormal, out Vector2[] texCoord)
        {
            vertexPos = new Vector3[3 * vertices.Length];
            vertexNormal = new Vector3[3 * vertices.Length];
            texCoord = new Vector2[2 * vertices.Length];

            for (int i = 0; i < vertices.Length; i+=8)
            {
                vertexPos[i] = new Vector3(
                    vertices[i  + 0],
                    vertices[i  + 1],
                    vertices[i  + 2]
                );

                vertexNormal[i] = new Vector3(
                    vertices[i  + 3],
                    vertices[i  + 4],
                    vertices[i  + 5]

                );

                texCoord[i] = new Vector2(
                    vertices[i  + 6],
                    vertices[i  + 7]
                );
            }
        }
    };
}