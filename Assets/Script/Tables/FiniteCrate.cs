using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteCrate : Table
{
    private StackFood stack;

    protected void Start()
    {
        tableRenderer = GetComponent<MeshRenderer>();
        if (tableRenderer != null)
        {
            originalMetallic = tableRenderer.material.GetFloat("_Metallic"); // Store original metallic value
        }

        foodObject = new GameObject("Empty");
        stack = foodObject.AddComponent<StackFood>();
        foodObject.transform.SetParent(transform);
        foodObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public override GameObject PutItem(GameObject gameObject){
        stack.InsertFood(gameObject);
        return null;
    }
    
    public override GameObject TakeItem(){
        GameObject returnItem = stack.Pop();
        return returnItem;
    }
}
