using UnityEngine;
using System.Collections;
using System.Linq;

public class PublicFunctions : MonoBehaviour {

    public static void PhaseThruTag(GameObject thing, string[] Tags)
    {
        foreach (string tag in Tags)
        {
            GameObject[] objects = { };

                objects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject col in objects)
            {
                Physics2D.IgnoreCollision(thing.GetComponent<Collider2D>(), col.GetComponent<Collider2D>());
            }//Ignore collisions for each object with the tag
        }//Applies for each tag in the array
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
