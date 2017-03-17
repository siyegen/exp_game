using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {


    int cols = 1; //z
    int rows = 2; //x
	// Use this for initialization
	void Start () {
        Debug.Log("Building mesh!");
        BuildMesh();
	}
    
    void BuildMesh() {

        int numTiles = rows * cols;
        int numTriangles = numTiles * 2;

        int vertSizeX = cols + 1;
        int vertSizeZ = rows +1;
        int numVertices = vertSizeX * vertSizeZ;

        Material basicMaterial = Resources.Load<Material>("Materials/Basic");

        Vector3[] vertices = new Vector3[numVertices];
        Vector3[] normals = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];
        int[] triangles = new int[numTriangles * 3];

        for(int i=0, z=0; z < vertSizeZ; z++) {
            for(int x=0; x < vertSizeX; x++, i++) {
                vertices[i] = new Vector3(x, 0, z);
                normals[i] = Vector3.up;
                // unclear about uv, look up more info
                uv[i] = new Vector2((float)x/vertSizeX, (float)z/vertSizeZ);
                Debug.Log("vertex: " + vertices[i]);
            }
        }

        // for(int triIndex=0, vertIndex=0, z=0; z < cols; z++) {
        //     for(int x=0; x < rows; x++, triIndex +=6, vertIndex++) {
        //         triangles[triIndex + 0] = vertIndex +               0;
        //         triangles[triIndex + 1] = vertIndex + rows +   1;
        //         triangles[triIndex + 2] = vertIndex + 1;//x + vertSizeX +   0;
        //         Debug.Log("1: "+triangles[triIndex+0]+ " 2: "+triangles[triIndex+1]+ " 3: "+triangles[triIndex+2]);

        //         // triangles[triIndex + 3] = i +             0;
        //         // triangles[triIndex + 4] = i +             1;
        //         // triangles[triIndex + 5] = i + vertSizeX +   0;
                
        //     }
        // }

        for(int z=0; z < cols; z++) {
            for(int x=0; x < rows; x++) {
                int index = z * rows + x;
                int offset = index*6;
                triangles[offset + 0] = index + 0;
                triangles[offset + 1] = index + rows + 1;
                triangles[offset + 2] = index + rows + 0;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial = basicMaterial;
    }
}
