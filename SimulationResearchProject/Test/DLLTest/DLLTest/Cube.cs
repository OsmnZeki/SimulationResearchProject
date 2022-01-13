using System.Numerics;
using MESPSimulation.Graphics.Model;

namespace MESPSimulation.Graphics.Objects
{
	public class Cube : Model.Model
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

			GeneralVertexData.SetVertices(vertices, out pos, out normal, out texCoord,numbOfVertices);
			
			indices = new int[numbOfVertices];

			for (int i = 0; i < numbOfVertices; i++)
			{
				indices[i] = i;
			}
		}

		public void Initialize()
		{
			Mesh mesh = new Mesh();
			mesh.SetVerticesPos(pos);
			mesh.SetIndices(indices);
			mesh.SetVerticesNormal(normal);
			mesh.SetVerticesTexCoordl(texCoord);
			mesh.Setup(Mesh.MeshSetupConfiguration.PosNormalTexCoordSetup);
			AddMesh(mesh);
		}

		public void SetDiffuse(Vector3 diffuse)
		{

			meshes[0].SetDiffuse(new Vector4(diffuse,1f));
		}

		public void SetSpecular(Vector3 specular)
		{
			meshes[0].SetSpecular(new Vector4(specular, 1f));
		}
	}
}