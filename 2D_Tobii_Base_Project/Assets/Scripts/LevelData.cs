using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelData : MonoBehaviour {
    public List<GameObject> listOfTypes;
    public Dictionary<string, GameObject> enemyTypes;
    public string[] levelConfigs;

    void Awake ()
    {
        levelConfigs = File.ReadAllLines("Levels.txt");

        enemyTypes = new Dictionary<string, GameObject>();
    }

	// Use this for initialization
	void Start ()
    {
        for (int i = 1;  i <= listOfTypes.Count; i++)
        {
            enemyTypes.Add(string.Format("type{0}", i), listOfTypes[i - 1]);
        }
    }

    public string[] GetLevel(int level)
    {
        return levelConfigs[level].Split(';');
    }
}
