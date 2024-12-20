using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotTable : Table {

    [SerializeField] private GameObject colorPot;
    
    public float timeCounter;
    public bool startCounter { get; set; } 
    public float cookingTime { get; set; }

    private float offsetYObject = -10.0f;
    private Renderer colorRender;
    
    protected override void Start() {
        base.Start();
        colorRender = colorPot.GetComponent<Renderer>();
        colorRender.material.color = new Color(1f, 1f, 1f, 0.1f);
    }

    void Update(){
        if (startCounter) {
            timeCounter -= Time.deltaTime;
            if(timeCounter < 0.0f){
                startCounter = false;
                colorRender.material.color = new Color(1, 0, 0, 1f);
                Destroy(foodObject);
                foodObject = Instantiate(foodObject.GetComponent<Item>().objAfterPot);
                foodObject.transform.position = new Vector3(transform.position.x, offsetYObject, transform.position.z);
            
                if (foodObject.GetComponent<Item>().canPot) {
                    startCounter = true;
                    timeCounter = foodObject.GetComponent<Item>().potTime;
                    cookingTime = timeCounter;
                }
            }
        }
    }

    public override GameObject PutItem(GameObject targetObject) {

        Item targetItem = null;
        Item currentItem = null;
        
        if (targetObject != null) {
            targetItem = targetObject.GetComponent<Item>();
        }

        if (foodObject != null) {
            currentItem = foodObject.GetComponent<Item>();
        }

        if (targetObject != null && targetItem.canPot) {
            GameObject returnItem = foodObject;
            startCounter = true;
            timeCounter = targetItem.potTime;
            foodObject = targetObject;
            foodObject.transform.position = new Vector3(transform.position.x, offsetYObject, transform.position.z);
            cookingTime = timeCounter;
            return returnItem;
        }
        else {
            return targetObject;
        }
    }

    public override GameObject TakeItem(){
        GameObject returnItem = foodObject;
        foodObject = null;
        startCounter = false;
        colorRender.material.color = new Color(1f, 1f, 1f, 0.1f);
        return returnItem;
    }
}
