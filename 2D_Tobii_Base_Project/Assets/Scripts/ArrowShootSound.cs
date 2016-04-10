using UnityEngine;
using System.Collections;

public class ArrowShootSound : MonoBehaviour {

    public AudioClip shoot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //if (GameObject.FindWithTag("Weapon"))
     //   {
     //       SoundManager.instance.PlaySingle(shoot);
     //   }

        if (Input.GetMouseButtonUp(0))
        {
            SoundManager.instance.PlaySingle(shoot);
        }
	}
}
