//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using System;
using UnityEngine;
using SystemRandom = System.Random;

[RequireComponent(typeof(ActivatableComponent))]
public class Enemy : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private SystemRandom _randomizer;
    private float _elapsed;
    private float _targetRotation;
    private bool _isBeingLookedAt;
    private bool _isActivated;
    private Action<Enemy> _destroyedAction = enemy => { };

    private ActivatableComponent _activatableComponent;
    private Renderer _rendererComponent;

    public float Health = 100f;

    public bool IsBeingLookedAt
    {
        get { return _isBeingLookedAt; }
    }

    public bool IsActivated
    {
        get { return _isActivated; }
    }

    public Bounds Bounds
    {
        get { return _rendererComponent.bounds; }
    }

    void Start()
    {
        _activatableComponent = GetComponent<ActivatableComponent>();
        _rendererComponent = GetComponent<Renderer>();
    }

    void Awake()
    {
        _randomizer = new SystemRandom(DateTime.Now.Millisecond);
        _elapsed = 0.0f;
        _startPosition = transform.position;
        _targetRotation = transform.rotation.z;

        // Start outside of the camera.
        transform.position = new Vector3(_startPosition.x < 0 ? -100 : 100, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // Are we dead?
        if (Health <= 0f)
        {
            _destroyedAction(this);
            return;
        }

        // Are we being looked at?
        _isBeingLookedAt = _activatableComponent.HasActivationFocus;

        // Have we been activated?
        _isActivated = _activatableComponent.IsActivated;

        // Change target position every now and then.
        _elapsed -= Time.deltaTime;
        if (_elapsed <= 0f)
        {
            // Randomize a new start position.
            var deltaX = _randomizer.Next(-1, 2);
            var deltaY = _randomizer.Next(-1, 2);
            _targetPosition = _startPosition + new Vector3(deltaX * 0.5f, deltaY * 0.5f);

            // Randomize the time to next movement.
            _elapsed = _randomizer.Next(1, 3);

            // Randomize a new rotation angle.
            _targetRotation = _randomizer.Next(-30, 30);
        }

        // Lerp towards the target position (2 units/sec).
        var speed = Time.deltaTime * 2f;
        var diffX = Mathf.Lerp(transform.position.x, _targetPosition.x, speed);
        var diffY = Mathf.Lerp(transform.position.y, _targetPosition.y, speed);
        transform.position = new Vector3(diffX, diffY, transform.position.z);

        // Lerp towards the target rotation.
        var rotationSpeed = Time.deltaTime * 0.1f;
        var deltaRotation = Mathf.Lerp(transform.rotation.z, _targetRotation, rotationSpeed);
        transform.Rotate(transform.rotation.x, transform.rotation.y, deltaRotation);
    }

    public void SetDestroyCallback(Action<Enemy> callback)
    {
        _destroyedAction = callback;
    }
}
