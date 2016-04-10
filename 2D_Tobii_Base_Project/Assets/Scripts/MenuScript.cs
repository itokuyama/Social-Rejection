using UnityEngine;
using System.Collections;
using System;

public class MenuScript : MonoBehaviour {
    public int state;
	public GameObject frontLogo;
    public GameObject playButton;
    public GameObject playCollider;
    public GameObject continueButton;
    public GameObject continueCollider;
    public GameObject[] levelTexts;

	// Use this for initialization
	void Start ()
    {
        state = 1;

        levelTexts = GameObject.FindGameObjectsWithTag("LevelText");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            playButton.SetActive(true);
            playCollider.SetActive(true);
			frontLogo.SetActive (true);
        }
        else
        {
            playButton.SetActive(false);
            playCollider.SetActive(false);
			frontLogo.SetActive (false);
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
            StartCoroutine(showFirstLevel());
        }
        if (continueCollider.GetComponent<Button>().isPressed)
        {
            StartCoroutine(GameObject.FindWithTag("GameController").GetComponent<Level>().LevelChange(true));
            switchState(1);
			GameObject nightOverlay = GameObject.FindGameObjectWithTag ("NightOverlay");
			nightOverlay.GetComponent<NightOverlayController> ().Reload ();
        }
    }

    IEnumerator showFirstLevel()
    {
        GameObject.Find("Level1Text").GetComponent<RectTransform>().anchoredPosition = new Vector3 (0, 0, 0);
        yield return new WaitForSeconds(1);
        GameObject.Find("Level1Text").GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 3000, 0);
    }

    void switchState(int newState)
    {
        state = newState;
    }
}
