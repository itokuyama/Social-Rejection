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
        levelConfigs = ("LevelNum; SpawnRate; Enemy Types; Enemy Amount; Pre - Time; Post - Time; Special Condition:"
+"1;0.4;type1;10;1;2;ground:"
+"2;0.4;type2;8;1;2;none:"
+"3;0.45;type2;10;1;2;none:"
+"4;0.5;type2;20;1;2;none:"
+"5;0.6;type5;25;1;2;none:"
+"6;0.7;type5;40;1;2;none:"
+"7;0.8;type6;40;1;2;none:"
+"8;1;type3;1;1;2;boss:"
+"9;1;type4;1;1;15;still:"
+"10;5;type1;20;1;30;final").Split(':');


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
