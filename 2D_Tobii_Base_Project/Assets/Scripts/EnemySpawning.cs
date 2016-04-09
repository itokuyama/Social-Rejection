using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour {
    public float spawnRate;
    public bool isSpawning;
    public bool leftSpawning;
    public bool rightSpawning;
    public GameObject toSpawn;
    private float spawnTime;

	// Use this for initialization
	void Start ()
    {
        spawnTime = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (isSpawning & spawnTime <= 0)
        {
            if (leftSpawning)
            {
                spawnAir(-6, 4.5f, -4.5f);
            }
            if (rightSpawning)
            {
                spawnAir(6, 4.5f, -4.5f);
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
        float spawnPlace = Random.Range(bottomBound, topBound);

        Instantiate(toSpawn, new Vector3(pos, spawnPlace, 0), Quaternion.Euler(0, 0, 0));
    }
}
