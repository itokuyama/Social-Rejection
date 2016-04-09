using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

	[Tooltip("Arrow object that you'll be firing")]
	public GameObject arrowPrefab;
	// For the head
	public float offsetHeight;
	public float shootingPower;
	public float reloadDelay;
	// Amount of time for full charge
	public float chargeTimeMax;
	float chargeTime = 0.0f;
	public float chargeFactorMax;
	public float chargeFactorMin;
	float chargeFactor = 0.0f;
	public float lineWidthMin;
	public float lineWidthExpand;
    public float health;
    public float totalHealth;
    public GameObject healthBar;

	// When you can take the next shot
	float reloadFinished;
	LineRenderer lineRenderer;
	// Says whether or not the player is charging a shot
	bool charging = false;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();

        health = totalHealth;

        healthBar = GameObject.FindWithTag("TowerHealthBar");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offsetPos = transform.position + new Vector3(0, offsetHeight, 0);

		// Draw line to mouse position
		Vector3 mousePosition = Input.mousePosition;
		float cameraDistance = Camera.main.transform.position.z;
		// Need to do this to account for camera's z difference
		mousePosition.z = -cameraDistance;
		Vector3 mousePositionConverted = Camera.main.ScreenToWorldPoint (mousePosition);
		lineRenderer.SetPosition (0, offsetPos);
		lineRenderer.SetPosition (1, mousePositionConverted);

		// On left button press spawn arrow
		if (Input.GetMouseButton (0) && !charging) {
			if (Time.time > reloadFinished) {
				reloadFinished = Time.time + reloadDelay;
				charging = true;
			}
		}

		if (charging) {
			chargeTime += Time.deltaTime;
			if (chargeTime > chargeTimeMax) {
				chargeFactor = chargeFactorMax;
			} else {
				chargeFactor = (chargeTime / chargeTimeMax) * chargeFactorMax;
			}

			lineRenderer.SetWidth (lineWidthMin, chargeFactor * lineWidthMin * lineWidthExpand);
		}

		if (Input.GetMouseButtonUp (0) && charging) {
			GameObject arrow = GameObject.Instantiate (arrowPrefab);

			// Fire in direction of mouse with shooting power
			arrow.transform.position = offsetPos;
			Vector3 shootDirection = mousePositionConverted - offsetPos;
			Vector2 shootForce = shootDirection;
			shootForce.Normalize ();
			if (chargeFactor < chargeFactorMin) {
				chargeFactor = chargeFactorMin;
			}
			shootForce *= shootingPower * chargeFactor;
			arrow.GetComponentInChildren<Rigidbody2D> ().AddForce (shootForce);

			float shootRotation = Vector3.Angle (transform.up, shootDirection);
			// Need to do this because Angle doesn't go past 180 degrees
			if (shootDirection.x < 0) {
				arrow.transform.Rotate (new Vector3 (0, 0, shootRotation));
			} else {
				arrow.transform.Rotate (new Vector3 (0, 0, -shootRotation));
			}

			// Reset charge variables
			charging = false;
			chargeTime = 0;
			chargeFactor = 0.0f;

			lineRenderer.SetWidth (lineWidthMin, lineWidthMin);
		}

        healthBar.transform.localScale = new Vector3(4 * (health / totalHealth), 0.2f, 1);
        healthBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(-200 * (1 - health/totalHealth), -50, 0);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
        }
    }
}
