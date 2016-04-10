//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Cockpit : MonoBehaviour
{
    private List<Enemy> _enemies;
    private GameObject _reticule;
    private Missile _missile;
    private Enemy _lockedOnEnemy;
    private MissileTargetIndicator _target;
    private SpriteRenderer _gunFireRenderer;
    private ParticleSystem _gunFireParticleSystem;
    private Renderer _reticuleRenderer;

    public Enemy EnemyPrefab;
    public Missile MissilePrefab;
    public Explosion ExplosionPrefab;
    private EyeXHost _eyexHost;

    private void Start()
    {
        _eyexHost = EyeXHost.GetInstance();

        _enemies = new List<Enemy>();
        _reticule = GameObject.Find("Reticule");
        _reticuleRenderer = _reticule.GetComponent<Renderer>();
        _gunFireRenderer = GameObject.Find("Gunfire").GetComponent<SpriteRenderer>();
        _gunFireParticleSystem = GameObject.Find("GunfireParticles").GetComponent<ParticleSystem>();
        _target = GameObject.Find("Target").GetComponent<MissileTargetIndicator>();
    }

    private void Update()
    {
        // Update the camera position.
        UpdateCamera();

        // Update the missile target indicator.
        UpdateMissileTargetIndicator();

        // Update weapon input.
        UpdateWeaponInput();

        // Not enough enemies left?
        if (_enemies.Count < 2)
        {
            SpawnNewEnemies();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _eyexHost.TriggerActivation();
        }
    }

    private void UpdateCamera()
    {
        var moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            moveVector = new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveVector = new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveVector += new Vector3(0, 1, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVector += new Vector3(0, -1, 0);
        }

        // Update the camera position.
        var moveSpeed = Time.deltaTime * 5f;
        Camera.main.transform.position += moveVector * moveSpeed;
    }

    private void UpdateMissileTargetIndicator()
    {
        // Get the enemy that the player is looking at.
        var enemyInFocus = _enemies.FirstOrDefault(x => x.IsBeingLookedAt || x.IsActivated);

        // Is the playing looking at a ship?
        if (enemyInFocus != null)
        {
            if (_missile == null)
            {
                // Set target since no missile has been launched.
                _lockedOnEnemy = enemyInFocus;

                // Set the target state (locked or not locked).
                _target.SetState(enemyInFocus.IsActivated);
            }
        }
        else
        {
            if (_missile == null)
            {
                // The player is not looking at the target and
                // no missile has been fired. Hide the target.
                _target.Hide();
            }
        }

        if (_lockedOnEnemy != null)
        {
            // Update the position of the target.
            _target.transform.position = _lockedOnEnemy.Bounds.center;
        }
    }

    private void UpdateWeaponInput()
    {
        // Firing machine gun?
        if (Input.GetMouseButton(0))
        {
            if (!_gunFireRenderer.enabled)
            {
                _gunFireRenderer.enabled = true;
            }

            // Fire the machine gun.
            var didHitTarget = FireMachineGun();

            // Play or stop particles depending on if we hit.
            if (didHitTarget)
            {
                _gunFireParticleSystem.Play();
            }
            else
            {
                _gunFireParticleSystem.Stop();
            }
        }
        else
        {
            _gunFireRenderer.enabled = false;
            _gunFireParticleSystem.Stop();
        }

        // Determine if we should fire a missile.
        var isActivated = _lockedOnEnemy != null && _lockedOnEnemy.IsActivated;
        var canFire = _missile == null;
        if (isActivated && canFire)
        {            
            // Fire a missile.
            FireMissile(_lockedOnEnemy);
        }
    }

    private void SpawnNewEnemies()
    {
        while (_enemies.Count < 2)
        {
            // Calculate the enemy position depending on other existing enemy.
            var hasLeft = _enemies.Any(x => x.transform.position.x < 0);
            var position = hasLeft ? new Vector3(3, 1) : new Vector3(-4, 1);

            // Spawn the enemy.
            var enemy = Instantiate(EnemyPrefab, position, Quaternion.identity) as Enemy;
            enemy.SetDestroyCallback(OnEnemyDestroyed);

            // Add the enemy to the list.
            _enemies.Add(enemy);
        }
    }

    private void FireMissile(Enemy enemy)
    {
        // Get the desired start position for the missile.
        var isOnLeft = enemy.transform.position.x < 0;
        var position = isOnLeft ? new Vector3(-10, -8, 0) : new Vector3(10, -8, 0);

        // Set the scale (to flip the missile).
        var scale = isOnLeft ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

        // Create the missile and set the target.
        _missile = Instantiate(MissilePrefab, position, Quaternion.identity) as Missile;
        _missile.transform.localScale = scale;
        _missile.SetEnemyTarget(enemy);
    }

    private bool FireMachineGun()
    {
        // Got any enemies?
        if (_enemies.Count > 0)
        {
            var center = _reticuleRenderer.bounds.center;
            var hitEnemy = _enemies.FirstOrDefault(x => x.Bounds.Contains(center));
            if (hitEnemy != null && hitEnemy.Health > 0f)
            {
                // Decrease health by 100 HP/second.
                hitEnemy.Health -= Time.deltaTime * 100;

                // We hit a target.
                return true;
            }
        }

        // We did not hit anything.
        return false;
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        // Spawn an explosion.
        Instantiate(ExplosionPrefab, enemy.transform.position, Quaternion.identity);

        // Remove enemy from list.
        _enemies.Remove(enemy);

        // Destroy the enemy object.
        Destroy(enemy.gameObject);
    }
}
