//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        // Destroy the explosion when particles are finished.
        var particleSystemComponent = gameObject.GetComponent<ParticleSystem>();
        var delay = particleSystemComponent.duration + particleSystemComponent.startLifetime;
        Destroy(gameObject, delay);
    }
}