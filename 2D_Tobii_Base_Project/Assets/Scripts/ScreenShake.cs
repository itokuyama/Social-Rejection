using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
	float amount;
	float duration;
	Camera cam;
	float timer = 0.0f;
	bool isRunning = false;
	GameObject mainPlayer;

	// To use ScreenShake, call ScreenShake's Shake from anywhere, with i.e.
	// GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>().Shake()
	// or (maybe) Camera.main.GetComponent<ScreenShake>().Shake()

	// Specify a shakeAmount and shakeDuration; defaults are provided
	public void Shake(float shakeAmount = 0.3f, float shakeDuration = 1.0f)
	{
		amount = shakeAmount;
		duration = shakeDuration;
		cam = GetComponent<Camera>();

		// If currently shaking, restart duration
		// else start a new shake
		if (isRunning) {
			timer = duration - shakeDuration;
		}
		else {
			StartCoroutine("ShakeCoroutine");
		}
	}

	IEnumerator ShakeCoroutine()
	{
		Vector3 trackVector = Vector3.zero;
		bool reverse = true;
		isRunning = true;
		while (timer <= duration) {
			timer += Time.deltaTime;
			if (reverse) {
				trackVector = new Vector3 (Random.Range (-amount, amount), Random.Range (-amount, amount), 0);
				cam.transform.position += trackVector;
			}
			else {
				cam.transform.position -= trackVector;
			}
			reverse = !reverse;
			yield return null;
		}

		if (reverse) {
			cam.transform.position -= trackVector;
		}

		timer = 0;
		isRunning = false;
	}
}
