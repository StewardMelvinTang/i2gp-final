using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public GameObject dropItem;
    public int health = 1;

    public GameObject TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
            return dropItem;
        }
        return null;
    }

    void Start() {
        
    }

    void Update() {

    }
}
