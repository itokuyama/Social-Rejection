using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        preTime = statTracker.preTimes[level - 1];
        postTime = statTracker.postTimes[level - 1];
    }
	
	// Update is called once per frame
	void Update ()
    {
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
        Debug.Log("Changed level");

        enemyTracker.spawnTime = preTime;

        enemyTracker.remaining = statTracker.numberOfEnemies[level - 1];

        preTime = GameObject.FindWithTag("EditorOnly").GetComponent<LevelData>().preTimes[level - 1];
        postTime = GameObject.FindWithTag("EditorOnly").GetComponent<LevelData>().postTimes[level - 1];
    }
}
