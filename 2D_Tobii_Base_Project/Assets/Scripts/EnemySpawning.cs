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
    public GameObject toSpawn;
    public float spawnTime;
    public Level condTracker;

	// Use this for initialization
	void Start ()
    {
        condTracker = GameObject.FindWithTag("GameController").GetComponent<Level>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(condTracker.condition);
        if (condTracker.condition == "ground" | condTracker.condition == "boss")
        {
            spawningGround = true;
        }
        else
        {
            spawningGround = false;
        }

        if (GameObject.FindWithTag("Events").GetComponent<MenuScript>().state == 2)
        {
            isSpawning = true;
        }
        else
        {
            isSpawning = false;
        }

        if (isSpawning & spawnTime <= 0)
        {
            if (leftSpawning & remaining > 0)
            {
                if (spawningGround)
                {
                    spawnGround(-15);
                }
                else
                {
                    spawnAir(-15, 10, -10);
                }
                remaining -= 1;
            }
            if (rightSpawning & remaining > 0)
            {
                if (spawningGround)
                {
                    spawnGround(15);
                }
                else
                {
                    spawnAir(15, 10, -10);
                }
                remaining -= 1;
            }

            spawnTime += 1 / spawnRate;
        }
        else if (spawnTime >= 0 & isSpawning)
        {
            spawnTime -= Time.deltaTime;
        }
	}

    void spawnAir (float pos, float topBound, float bottomBound)
    {
        float spawnPlace = UnityEngine.Random.Range(bottomBound, topBound);

        Vector3 spawnPos = new Vector3(pos, spawnPlace, 0);

        float angle = PublicFunctions.FindAngle((spawnPos - GameObject.FindWithTag("Tower").transform.position).normalized.x, (spawnPos - GameObject.FindWithTag("Tower").transform.position).normalized.y);

        Instantiate(toSpawn, new Vector3(pos, spawnPlace, 0), Quaternion.Euler(0, 0, angle + 90));
    }

    void spawnGround(float pos)
    {
        Instantiate(toSpawn, new Vector3(pos, -3, 0), Quaternion.Euler(0, 0, 0));
    }
}
