using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {

	public float heightMin;
	public float heightMax;
	public float widthMin;
	public float widthMax;
	public float speedMin;
	public float speedMax;
	float speed;

	// Use this for initialization
	void Start () {
		speed = Random.Range (speedMin, speedMax);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3(speed, 0, 0);

		if (transform.position.x < widthMin) {
			transform.position = new Vector3 (widthMax, Random.Range (heightMin, heightMax), transform.position.z);
			speed = Random.Range (speedMin, speedMax);
		}
	
	}
}
