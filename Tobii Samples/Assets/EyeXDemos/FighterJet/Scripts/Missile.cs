//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Missile : MonoBehaviour
{
    private Enemy _enemy;
    private Renderer _renderer;
    private const float _scale = 0.35f;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Make sure we got an enemy.
        if (_enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the missile towards the enemy ship.
        var speed = Time.deltaTime * 10f;
        transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, speed);

        // Scale the missile (and flip it if necessary).
        var isLeft = transform.position.x < 0;
        transform.localScale = new Vector3(isLeft ? _scale : -_scale, _scale, _scale);

        // Reached center?
        var dist = Vector3.Distance(_renderer.bounds.center, _enemy.Bounds.center);
        if (Mathf.Abs(dist) < 0.5f)
        {
            // Set the enemy health to zero.
            _enemy.Health = 0f;

            // Destroy the missile.
            Destroy(gameObject);
        }
    }

    public void SetEnemyTarget(Enemy enemy)
    {
        _enemy = enemy;
    }
}
