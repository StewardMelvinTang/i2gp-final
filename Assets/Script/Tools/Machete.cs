using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machete : Item {

    [SerializeField] private string rightHandBoneName;
    public override GameObject Use() {
        GameObject[] animals = GameObject.FindGameObjectsWithTag("animal");
        foreach (GameObject animal in animals) {
            Debug.Log(Vector3.Distance(animal.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
            if (Vector3.Distance(animal.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 2.0f) {
                return animal.GetComponent<Animal>().TakeDamage(1);
            }
        }
        return null;
    }


    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }
}
