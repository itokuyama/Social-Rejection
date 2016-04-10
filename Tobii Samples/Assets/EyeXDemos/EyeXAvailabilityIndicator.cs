//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

[RequireComponent(typeof(EyeXEngineAvailabilityComponent))]
public class EyeXAvailabilityIndicator : MonoBehaviour
{
    void Start()
    {
        var availabilityComponent = GetComponent<EyeXEngineAvailabilityComponent>();
        GetComponent<Renderer>().enabled = !availabilityComponent.IsEyeXAvailable;
    }

    void Update()
    {
        var pos = new Vector3(Screen.width - 50, Screen.height - 50, 10);
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }
}
