using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

	[Tooltip("Arrow object that you'll be firing")]
	public GameObject arrowPrefab;
	public float shootingPower;

	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Draw line to mouse position
		Vector3 mousePosition = Input.mousePosition;
		float cameraDistance = Camera.main.transform.position.z;
		// Need to do this to account for camera's z difference
		mousePosition.z = -cameraDistance;
		Vector3 mousePositionConverted = Camera.main.ScreenToWorldPoint (mousePosition);
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, mousePositionConverted);

		// On left click spawn arrow
		if (Input.GetMouseButtonDown (0)) {
			GameObject arrow = GameObject.Instantiate (arrowPrefab);

			// Fire in direction of mouse with shooting power
			arrow.transform.position = transform.position;
			Vector3 shootDirection = mousePositionConverted - transform.position;
			Vector2 shootForce = shootDirection;
			shootForce.Normalize ();
			shootForce *= shootingPower;
			arrow.GetComponentInChildren<Rigidbody2D> ().AddForce (shootForce);

			float shootRotation = Vector3.Angle (transform.up, shootDirection);
			// Need to do this because Angle doesn't go past 180 degrees
			if (shootDirection.x < 0) {
				arrow.transform.Rotate (new Vector3 (0, 0, shootRotation));
			} else {
				arrow.transform.Rotate (new Vector3 (0, 0, -shootRotation));
			}
		}
	}
}
