using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machete : Item {

    [SerializeField] private string rightHandBoneName;
    private float hitDuration = 0.0f; 

    public override GameObject Use() {
        // GameObject[] animals = GameObject.FindGameObjectsWithTag("animal");
        // foreach (GameObject animal in animals) {
        //     Debug.Log(Vector3.Distance(animal.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
        //     if (Vector3.Distance(animal.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 2.0f) {
        //         return animal.GetComponent<Animal>().TakeDamage(1);
        //     }
        // }

        if (currentDurability <= 0.0f) return null;
        if(hitDuration > 0.0f) return null;

        hitDuration = 1.0f;
        currentDurability = Mathf.Clamp(currentDurability - durabilityDmgPerHit, 0.0f, maxDurability);
        
        return null;
        
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "animal" && hitDuration > 0.0f){
            Debug.Log("Punch");
            GameObject animal = other.gameObject;
            animal.GetComponent<Animal>().TakeDamage(1);
            hitDuration = 0.0f;
        }
    }


    void Start() {
        hitDuration = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if(hitDuration > 0.0f){
            hitDuration -= Time.deltaTime;
        }
    }
}

public class FMath {
}
