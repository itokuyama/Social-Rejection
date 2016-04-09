using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public int health;
    public int totalHealth;
    public float speed;
    private GameObject target;

	// Use this for initialization
	void Start ()
    {
        health = totalHealth; //Sets health to max value
        target = GameObject.FindWithTag("Tower"); //Sets the target destination as the tower
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = new Vector3(target.transform.position.x - gameObject.transform.position.x, 0, 0); //Sets enemy to always be travelling toward the tower

        transform.Translate(direction.normalized*speed*Time.deltaTime); //The enemy travels towards the tower at a designated speed

        if (health <= 0)
        {
            Destroy(gameObject); //Enemy dies
        }

        Destroy(gameObject, 3);
	}

    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            health -= 1; //Enemy gets hurt
        }
    }
}
