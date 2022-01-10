using System.Collections.Generic;
using System.Numerics;

namespace DLLTest
{
	public class Cube : Model
	{
		public int numbOfVertices;
		public Vector3[] pos;
		public Vector3[] normal;
		public Vector2[] texCoord;
		public  int[] indices;
		
		public Cube() : base()
		{
			numbOfVertices = 36;
			float[] vertices = new float[]
				{
					//positions					normal				textcoords
					-0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f,
					0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 1.0f, 0.0f,
					0.5f, 0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 1.0f, 1.0f,
					0.5f, 0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 1.0f, 1.0f,
					-0.5f, 0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 0.0f, 1.0f,
					-0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f,

					-0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
					0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
					0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
					0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
					-0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
					-0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f,

					-0.5f, 0.5f, 0.5f, -1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
					-0.5f, 0.5f, -0.5f, -1.0f, 0.0f, 0.0f, 1.0f, 1.0f,
					-0.5f, -0.5f, -0.5f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
					-0.5f, -0.5f, -0.5f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
					-0.5f, -0.5f, 0.5f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
					-0.5f, 0.5f, 0.5f, -1.0f, 0.0f, 0.0f, 1.0f, 0.0f,

					0.5f, 0.5f, 0.5f, +1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
					0.5f, 0.5f, -0.5f, +1.0f, 0.0f, 0.0f, 1.0f, 1.0f,
					0.5f, -0.5f, -0.5f, +1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
					0.5f, -0.5f, -0.5f, +1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
					0.5f, -0.5f, 0.5f, +1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
					0.5f, 0.5f, 0.5f, +1.0f, 0.0f, 0.0f, 1.0f, 0.0f,

					-0.5f, -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, 0.0f, 1.0f,
					0.5f, -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, 1.0f, 1.0f,
					0.5f, -0.5f, 0.5f, 0.0f, -1.0f, 0.0f, 1.0f, 0.0f,
					0.5f, -0.5f, 0.5f, 0.0f, -1.0f, 0.0f, 1.0f, 0.0f,
					-0.5f, -0.5f, 0.5f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f,
					-0.5f, -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, 0.0f, 1.0f,

					-0.5f, 0.5f, -0.5f, 0.0f, +1.0f, 0.0f, 0.0f, 1.0f,
					0.5f, 0.5f, -0.5f, 0.0f, +1.0f, 0.0f, 1.0f, 1.0f,
					0.5f, 0.5f, 0.5f, 0.0f, +1.0f, 0.0f, 1.0f, 0.0f,
					0.5f, 0.5f, 0.5f, 0.0f, +1.0f, 0.0f, 1.0f, 0.0f,
					-0.5f, 0.5f, 0.5f, 0.0f, +1.0f, 0.0f, 0.0f, 0.0f,
					-0.5f, 0.5f, -0.5f, 0.0f, +1.0f, 0.0f, 0.0f, 1.0f
				};

			GeneralVertexData.SetVertices(vertices, out pos, out normal, out texCoord);
			
			indices = new int[numbOfVertices];

			for (int i = 0; i < numbOfVertices; i++)
			{
				indices[i] = i;
			}
			
			Mesh mesh = new Mesh();
			mesh.SetVerticesPos(pos);
			mesh.SetIndices(indices);
			mesh.SetVerticesNormal(normal);
			mesh.SetVerticesTexCoordl(texCoord);
			mesh.Setup(Mesh.MeshSetupConfiguration.PosNormalTexCoordSetup);

			AddMesh(mesh);
		}
	}
}