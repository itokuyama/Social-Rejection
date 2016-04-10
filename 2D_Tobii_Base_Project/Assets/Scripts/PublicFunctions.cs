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

    public static float WrapSin(float a)
    {
        if (a < 0)
        {
            a += 2 * Mathf.PI;
        }

        return a;
    }//To account for the range of asin.

    public static float FindAngle(float x, float y)
    {
        float result = 0;

        float[] xlist = { Mathf.Rad2Deg * Mathf.Acos(x), -Mathf.Rad2Deg * Mathf.Acos(x) };
        float[] ylist = { Mathf.Rad2Deg * WrapSin(Mathf.Asin(y)), 180 - (Mathf.Rad2Deg * WrapSin(Mathf.Asin(y))) };
        //Since each arc formula only accounts for half of the possible angles, both halves of each are covered

        float[] differences = {Mathf.Abs(xlist[0] - ylist[0]), Mathf.Abs(xlist[0] - ylist[1]),
                               Mathf.Abs(xlist[1] - ylist[0]), Mathf.Abs(xlist[1] - ylist[1])};
        //Tests all possible configurations of angles

        foreach (float a in xlist)
        {
            foreach (float b in ylist)
            {
                if (Mathf.Abs(a - b) == differences.Min())
                {
                    result = a;
                }
            }
        }//Picks the angle that is most likely to be true

        return result;
    }//Finds the angle between two objects

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
