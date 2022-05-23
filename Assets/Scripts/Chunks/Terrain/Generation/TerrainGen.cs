using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGen : MonoBehaviour
{
    public Transform player;

    [Header("Tilemaps")]

    public Tilemap tileMap;
    public Tilemap objects;


    public Chunk[] terrainTypes;

    [Header("Terrain")]
    public TileBase[] tileBases;

    public float[] minHeights;
    public float[] maxHeights;

    public Object[] ObjectTypes;


    [Header("Objects")]
    public TileBase[] tileBasesObj;

    public float[] minHeightsObj;
    public float[] maxHeightsObj;

    [Header("Generation")]

    public int distance;

    public int mapSizeX;
    public int mapSizeY;
    public int seed;
    public Vector2 offset;

    public float noiseScale;
    public int octaves;
    public float persistance;
    public float lacunarity;

    public float forestNoiseScale;
    public int forestOctaves;
    public float forestPersistance;
    public float forestLacunarity;

    void Start()
    {
        terrainTypes = new Chunk[tileBases.Length];

        for (int i = 0; i < tileBases.Length; i++)
        {
            terrainTypes[i] = new Chunk(tileBases[i], minHeights[i], maxHeights[i]);

        }

        ObjectTypes = new Object[tileBasesObj.Length];
        for (int i = 0; i < tileBasesObj.Length; i++)
        {
            ObjectTypes[i] = new Object(tileBasesObj[i], minHeightsObj[i], maxHeightsObj[i]);
        }
        GeneratePlanet();
        player.position = new Vector3(mapSizeY / 2, mapSizeY / 2, -10);
    }

    public void GeneratePlanet()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapSizeY, mapSizeX, seed, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] objectNoiseMap = Noise.GenerateNoiseMap(mapSizeY, mapSizeX, seed, forestNoiseScale, forestOctaves, forestPersistance, forestLacunarity, offset);
        for (int x = 0; x < mapSizeX; x += distance)
        {
            for (int y = 0; y < mapSizeY; y += distance)
            {

                tileMap.SetTile(tileMap.WorldToCell(new Vector2(x, y)), terrainType(x,y,noiseMap));

                TileBase newObject = newTerrainType(x, y, objectNoiseMap);
                if (newObject != null && terrainType(x, y, noiseMap) == tileBases[0])
                {
                    objects.SetTile(objects.WorldToCell(new Vector2(x, y)), newObject);
                }
                if(newObject == tileBasesObj[2] && terrainType(x, y, noiseMap) == tileBases[3])
                {
                    objects.SetTile(objects.WorldToCell(new Vector2(x, y)), newObject);
                }
            }
        }
    }
    TileBase terrainType(int x, int y, float[,] noiseMap)
    {
        float value = noiseMap[x, y];
        for (int i = 0; i < terrainTypes.Length; i++)
        {
            if (value >= terrainTypes[i].minHeight && value <= terrainTypes[i].maxHeight)
            {
                return terrainTypes[i].type;
            }
        }
        return null;
    }
    TileBase newTerrainType(int x, int y, float[,] noiseMap)
    {

        float value = noiseMap[x, y];
        for (int i = 0; i < ObjectTypes.Length; i++)
        {
            if (value >= ObjectTypes[i].minHeight && value <= ObjectTypes[i].maxHeight)
            {
                return ObjectTypes[i].type;
            }
        }
        return null;
    }
    public struct Chunk
    {
        public TileBase type;
        public float minHeight;
        public float maxHeight;

        public Chunk(TileBase type, float min, float max)
        {
            this.type = type;
            this.minHeight = min;
            this.maxHeight = max;
        }
    }
    public struct Object
    {
        public TileBase type;
        public float minHeight;
        public float maxHeight;

        public Object(TileBase type, float min, float max)
        {
            this.type = type;
            this.minHeight = min;
            this.maxHeight = max;
        }
    }
}

