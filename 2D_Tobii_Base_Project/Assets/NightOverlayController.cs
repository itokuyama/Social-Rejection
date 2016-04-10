using UnityEngine;
using System.Collections;

public class NightOverlayController : MonoBehaviour {

	public float dayTimer;
	public float opacityFactor = 0.7f;
	float localTimer;
	public bool opacityTrigger = true;
	// Use this for initialization
	void Start () {
		localTimer = dayTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if (localTimer > 0) {
			localTimer -= Time.deltaTime;
			float opacity = opacityFactor * localTimer / dayTimer;
			GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, opacity);
			if (false) {
				opacityTrigger = false;
				foreach (GameObject celestialObject in GameObject.FindGameObjectsWithTag ("CelestialObject")) {
					celestialObject.GetComponent<CelestialObjectController> ().SwitchToSun ();
				}
			}
		}
	}

	public void Reload() {
		opacityTrigger = true;
		localTimer = dayTimer;
		foreach (GameObject celestialObject in GameObject.FindGameObjectsWithTag ("CelestialObject")) {
			celestialObject.GetComponent<CelestialObjectController> ().SwitchToMoon ();
		}
	}
}
