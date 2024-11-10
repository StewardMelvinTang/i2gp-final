using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 2.5f;
    [SerializeField] private float raycastRange = 1f;
    [SerializeField] private float holdDistance = 1.0f;

    private Table lastHitTable;
    private GameObject holdItem;
    private Stack<GameObject> backpack;

    /*
        MOVE        : WASD
        INTERACT    : F
    */

    void Start()
    {
        holdItem = null;
        backpack = new Stack<GameObject>();
    }

    void Update()
    {
        UpdatePlayerMovement();
        RayCastObject();
        UseItem();
    }

    private void UpdatePlayerMovement()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveVector.magnitude > 0.1f)
        {
            moveVector = moveVector.normalized;
            transform.position += moveVector * movementSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
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
                obj.transform.SetParent(gameObject.transform);
                backpack.Push(droppedObj);
                obj.transform.localPosition = new Vector3(0, 0, -1 * backpack.Count);
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
                ObjectInteract(table);
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