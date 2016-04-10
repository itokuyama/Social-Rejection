using UnityEngine;
using System.Collections;
using System;

public class MenuScript : MonoBehaviour {
    public int state;
    public GameObject playButton;
    public GameObject playCollider;
    public GameObject continueButton;
    public GameObject continueCollider;

	// Use this for initialization
	void Start ()
    {
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            playButton.SetActive(true);
            playCollider.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
            playCollider.SetActive(false);
        }

        if (state == 3)
        {
            continueButton.SetActive(true);
            continueCollider.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
            continueCollider.SetActive(false);
        }

        if (playCollider.GetComponent<Button>().isPressed)
        {
            switchState(2);
        }
        if (continueCollider.GetComponent<Button>().isPressed)
        {
            StartCoroutine(GameObject.FindWithTag("GameController").GetComponent<Level>().LevelChange(true));
            switchState(1);
        }
    }

    void switchState(int newState)
    {
        state = newState;
    }
}
