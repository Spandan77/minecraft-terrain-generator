using UnityEngine;

public class TerrrainGenerator : MonoBehaviour
{
    public GameObject chunkPrefab;

    public float amplitude;
    public float scale;
    public int sizeOfEachChunk;

    public int chunksSize;

    void Start()
    {
        for (int x = 0; x < chunksSize; x++)
        {
            for (int z = 0; z < chunksSize; z++)
            {
                int i = x-((chunksSize-1)/2);
                int j = z-((chunksSize-1)/2);
                
                GameObject Chunk = Instantiate(chunkPrefab, new(sizeOfEachChunk*i, 0, sizeOfEachChunk*j), Quaternion.identity, transform);
                Chunk.GetComponent<ChunkGenerator>().GenerateTerrain(new(scale*i, scale*j));
            }
        }

        // GameObject chunkCenter = Instantiate(chunkPrefab, new(), Quaternion.identity, transform);
        // chunkCenter.GetComponent<TerrainGenerator>().GenerateTerrain(new());

        // GameObject chunkLeft = Instantiate(chunkPrefab, new(sizeOfEachChunk, 0, 0), Quaternion.identity, transform);
        // chunkLeft.GetComponent<TerrainGenerator>().GenerateTerrain(new(scale, 0));

        // GameObject chunkRight = Instantiate(chunkPrefab, new(-sizeOfEachChunk, 0, 0), Quaternion.identity, transform);
        // chunkRight.GetComponent<TerrainGenerator>().GenerateTerrain(new(-scale, 0));
    }
}
