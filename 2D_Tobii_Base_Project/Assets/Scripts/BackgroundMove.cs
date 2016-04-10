using UnityEngine;
using System.Collections;

public class BackgroundMove : MonoBehaviour {
	public float leftThreshold;
	public GameObject otherHalf;
	public GameObject otherOtherHalf;
	public float speed;
	float width;

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		width = sr.bounds.size.x;

		otherHalf.transform.position = transform.position + new Vector3 (width, 0, 0);
		otherOtherHalf.transform.position = transform.position + new Vector3 (width / 2, 0, 0);
		otherOtherHalf.transform.position += new Vector3 (0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3(speed, 0, 0);
		otherHalf.transform.position -= new Vector3 (speed, 0, 0);
		otherOtherHalf.transform.position -= new Vector3 (speed, 0, 0);

		if (transform.position.x < leftThreshold) {
			transform.position = otherHalf.transform.position + new Vector3 (width, 0, 0);
			otherOtherHalf.transform.position = otherHalf.transform.position + new Vector3 (width / 2, 0, 0);
			otherOtherHalf.transform.position += new Vector3 (0, 0, 1);
		}

		if (otherHalf.transform.position.x < leftThreshold) {
			otherHalf.transform.position = transform.position + new Vector3 (width, 0, 0);
			otherOtherHalf.transform.position = transform.position + new Vector3 (width / 2, 0, 0);
			otherOtherHalf.transform.position += new Vector3 (0, 0, 1);
		}

			
	}
}
