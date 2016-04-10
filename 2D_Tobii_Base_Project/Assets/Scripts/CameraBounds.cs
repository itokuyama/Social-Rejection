using UnityEngine;
using System.Collections;

public class CameraBounds : MonoBehaviour {

    public float cameraSpeed;

    public float panningHeightFactor;
    public float panningWidthFactor;
    Rect panningRect;

    public float boundingWidth;
    public float boundingHeight;
    Rect boundingRect;

    private float halfHeight;
    private float halfWidth;

    public float heightOffset;

	// Use this for initialization
	void Start () {
        Camera camera = GetComponent<Camera>();
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        panningRect = new Rect(0, 0, panningWidthFactor * cameraWidth, panningHeightFactor * cameraHeight);

        boundingRect = new Rect(0, 0, boundingWidth, boundingHeight);
        boundingRect.center = Vector3.zero + new Vector3 (0, heightOffset, 0);

        halfHeight = cameraHeight / 2;
        halfWidth = cameraWidth / 2;
    }

    
    // Update is called once per frame
    void Update () {
        var lastGazePoint = GetComponent<GazePointDataComponent>().LastGazePoint;
        panningRect.center = transform.position;

        Vector2 screenSpace = new Vector2 (0,0);
        if (lastGazePoint.IsValid)
        {
            screenSpace = lastGazePoint.Screen;
            Vector3 eyePos = Camera.main.ScreenToWorldPoint(screenSpace);

            transform.position += new Vector3(0, 0, 10);
            if (!panningRect.Contains(eyePos))
            {
                Vector3 cameraMove = Vector3.Lerp(transform.position, eyePos, cameraSpeed);
                transform.position = cameraMove;
                if (transform.position.x - halfWidth < boundingRect.xMin)
                {
                    transform.position = new Vector3(boundingRect.xMin + halfWidth, transform.position.y, 0);
                }
                else if (transform.position.x + halfWidth > boundingRect.xMax)
                {
                    transform.position = new Vector3(boundingRect.xMax - halfWidth, transform.position.y, 0);
                }

                if (transform.position.y - halfHeight < boundingRect.yMin)
                {
                    transform.position = new Vector3(transform.position.x, boundingRect.yMin + halfHeight, 0);
                }
                else if(transform.position.y + halfHeight > boundingRect.yMax)
                {
                    transform.position = new Vector3(transform.position.x, boundingRect.yMax - halfHeight, 0);
                }
            }

            transform.position -= new Vector3(0, 0, 10);
        }
    }
}
