﻿using UnityEngine;
using System.Collections;

public class EnemyFlying : MonoBehaviour {

    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public AudioClip hit4;

    public int health;
    public int totalHealth;
    public float speed;
    //private GameObject target;

    // Use this for initialization
    void Start()
    {
        health = totalHealth; //Sets health to max value
        //target = GameObject.FindWithTag("Tower"); //Sets the target destination as the tower

        PublicFunctions.PhaseThruTag(gameObject, new string[] { "Enemy" });
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 difference = target.transform.position - gameObject.transform.position;//Gets difference between self and target

        Vector3 direction = new Vector3(0, 1, 0); //Sets enemy to always be travelling toward the tower

        transform.Translate(direction.normalized * speed * Time.deltaTime); //The enemy travels towards the tower at a designated speed

        if (health <= 0)
        {
            Destroy(gameObject); //Enemy dies
            SoundManager.instance.RandomizeSfx(hit1, hit2, hit3, hit4);
        }

        PublicFunctions.PhaseThruTag(gameObject, new string[] { "Enemy" }); //Phase through other enemies
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            health -= 1; //Enemy gets hurt
        }
        if (other.gameObject.CompareTag("Tower"))
        {
            Destroy(gameObject); //Enemy dies when it reaches tower
        }
    }
}
