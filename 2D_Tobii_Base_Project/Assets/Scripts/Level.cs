using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Level : MonoBehaviour {
    public int level;
    public int totalLevels;
    private EnemySpawning enemyTracker;
    private LevelData statTracker;
    public float preTime;
    public float postTime;
    private bool alreadyChanged;

	// Use this for initialization
	void Start ()
    {
        level = 1;

        enemyTracker = GameObject.FindWithTag("Spawner").GetComponent<EnemySpawning>();
        statTracker = GameObject.FindWithTag("EditorOnly").GetComponent<LevelData>();

        alreadyChanged = false;

        enemyTracker.spawnRate = Convert.ToSingle(statTracker.GetLevel(level)[1]);

        enemyTracker.remaining = Convert.ToInt32(statTracker.GetLevel(level)[3]);

        enemyTracker.spawnTime = Convert.ToInt32(statTracker.GetLevel(level)[4]);
        Debug.Log(statTracker.GetLevel(level)[3]);

        postTime = Convert.ToInt32(statTracker.GetLevel(level)[5]);
    }
	
	// Update is called once per frame
	void Update ()
    {
        enemyTracker.toSpawn = statTracker.enemyTypes[statTracker.GetLevel(level)[2]];

        int enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").GetLength(0);

	    if (enemyTracker.remaining == 0 & enemiesLeft == 0 & !alreadyChanged)
        {
            StartCoroutine(LevelChange());
            alreadyChanged = true;
        }
        else if (enemyTracker.remaining > 0 | enemiesLeft > 0)
        {
            alreadyChanged = false;
        }
	}

    IEnumerator LevelChange ()
    {
        yield return new WaitForSeconds(postTime);

        level++;

        enemyTracker.spawnRate = Convert.ToInt32(statTracker.GetLevel(level)[1]);

        enemyTracker.remaining = Convert.ToInt32(statTracker.GetLevel(level)[3]);

        enemyTracker.spawnTime =  Convert.ToInt32(statTracker.GetLevel(level)[4]);
        postTime = Convert.ToInt32(statTracker.GetLevel(level)[5]);

        enemyTracker.toSpawn = statTracker.enemyTypes[statTracker.GetLevel(level)[2]];
    }
}
