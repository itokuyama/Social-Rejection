//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Script for a game object that grows dynamically when it senses the user's gaze.
/// </summary>
[RequireComponent(typeof(GazeAwareComponent))]
public class GrowOnGaze : MonoBehaviour
{
    private static readonly Vector3 NormalScale = new Vector3(1.0f, 1.0f, 1.0f);
    private static readonly Vector3 LargeScale = new Vector3(1.5f, 1.5f, 1.5f);

    private float _scaleFactor = 0;

    public float speed = 10.0f;
    private GazeAwareComponent _gazeAwareComponent;

    protected void Start()
    {
        _gazeAwareComponent = GetComponent<GazeAwareComponent>();
    }

    /// <summary>
    /// Update interactor bounds and transform
    /// </summary>
    protected void Update()
    {
        // Update the scale factor depending on whether the eye-gaze is on the object or not.
        if (_gazeAwareComponent.HasGaze)
        {
            _scaleFactor = Mathf.Clamp01(_scaleFactor + speed * Time.deltaTime);
        }
        else
        {
            _scaleFactor = Mathf.Clamp01(_scaleFactor - speed * Time.deltaTime);
        }

        transform.localScale = Vector3.Slerp(NormalScale, LargeScale, _scaleFactor);
    }
}
