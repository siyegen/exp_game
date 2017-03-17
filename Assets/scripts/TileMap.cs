using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {


    int xSize = 200;
    int zSize = 200;
    float tileSize = 1.0f;
	// Use this for initialization
	void Start () {
        Debug.Log("Building mesh!");
        BuildMesh();
	}
    
    void BuildMesh() {

        int numTiles = zSize * xSize;
        int numTriangles = numTiles * 2;

        int vertSizeX = xSize + 1;
        int vertSizeZ = zSize +1;
        int numVerts = vertSizeX * vertSizeZ;

        Vector3[] vertices = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        int[] triangles = new int[numTriangles * 3];

        for(int z=0, index=0; z < vertSizeZ; z++) {
            for(int x=0; x < vertSizeX; x++, index++) {
                //int index = z * vertSizeX + x;
                vertices[z * vertSizeX + x] = new Vector3(x*tileSize, 0, z*tileSize);
                normals[z * vertSizeX + x] = Vector3.up;
                // unclear about uv, look up more info
                uv[z * vertSizeX + x] = new Vector2((float)x/vertSizeX, (float)z/vertSizeZ);
                //Debug.Log("vertex: " + vertices[index]);
            }
        }
        
        for(int z=0; z < zSize; z++) {
            for(int x=0; x < xSize; x++) {
                int squareIndex = z * xSize + x;
                int triOffset = squareIndex * 6;
                triangles[triOffset + 0] = z * vertSizeX + x + 0;
                triangles[triOffset + 1] = z * vertSizeX + x + vertSizeX + 0;
                triangles[triOffset + 2] = z * vertSizeX + x + vertSizeX + 1;


                triangles[triOffset + 3] = z * vertSizeX + x +         0;
                triangles[triOffset + 4] = z * vertSizeX + x + vertSizeX + 1;
                triangles[triOffset + 5] = z * vertSizeX + x +         1;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        Material basicMaterial = Resources.Load<Material>("Materials/Basic");
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial = basicMaterial;
    }
}
