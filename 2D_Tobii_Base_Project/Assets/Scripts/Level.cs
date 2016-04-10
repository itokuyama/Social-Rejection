using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Level : MonoBehaviour {
    public int level;
    public int totalLevels;
    private EnemySpawning enemyTracker;
    private LevelData statTracker;
    private TowerController towerStuff;
    public float preTime;
    public float postTime;
    private bool alreadyChanged;
    public string condition;

	// Use this for initialization
	void Start ()
    {
        level = 1;

        enemyTracker = GameObject.FindWithTag("Spawner").GetComponent<EnemySpawning>();
        statTracker = GameObject.FindWithTag("EditorOnly").GetComponent<LevelData>();
        towerStuff = GameObject.FindWithTag("Tower").GetComponent<TowerController>();

        alreadyChanged = false;

        enemyTracker.spawnRate = Convert.ToSingle(statTracker.GetLevel(level)[1]);

        enemyTracker.remaining = Convert.ToInt32(statTracker.GetLevel(level)[3]);

        enemyTracker.spawnTime = Convert.ToInt32(statTracker.GetLevel(level)[4]);

        postTime = Convert.ToInt32(statTracker.GetLevel(level)[5]);

        condition = statTracker.GetLevel(level)[6];
    }
	
	// Update is called once per frame
	void Update ()
    {
        MenuScript states = GameObject.FindWithTag("Events").GetComponent<MenuScript>();

        enemyTracker.toSpawn = statTracker.enemyTypes[statTracker.GetLevel(level)[2]];

        int enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").GetLength(0);

	    if (enemyTracker.remaining == 0 & enemiesLeft == 0 & !alreadyChanged & states.state == 2)
        {
            if (level == statTracker.levelConfigs.GetLength(0) - 1)
            {
                states.state = 3;
            }
            else
            {
                StartCoroutine(LevelChange(false));
                alreadyChanged = true;
            }
        }
        else if (enemyTracker.remaining > 0 | enemiesLeft > 0)
        {
            alreadyChanged = false;
        }

        if (states.state == 3 & !towerStuff.lost)
        {
            towerStuff.winText.SetActive(true);
            towerStuff.loseText.SetActive(false);
        }
        else if (states.state == 3 & towerStuff.lost)
        {
            towerStuff.winText.SetActive(false);
            towerStuff.loseText.SetActive(true);
        }
        else
        {
            towerStuff.winText.SetActive(false);
            towerStuff.loseText.SetActive(false);
        }
	}

    public IEnumerator LevelChange (bool restart)
    {
        if (!restart)
        {
            yield return new WaitForSeconds(postTime);

            level++;

            enemyTracker.spawnRate = Convert.ToSingle(statTracker.GetLevel(level)[1]);

            enemyTracker.toSpawn = statTracker.enemyTypes[statTracker.GetLevel(level)[2]];

            enemyTracker.remaining = Convert.ToInt32(statTracker.GetLevel(level)[3]);

            enemyTracker.spawnTime = Convert.ToInt32(statTracker.GetLevel(level)[4]);
            postTime = Convert.ToInt32(statTracker.GetLevel(level)[5]);

            condition = statTracker.GetLevel(level)[6];
        }
        else if (restart)
        {
            level = 1;

            enemyTracker.spawnRate = Convert.ToSingle(statTracker.GetLevel(level)[1]);

            enemyTracker.toSpawn = statTracker.enemyTypes[statTracker.GetLevel(level)[2]];

            enemyTracker.remaining = Convert.ToInt32(statTracker.GetLevel(level)[3]);

            enemyTracker.spawnTime = Convert.ToInt32(statTracker.GetLevel(level)[4]);
            postTime = Convert.ToInt32(statTracker.GetLevel(level)[5]);

            condition = statTracker.GetLevel(level)[6];
        }
        Debug.Log(condition);
    }
}
