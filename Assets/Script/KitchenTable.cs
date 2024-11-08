using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTable : MonoBehaviour
{
    [SerializeField] private bool infiniteItem = true;
    [SerializeField] private GameObject foodObject;

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

    // Put Items
    public void PutItem(GameObject gameObject){
        if(infiniteItem){
            Destroy(gameObject);
            return;
        }
        foodObject = gameObject;
        foodObject.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }
    
    // To Take Item From the Desk or Supplies
    // TODO: If can put multiple things
    public GameObject TakeItem(){
        GameObject returnItem = foodObject;
        if (!infiniteItem) { 
            Destroy(foodObject);
            foodObject = null;
        }
        return returnItem;
    }
}
