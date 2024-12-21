using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteCrate : Table
{
    private StackFood stack;
    // private String name;

    int idx = 0;

    protected void Start()
    {
        idx = 0;
        tableRenderer = GetComponent<MeshRenderer>();
        if (tableRenderer != null)
        {
            originalMetallic = tableRenderer.material.GetFloat("_Metallic"); // Store original metallic value
        }

        foodObject = new GameObject("Empty");
        stack = foodObject.AddComponent<StackFood>();
        foodObject.transform.SetParent(transform);
        foodObject.transform.localPosition = new Vector3(0, -100, 0);
    }

    public override GameObject PutItem(GameObject gameObject){
        if(gameObject == null) return null;
        if(idx >= 8) return gameObject;
        stack.InsertFood(gameObject);
        idx++;
        gameObject.transform.rotation = Quaternion.Euler(-45f, 0f, 0f);
        if(idx == 1) gameObject.transform.localPosition = new Vector3(0.345f, 100.2f, 0.4f);
        else if(idx == 2) gameObject.transform.localPosition = new Vector3(0.345f, 100.2f, 0.1f);
        else if(idx == 3) gameObject.transform.localPosition = new Vector3(0.345f, 100.2f, -0.2f);
        else if(idx == 4) gameObject.transform.localPosition = new Vector3(0.345f, 100.2f, -0.5f);
        else if(idx == 5) gameObject.transform.localPosition = new Vector3(-0.345f, 100.2f, 0.4f);
        else if(idx == 6) gameObject.transform.localPosition = new Vector3(-0.345f, 100.2f, 0.1f);
        else if(idx == 7) gameObject.transform.localPosition = new Vector3(-0.345f, 100.2f, -0.2f);
        else if(idx == 8) gameObject.transform.localPosition = new Vector3(-0.345f, 100.2f, -0.5f);
        return null;
    }
    
    public override GameObject TakeItem(){
        GameObject returnItem = stack.Pop();
        if (returnItem != null)
        {
            returnItem.transform.rotation = Quaternion.identity;
            idx--;
        }
        return returnItem;
    }
}
