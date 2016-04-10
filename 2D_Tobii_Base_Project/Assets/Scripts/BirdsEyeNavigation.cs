//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// Script to navigate on an object, for example a map, using eye fixation data.
/// </summary>
[RequireComponent(typeof(FixationDataComponent), typeof(Renderer))]
public class BirdsEyeNavigation : MonoBehaviour
{
    /// <summary>
    /// The camera to be used for navigation
    /// </summary>
    public new Camera camera;

    /// <summary>
    /// The zoom speed 
    /// </summary>
    public float speed = 5.0f;

    private Renderer _rendererComponent;
    private FixationDataComponent _fixationDataComponent;
    private PannableComponent _pannableComponent;

    private float _initialSize;
    private Vector3 _initialPosition;
    private float _progress;
    private float _sourceSize;
    private float _targetSize;
    private Vector3 _sourcePosition;
    private Vector3 _targetPosition;

    public void Start()
    {
        _rendererComponent = gameObject.GetComponent<Renderer>();

        _fixationDataComponent = GetComponent<FixationDataComponent>();
        _fixationDataComponent.enabled = false;

        _pannableComponent = GetComponent<PannableComponent>();

        _initialSize = camera.orthographicSize;
        _initialPosition = camera.transform.position;

        _targetSize = _initialSize;
        _targetPosition = camera.transform.position;
        _progress = 1.0f;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _pannableComponent.BeginPanning();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _pannableComponent.EndPanning();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // to zoom out to a bird's eye view: center the camera and set its orthographic size 
            // large enough to cover the bounds of the game object (i.e., the map).
            SetTargetPosition(new Vector3(0, 0, camera.transform.position.z));

            var bounds = _rendererComponent.bounds;
            var w = bounds.extents.x / camera.aspect;
            var h = bounds.extents.y;
            SetTargetSize(Mathf.Max(w, h));

            // start detecting fixations
            _fixationDataComponent.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // stop detecting fixations
            _fixationDataComponent.enabled = false;

            var lastFixation = _fixationDataComponent.LastFixation;
            if (lastFixation.IsValid && lastFixation.GazePoint.IsWithinScreenBounds)
            {
                var gazePointScreen = (Vector3)lastFixation.GazePoint.Screen;

                // center the camera on the gaze point.
                var gazePointWorld = camera.ScreenToWorldPoint(gazePointScreen);
                var targetPositionBeforeAdjustment = new Vector3(gazePointWorld.x, gazePointWorld.y, camera.transform.position.z);

                // ...but when we zoom in, we want to zoom in at the gaze point. That is, the map 
                // position at the gaze point should remain at the same spot in the viewport after
                // the zoom.
                var originalSize = camera.orthographicSize;
                var originalPosition = camera.transform.position;

                // temporarily set size and position to calculate adjustment
                // NOTE: This can probably be calculated in a smarter way
                camera.orthographicSize = _initialSize;
                camera.transform.position = targetPositionBeforeAdjustment;
                var adjustment = camera.ScreenToWorldPoint(gazePointScreen) - gazePointWorld;

                // restore size and position before setting new targets
                camera.orthographicSize = originalSize;
                camera.transform.position = originalPosition;

                SetTargetSize(_initialSize);
                SetTargetPosition(targetPositionBeforeAdjustment - new Vector3(adjustment.x, adjustment.y, 0));
            }
            else
            {
                SetTargetSize(_initialSize);
                SetTargetPosition(_initialPosition);
            }
        }
        else
        {
            // Not zoomed out? Pan according to the pannable behavior.
            if (!Input.GetKey(KeyCode.Space))
            {
                var velocity = _pannableComponent.Velocity;
                if (velocity != Vector2.zero)
                {
                    // Calculate the new target position depending on where the player looked.
                    SetTargetPosition(_sourcePosition + new Vector3(-velocity.normalized.x, velocity.normalized.y, 0));
                    SetTargetSize(_initialSize);
                }
            }   
        }

        UpdateCameraSizeAndPosition();
    }

    void LateUpdate()
    {
        // Last thing we do each frame is clamping the camera position.
        // By overriding the target position if we go outside the boundaries,
        // we will get a nice "rubber band" effect that take us inside the map bounds again.

        var x = _targetPosition.x;
        var y = _targetPosition.y;

        // NOTE: This can be calculated nicer.
        if (x < _rendererComponent.bounds.min.x)
        {
            x = _rendererComponent.bounds.min.x;
        }
        else if (x > _rendererComponent.bounds.max.x)
        {
            x = _rendererComponent.bounds.max.x;
        }
        if (y < _rendererComponent.bounds.min.y)
        {
            y = _rendererComponent.bounds.min.y;
        }
        else if (y > _rendererComponent.bounds.max.y)
        {
            y = _rendererComponent.bounds.max.y;
        }

        _targetPosition = new Vector3(x, y, _targetPosition.z);
    }

    private void UpdateCameraSizeAndPosition()
    {
        if (_progress < 1.0f)
        {
            _progress = Mathf.Clamp01(_progress + speed * Time.deltaTime);
        }

        camera.orthographicSize = Mathf.Lerp(_sourceSize, _targetSize, _progress);
        camera.transform.position = Vector3.Lerp(_sourcePosition, _targetPosition, _progress);
    }

    private void SetTargetPosition(Vector3 targetPosition)
    {
        _sourcePosition = camera.transform.position;
        _targetPosition = targetPosition;
        _progress = 0.0f;
    }

    private void SetTargetSize(float targetSize)
    {
        _sourceSize = camera.orthographicSize;
        _targetSize = targetSize;
        _progress = 0.0f;
    }
}
