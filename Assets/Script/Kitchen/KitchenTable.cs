using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTable : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] protected bool infiniteItem = true;
    [SerializeField] protected GameObject foodObject;

    protected MeshRenderer tableRenderer;
    protected float originalMetallic;

    void Start()
    {
        tableRenderer = GetComponent<MeshRenderer>();
        if (tableRenderer != null)
        {
            originalMetallic = tableRenderer.material.GetFloat("_Metallic"); // Store original metallic value
        }
        if(foodObject != null && infiniteItem == false){
            foodObject = Instantiate(foodObject);
            foodObject.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
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
    public virtual GameObject PutItem(GameObject gameObject){
        if(infiniteItem){
            Destroy(gameObject);
            return null;
        }
        GameObject ret = foodObject;
        if(foodObject) Destroy(foodObject);
        foodObject = gameObject;
        foodObject.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);

        return ret;
    }
    
    // To Take Item From the Desk or Supplies
    // TODO: If can put multiple things
    public virtual GameObject TakeItem(){
        GameObject returnItem = foodObject;
        if (!infiniteItem) { 
            Destroy(foodObject);
            foodObject = null;
        }
        return returnItem;
    }
}
