using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crate : Table
{
    void Start(){
        tableRenderer = GetComponent<MeshRenderer>();
        if (tableRenderer != null)
        {
            originalMetallic = tableRenderer.material.GetFloat("_Metallic"); // Store original metallic value
        }
    }
    
    public override GameObject PutItem(GameObject gameObject){
        Destroy(gameObject);
        return null;
    }
    
    public override GameObject TakeItem(){
        GameObject returnItem = Instantiate(foodObject);
        return returnItem;
    }
}
