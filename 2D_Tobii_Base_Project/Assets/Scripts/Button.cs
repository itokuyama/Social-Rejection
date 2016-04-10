using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
    public bool isPressed;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            StartCoroutine(Press());
        }
    }

    IEnumerator Press()
    {
        isPressed = true;

        yield return new WaitForEndOfFrame();

        isPressed = false;
    }
}
