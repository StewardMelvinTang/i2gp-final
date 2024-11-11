using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float raycastRange = 1f;
    [SerializeField] private float holdDistance = 1.0f;

    private Table lastHitTable;
    private GameObject holdItem;
    private StackFood backpack;
    private float backpackDelay;


    /*
        MOVE        : WASD
        INTERACT    : F
    */

    void Start() 
    {
        holdItem = null;

        GameObject backpackObject = new GameObject("Backpack");
        backpackObject.transform.SetParent(transform);
        backpack = backpackObject.AddComponent<StackFood>();
        backpack.transform.localPosition = new Vector3(0, 0, -1);
        backpackDelay = 0.0f;
    }

    void Update()
    {
        /* Timer */
        if(backpackDelay > 0.0f){
            backpackDelay -= Time.deltaTime;
        }
        /* Update Function */
        // UpdatePlayerMovement();
        RayCastObject();
        UseItem();
        
    }

    // void FixedUpdate() {
    //     UpdatePlayerMovement();
    // }

    // private void UpdatePlayerMovement()
    // {
    //     Vector3 moveVector = new Vector3(0.0f, 0.0f, 0.0f);

    //     if (Input.GetKey(KeyCode.W)) moveVector.z += 1.0f;
    //     if (Input.GetKey(KeyCode.S)) moveVector.z -= 1.0f;
    //     if (Input.GetKey(KeyCode.A)) moveVector.x -= 1.0f;
    //     if (Input.GetKey(KeyCode.D)) moveVector.x += 1.0f;

    //     if (moveVector != Vector3.zero)
    //     {
    //         transform.position += moveVector.normalized * movementSpeed * Time.deltaTime;
    //         Quaternion targetRotation = Quaternion.LookRotation(moveVector);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    //     }
    //     // Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    //     // if (moveVector.magnitude > 0.1f)
    //     // {
    //     //     moveVector = moveVector.normalized;
    //     //     transform.position += moveVector * movementSpeed * Time.deltaTime;

    //     //     Quaternion targetRotation = Quaternion.LookRotation(moveVector);
    //     //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    //     // }
    // }



    private void RefillCrate(FiniteCrate crate){
        if (Input.GetKey(KeyCode.F) && backpack.foodStack.Count > 0){
            if(backpackDelay <= 0.0f){
                GameObject obj = backpack.Pop();
                crate.PutItem(obj);
                backpackDelay = 0.1f;
            }
        }
        else{
            ObjectInteract(crate);
        }
    }

    private void ObjectInteract(Table table)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject item = null;
            
            if (holdItem)
            {
                holdItem.transform.SetParent(null);
                item = table.PutItem(holdItem);
                holdItem = item;
                if (item) Debug.Log("Object Interaction returns an output (it failed putting) : " + item.name);
            }
            else
            {
                item = table.TakeItem();
            }

            if (item != null)
            {
                holdItem = item;
                holdItem.transform.SetParent(transform); 
                holdItem.transform.localPosition = item.GetComponent<Item>().getHoldPosition();
            }
        }
    }

    private void UseItem() {
        Item item = null;

        if (holdItem) {
            item = holdItem.GetComponent<Item>();
        }

        if (Input.GetKeyDown(KeyCode.E) && item && item.isTool) {
            GameObject droppedObj = holdItem.GetComponent<Item>().Use();
            if (droppedObj) {
                GameObject obj = Instantiate(droppedObj);
                // obj.transform.SetParent(gameObject.transform);
                backpack.InsertFood(obj);
            }
        }
    }

    /*
        #==================================================#
        #========= You can ignore below functions =========#
        #==================================================#
    */
    private void RayCastObject()
    {
        Ray ray = new Ray(transform.position, transform.forward + Vector3.down * 0.5f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            Table table = hit.collider.GetComponent<Table>();
            FiniteCrate finiteCrate = hit.collider.GetComponent<FiniteCrate>();

            if (table != null)
            {
                /* Highlighted */
                table.SetHighlighted(true);
                if (lastHitTable != null && lastHitTable != table)
                {
                    lastHitTable.SetHighlighted(false);
                }
                lastHitTable = table;

                /* Interact */
                if(finiteCrate){
                    RefillCrate(finiteCrate);
                }
                else{
                    ObjectInteract(table);
                }
            }
            else if (lastHitTable != null)
            {
                lastHitTable.SetHighlighted(false);
                lastHitTable = null;
            }
        }
        else if (lastHitTable != null)
        {
            lastHitTable.SetHighlighted(false);
            lastHitTable = null;
        }
    }
}