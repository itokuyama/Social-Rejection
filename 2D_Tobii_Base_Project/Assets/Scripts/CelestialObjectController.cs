using UnityEngine;
using System.Collections;

public class CelestialObjectController : MonoBehaviour {
	public Sprite sun;
	public Sprite moon;
	float speed;
	public float speedMin;
	public float speedMax;
	public float widthMin;
	public float widthMax;
	public float heightMin;
	public float heightMax;

	// Use this for initialization
	void Start () {
		speed = Random.Range (speedMin, speedMax);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3 (speed, 0, 0);
		if (transform.position.x < widthMin) {
			transform.position = new Vector3 (widthMax, Random.Range(heightMin, heightMax), transform.position.z);
			speed = Random.Range (speedMin, speedMax);
		}
	}

	// Call this to switch a moon to a sun
	public void SwitchToSun() {
		GetComponent<SpriteRenderer> ().sprite = sun;
	}

	public void SwitchToMoon() {
		GetComponent<SpriteRenderer> ().sprite = moon;
	}
}
