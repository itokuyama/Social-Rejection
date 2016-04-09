using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public float destroyWhenBelow = -10;
	public float turnRate = 0.1f;
	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Destroy if off screen
		if (transform.position.y < destroyWhenBelow) {
			Destroy (gameObject);
		}

		Vector2 currentVel = rigidBody.velocity;
		float turnRotation = Vector3.Angle (transform.up, rigidBody.velocity);
		float partialTurn = Mathf.Lerp (turnRotation, 0, 1 - turnRate);
		if (rigidBody.velocity.x < 0) {
			transform.Rotate (new Vector3 (0, 0, partialTurn));
		} else {
			transform.Rotate (new Vector3 (0, 0, -partialTurn));
		}

	}
}
