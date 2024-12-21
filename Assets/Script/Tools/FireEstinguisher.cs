using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEstinguisher : Item {

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
        hitDuration = 1.0f;
        return null;
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
