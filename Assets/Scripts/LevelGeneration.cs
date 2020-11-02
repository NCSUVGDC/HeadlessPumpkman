using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [Header("Terrain Prefabs")]

    public GameObject terrain_L;
    public GameObject terrain_R;
    public GameObject terrain_1;
    public GameObject terrain_2;
    public GameObject terrain_3;

    [Space(10)]

    public GameObject wall_1_L;
    public GameObject wall_1_R;
    public GameObject wall_1_1;
    public GameObject wall_1_2;
    public GameObject wall_1_3;

    [Space(10)]

    public GameObject wall_2_L;
    public GameObject wall_2_R;
    public GameObject wall_2_1;
    public GameObject wall_2_2;
    public GameObject wall_2_3;


    [Header("Spawn Prefabs")]
    public GameObject enemy;
    public GameObject coin;
    public GameObject crate;


    [Header("Scene References")]

    public GameObject origin;

    public GameObject player;


    [Header("Generation Values")]

    [Min(0)]
    public int levelLength = 100;

    [Min(0)]
    public int levelHeight = 12;

    [Min(0)]
    public int maxCliffHeight = 5;

    [Min(0)]
    public int enemyNum = 5;

    [Min(0)]
    public int crateNum = 12;

    [Min(0)]
    public int coinNum = 100;

    private enum LayerTypes { EMPTY = -1, TERRAIN, WALL_1, WALL_2 };
    private int[,] generatedArray;

    void Awake()
    {
        GenerateLevel();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.x >= origin.transform.position.x)
        {
            GameObject go = new GameObject();
            Vector3 oldOrigin = origin.transform.position;
            origin = Instantiate(go);
            origin.name = "GeneratedLevel";
            origin.transform.position = oldOrigin + new Vector3(levelLength, 0, 0);
            GenerateLevel();
        }
    }


    public void GenerateLevel()
    {
        // Declare generatedArray
        generatedArray = new int[levelLength, levelHeight];

        // Define generatedArray
        int terrainType = (int)LayerTypes.EMPTY;
        for (int i = 0; i < levelLength; i++)
        {
            for (int j = 0; j < levelHeight; j++)
            {
                if (j != 0)
                {
                    // If TERRAIN below
                    if (generatedArray[i, j - 1] == (int)LayerTypes.TERRAIN)
                    {
                        terrainType = (int)LayerTypes.EMPTY;
                    }

                    // If WALL_1 below
                    else if (generatedArray[i, j - 1] == (int)LayerTypes.WALL_1)
                    {
                        terrainType = (int)LayerTypes.TERRAIN;
                    }

                    // If WALL_2 below
                    else if (generatedArray[i, j - 1] == (int)LayerTypes.WALL_2)
                    {
                        terrainType = Random.Range(1, 3);
                    }

                    // Restrict cliff size
                    if (terrainType == (int)LayerTypes.WALL_2 && j > maxCliffHeight - 3)
                    {
                        terrainType = (int)LayerTypes.WALL_1;
                    }
                }
                else
                {
                    terrainType = Random.Range(-1, 3);
                }

                generatedArray[i, j] = terrainType;
            }
        }

        // Standardize GeneratedArray platform heights
        for (int i = 1; i < levelLength - 1; i++)
        {
            for (int j = 0; j < levelHeight - 1; j++)
            {
                if (generatedArray[i - 1, j] > (int)LayerTypes.EMPTY)
                {
                    if (Random.Range(0, 2) != 0)
                    {
                        generatedArray[i, j] = generatedArray[i - 1, j];
                        generatedArray[i + 1, j] = generatedArray[i, j];
                    }

                }
            }
        }

        // Fill in GeneratedArray columns
        for (int i = 0; i < levelLength; i++)
        {
            int currentLayer = -1;

            for (int j = levelHeight - 1; j >= 0; j--)
            {
                if (currentLayer == 1)
                {
                    currentLayer = 2;
                }

                if (currentLayer == 0)
                {
                    currentLayer = 1;
                }

                if (generatedArray[i, j] > (int)LayerTypes.EMPTY && currentLayer == -1)
                {
                    currentLayer = 0;
                }

                generatedArray[i, j] = currentLayer;
            }
        }


        // Create level from generatedArray
        for (int i = 0; i < levelLength; i++)
        {
            for (int j = 0; j < levelHeight; j++)
            {
                // If terrain layer
                if (generatedArray[i, j] == (int)LayerTypes.TERRAIN)
                {
                    if (i > 0 && i < levelLength - 1)
                    {
                        if (generatedArray[i - 1, j] == (int)LayerTypes.TERRAIN && generatedArray[i + 1, j] >= (int)LayerTypes.TERRAIN)
                        {
                            GameObject spawn = Instantiate(terrain_1, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] == (int)LayerTypes.TERRAIN && generatedArray[i + 1, j] < (int)LayerTypes.TERRAIN)
                        {
                            GameObject spawn = Instantiate(terrain_R, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] < (int)LayerTypes.TERRAIN)
                        {
                            GameObject spawn = Instantiate(terrain_L, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] > (int)LayerTypes.TERRAIN)
                        {
                            GameObject spawn = Instantiate(terrain_1, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }
                    }
                    else
                    {
                        GameObject spawn = Instantiate(terrain_L, origin.transform);
                        spawn.transform.localPosition = new Vector3(i, j, 0);
                    }
                }

                // If wall 1 layer
                else if (generatedArray[i, j] == (int)LayerTypes.WALL_1)
                {
                    if (i > 0 && i < levelLength - 1)
                    {
                        if (generatedArray[i - 1, j] == (int)LayerTypes.WALL_1 && generatedArray[i + 1, j] >= (int)LayerTypes.WALL_1)
                        {
                            GameObject spawn = Instantiate(wall_1_1, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] == (int)LayerTypes.WALL_1 && generatedArray[i + 1, j] < (int)LayerTypes.WALL_1)
                        {
                            GameObject spawn = Instantiate(wall_1_R, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] < (int)LayerTypes.WALL_1)
                        {
                            GameObject spawn = Instantiate(wall_1_L, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] > (int)LayerTypes.WALL_1)
                        {
                            GameObject spawn = Instantiate(wall_1_1, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }
                    }
                    else
                    {
                        GameObject spawn = Instantiate(wall_1_L, origin.transform);
                        spawn.transform.localPosition = new Vector3(i, j, 0);
                    }
                }

                // If wall 2 layer
                else if (generatedArray[i, j] == (int)LayerTypes.WALL_2)
                {
                    if (i > 0 && i < levelLength - 1)
                    {
                        if (generatedArray[i - 1, j] == (int)LayerTypes.WALL_2 && generatedArray[i + 1, j] >= (int)LayerTypes.WALL_2)
                        {
                            GameObject spawn = Instantiate(wall_2_1, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] == (int)LayerTypes.WALL_2 && generatedArray[i + 1, j] < (int)LayerTypes.WALL_2)
                        {
                            GameObject spawn = Instantiate(wall_2_R, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] < (int)LayerTypes.WALL_2)
                        {
                            GameObject spawn = Instantiate(wall_2_L, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }

                        else if (generatedArray[i - 1, j] > (int)LayerTypes.WALL_2)
                        {
                            GameObject spawn = Instantiate(wall_2_1, origin.transform);
                            spawn.transform.localPosition = new Vector3(i, j, 0);
                        }
                    }
                    else
                    {
                        GameObject spawn = Instantiate(wall_2_L, origin.transform);
                        spawn.transform.localPosition = new Vector3(i, j, 0);
                    }
                }
            }
        }

        // Spawn enemies
        for (int i = 0; i < enemyNum; i++)
        {
            GameObject spawnEnemy = Instantiate(enemy, origin.transform);
            spawnEnemy.transform.localPosition = new Vector3(Random.Range(0, levelLength), levelHeight, 0);
        }

        // Spawn crates
        for (int i = 0; i < crateNum; i++)
        {
            GameObject spawnCrate = Instantiate(crate, origin.transform);
            spawnCrate.transform.localPosition = new Vector3(Random.Range(0, levelLength), levelHeight, 0);
            spawnCrate.transform.localScale = new Vector3(2, 2, 2);
        }

        // Spawn coins
        for (int i = 0; i < coinNum; i++)
        {
            GameObject spawnCoin = Instantiate(coin, origin.transform);
            spawnCoin.transform.localPosition = new Vector3(Random.Range(0, levelLength), Random.Range(0, levelHeight), 0);
        }
    }
}
