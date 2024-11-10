using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public GameObject dropItem;
    public int health = 1;

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Instantiate(dropItem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Start() {
        
    }

    void Update() {

    }
}
