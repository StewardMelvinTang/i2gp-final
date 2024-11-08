using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTable : MonoBehaviour
{
    private MeshRenderer tableRenderer;
    private float originalMetallic;

    void Start()
    {
        tableRenderer = GetComponent<MeshRenderer>();
        if (tableRenderer != null)
        {
            originalMetallic = tableRenderer.material.GetFloat("_Metallic"); // Store original metallic value
        }
    }

    void Update(){

    }

    public void SetHighlighted(bool highlighted)
    {
        if (tableRenderer != null)
        {
            tableRenderer.material.SetFloat("_Metallic", highlighted ? 0.8f : originalMetallic); // Adjust metallic value
        }
    }
}
