using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemySpawning : MonoBehaviour {
    public float spawnRate;
    public bool isSpawning;
    public bool leftSpawning;
    public bool rightSpawning;
    public bool spawningGround;
    public int remaining;
    private LevelData data;
    private GameObject toSpawn;
    public float spawnTime;
    private Level levelTracker;

	// Use this for initialization
	void Start ()
    {
        levelTracker = GameObject.FindWithTag("GameController").GetComponent<Level>();

        data = GameObject.FindWithTag("EditorOnly").GetComponent<LevelData>();

        spawnTime = levelTracker.preTime;

        remaining = data.numberOfEnemies[levelTracker.level - 1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        spawnRate = data.spawnRates[levelTracker.level - 1];

        toSpawn = data.enemyTypes[levelTracker.level - 1];

        if (isSpawning & spawnTime <= 0)
        {
            if (leftSpawning & remaining > 0)
            {
                if (spawningGround)
                {
                    spawnGround(-6);
                }
                else
                {
                    spawnAir(-6, 4.5f, -4.5f);
                }
                remaining -= 1;
            }
            if (rightSpawning & remaining > 0)
            {
                if (spawningGround)
                {
                    spawnGround(6);
                }
                else
                {
                    spawnAir(6, 4.5f, -4.5f);
                }
                remaining -= 1;
            }

            spawnTime += 1 / spawnRate;
        }
        else if (spawnTime >= 0)
        {
            spawnTime -= Time.deltaTime;
        }
	}

    void spawnAir (float pos, float topBound, float bottomBound)
    {
        float spawnPlace = UnityEngine.Random.Range(bottomBound, topBound);

        Instantiate(toSpawn, new Vector3(pos, spawnPlace, 0), Quaternion.Euler(0, 0, 0));
    }

    void spawnGround(float pos)
    {
        Instantiate(toSpawn, new Vector3(pos, -3, 0), Quaternion.Euler(0, 0, 0));
    }
}
