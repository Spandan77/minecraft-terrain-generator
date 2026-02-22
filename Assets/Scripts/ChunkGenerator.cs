using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkGenerator : MonoBehaviour
{
    TerrrainGenerator parent;
    
    MeshFilter filter;
    MeshCollider meshCollider;
    Mesh mesh;

    Vector2 offset;
    public void GenerateTerrain(Vector2 offset)
    {
        parent = GetComponentInParent<TerrrainGenerator>();

        this.offset = offset;

        filter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        vertices = new List<Vector3>();
        triangles = new List<int>();

        CreateTerrain();

        filter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    // void Update()
    // {
    //     vertices = new List<Vector3>();
    //     triangles = new List<int>();

    //     if (toggle){
    //         toggle = !toggle;
    //         CreateTerrain();
    //         filter.mesh = mesh;
    //     }
    // }

    // public GameObject cube;
    void CreateTerrain()
    {
        for (int x = 0; x < parent.sizeOfEachChunk; x++)
        {
            for (int z = 0; z < parent.sizeOfEachChunk; z++)
            {
                float y = function(x, z);

                for (int i = 0; i <= y; i++)
                {
                    Vector3[] dirs = GetDirections(x, z, i);
                    CreateMeshAt(
                        new Vector3(x - (float)parent.sizeOfEachChunk/2 + 0.5f, i,z - (float)parent.sizeOfEachChunk/2 + 0.5f),
                        dirs
                    );
                }
            }
        }
    }

    float function(int x, int z)
    {
        return Mathf.Round(Mathf.PerlinNoise((float)x/parent.sizeOfEachChunk * parent.scale + offset.x, (float)z/parent.sizeOfEachChunk * parent.scale + offset.y) * parent.amplitude);
    }

    private Vector3[] GetDirections(int x, int z, int i)
    {
        Vector3[] dirs = new Vector3[6];

        if (i > function(x+1, z))
        {
            dirs[1] = Vector3.left;
        }
        if (i > function(x-1, z))
        {
            dirs[2] = Vector3.right;
        }
        if (i > function(x, z+1))
        {
            dirs[3] = Vector3.forward;
        }
        if (i > function(x, z-1))
        {
            dirs[4] = Vector3.back;
        }
        
        if (i == function(x, z))
        {
            dirs[5] = Vector3.up;
        }

        return dirs;
    }

    List<Vector3> vertices;
    List<int> triangles;

    void CreateMeshAt(Vector3 center, Vector3[] dir)
    {
        mesh = new Mesh();

        //LEFT
        if (dir.Contains(Vector3.left))
        {
            AddFace(
                new Vector3[]{
                    center + new Vector3(0.5f, 0 - 0.5f, 0 - 0.5f),
                    center + new Vector3(0.5f, 0 - 0.5f, 1 - 0.5f),
                    center + new Vector3(0.5f, 1 - 0.5f, 0 - 0.5f),
                    center + new Vector3(0.5f, 1 - 0.5f, 1 - 0.5f),
                },
                new int[]{
                    0, 2, 1,
                    1, 2, 3,
                }
            );
        }
        
        //RIGHT
        if (dir.Contains(Vector3.right))
        {
            AddFace(
                new Vector3[] {
                    center - new Vector3(0.5f, 0 - 0.5f, 0 - 0.5f),
                    center - new Vector3(0.5f, 0 - 0.5f, 1 - 0.5f),
                    center - new Vector3(0.5f, 1 - 0.5f, 0 - 0.5f),
                    center - new Vector3(0.5f, 1 - 0.5f, 1 - 0.5f),
                },
                new int[] {
                    0, 1, 2,
                    1, 3, 2,
                }
            );
        }

        // BACK
        if (dir.Contains(Vector3.back))
        {
            AddFace(
                new Vector3[] {
                    center - new Vector3(0 - 0.5f, 0 - 0.5f, 0.5f),
                    center - new Vector3(0 - 0.5f, 1 - 0.5f, 0.5f),
                    center - new Vector3(1 - 0.5f, 0 - 0.5f, 0.5f),
                    center - new Vector3(1 - 0.5f, 1 - 0.5f, 0.5f),
                },
                new int[] {
                    0, 1, 2,
                    1, 3, 2,
                }
            );
        }

        // FRONT
        if (dir.Contains(Vector3.forward))
        {
            AddFace(
                new Vector3[] {
                    center + new Vector3(0 - 0.5f, 0 - 0.5f, 0.5f),
                    center + new Vector3(0 - 0.5f, 1 - 0.5f, 0.5f),
                    center + new Vector3(1 - 0.5f, 0 - 0.5f, 0.5f),
                    center + new Vector3(1 - 0.5f, 1 - 0.5f, 0.5f),
                },
                new int[] {
                    0, 2, 1,
                    1, 2, 3,
                }
            );
        }

        //DOWN
        if (dir.Contains(Vector3.down))
        {
            AddFace(
                new Vector3[] {
                    center - new Vector3(0 - 0.5f, 0.5f, 0 - 0.5f),
                    center - new Vector3(0 - 0.5f, 0.5f, 1 - 0.5f),
                    center - new Vector3(1 - 0.5f, 0.5f, 0 - 0.5f),
                    center - new Vector3(1 - 0.5f, 0.5f, 1 - 0.5f),
                },
                new int[] {
                    0, 2, 1,
                    1, 2, 3,
                }
            );
        }

        // UP
        if (dir.Contains(Vector3.up))
        {
            AddFace(
                new Vector3[] {
                    center + new Vector3(0 - 0.5f, 0.5f, 0 - 0.5f),
                    center + new Vector3(0 - 0.5f, 0.5f, 1 - 0.5f),
                    center + new Vector3(1 - 0.5f, 0.5f, 0 - 0.5f),
                    center + new Vector3(1 - 0.5f, 0.5f, 1 - 0.5f),
                },
                new int[] {
                    0, 1, 2,
                    1, 3, 2
                }
            );
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
    }
    
    void AddFace(Vector3[] newVertices, int[] newTrianglesOrder)
    {
        int previousVerticesCount = vertices.Count;
        
        vertices.AddRange(newVertices);

        for (int i = 0; i < newTrianglesOrder.Length; i++)
        {
            triangles.Add(previousVerticesCount + newTrianglesOrder[i]);
        }
    }
}
