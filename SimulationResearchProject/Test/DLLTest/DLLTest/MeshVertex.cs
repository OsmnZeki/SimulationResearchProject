using System.Collections.Generic;
using System.Numerics;

namespace DLLTest
{
    struct MeshVertex {
        Vector3 pos;
        Vector3 normal;
        Vector2 texCoord;

        static MeshVertex[] SetVertices(List<float> vertices, int numOfVertices)
        {
            MeshVertex[] ret = new MeshVertex[numOfVertices];

            for (int i = 0; i < numOfVertices; i+=8)
            {
                ret[i].pos = new Vector3(
                    vertices[i  + 0],
                    vertices[i  + 1],
                    vertices[i  + 2]
                );

                ret[i].normal = new Vector3(
                    vertices[i  + 3],
                    vertices[i  + 4],
                    vertices[i  + 5]

                );

                ret[i].texCoord = new Vector2(
                    vertices[i  + 6],
                    vertices[i  + 7]
                );
            }

            return ret;
        }
    };
}