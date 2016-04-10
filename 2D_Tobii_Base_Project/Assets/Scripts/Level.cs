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
    private MenuScript states;

	// Use this for initialization
	void Start ()
    {
        level = 1;

        enemyTracker = GameObject.FindWithTag("Spawner").GetComponent<EnemySpawning>();
        statTracker = GameObject.FindWithTag("EditorOnly").GetComponent<LevelData>();
        towerStuff = GameObject.FindWithTag("Tower").GetComponent<TowerController>();
        states = GameObject.FindWithTag("Events").GetComponent<MenuScript>();

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
        enemyTracker.toSpawn = statTracker.enemyTypes[statTracker.GetLevel(level)[2]];

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int enemiesLeft = enemies.GetLength(0);

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
            foreach (GameObject toBeGone in enemies)
            {
                Destroy(toBeGone);
            }
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

            if (condition == "final")
            {
                GameObject.FindWithTag("NightOverlay").GetComponent<NightOverlayController>().opacityTrigger = false;
                foreach (GameObject celestialObject in GameObject.FindGameObjectsWithTag("CelestialObject"))
                {
                    celestialObject.GetComponent<CelestialObjectController>().SwitchToSun();
                }
            }

            GameObject.Find(string.Format("Level{0}Text", level)).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(Convert.ToInt32(statTracker.GetLevel(level)[4]));
            GameObject.Find(string.Format("Level{0}Text", level)).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 3000, 0);
        }
        else if (restart)
        {
            level = 1;

            towerStuff.health = towerStuff.totalHealth;

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
