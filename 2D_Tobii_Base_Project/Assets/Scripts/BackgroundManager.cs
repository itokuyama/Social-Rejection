using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	public Sprite[] clouds;
	public Sprite sun;
	public Sprite moon;
	public Vector2 height;
	public Vector2 width;
	public Vector2 speed;
	public Sprite background;
	public float backgroundScroll;
	float leftBoundary;
	ArrayList cloudList;
	GameObject bgLeft;
	SpriteRenderer bgLeftSr;
	GameObject bgRight;
	SpriteRenderer bgRightSr;
	float bgWidth;

	// Use this for initialization
	void Start () {
		cloudList = new ArrayList ();

		bgLeft = new GameObject ();
		bgLeft.transform.position += new Vector3(0, 0, 10);
		bgLeft.name = "Background";
		bgLeftSr = bgLeft.AddComponent<SpriteRenderer>();
		bgLeftSr.sprite = background;

		bgRight = Instantiate (bgLeft);
		// Move right of bgLeft
		bgWidth = background.rect.width / background.pixelsPerUnit;
		bgRight.transform.position += new Vector3 (bgWidth, 0, 0);
		bgRightSr = bgRight.GetComponent<SpriteRenderer> ();
		leftBoundary = -bgRightSr.bounds.extents.x;

		foreach (var cloud in clouds) {
			GameObject cloudGO = new GameObject ();
			cloudGO.transform.position = new Vector3 (Random.Range (width.x, width.y),
				Random.Range (height.x, height.y),
				5);
			cloudGO.name = "Cloud";
			SpriteRenderer cloudGOSr = cloudGO.AddComponent<SpriteRenderer> ();
			cloudGOSr.sprite = cloud;
			cloudList.Add (cloudGO);
			StartCoroutine (CloudMove (cloudGO, Random.Range (speed.x, speed.y)));
		}
	}

	// Update is called once per frame
	void Update () {
		// Update background
		bgLeft.transform.position -= new Vector3(backgroundScroll, 0, 0);
		bgRight.transform.position -= new Vector3 (backgroundScroll, 0, 0);

		if (bgLeftSr.bounds.center.x + bgLeftSr.bounds.extents.x < leftBoundary) {
			bgLeft.transform.position = bgRight.transform.position + new Vector3 (bgWidth, 0, 0);
		} else if (bgRightSr.bounds.center.x + bgRightSr.bounds.extents.x < leftBoundary) {
			bgRight.transform.position = bgLeft.transform.position + new Vector3 (bgWidth, 0, 0);
		}

		foreach (GameObject cloud in cloudList) {
			if (cloud.transform.position.x <= width.x) {
				Vector3 spawnPosition = new Vector3 (width.y, Random.Range (height.x, height.y), 5);
				cloud.transform.position = spawnPosition;
				StartCoroutine (CloudMove (cloud, Random.Range (speed.x, speed.y)));
			}
		}
	}

	IEnumerator CloudMove(GameObject cloud, float speed) {
		while (cloud.transform.position.x > width.x) {
			cloud.transform.position -= new Vector3 (speed, 0, 0);
			yield return null;
		}
	}


}
