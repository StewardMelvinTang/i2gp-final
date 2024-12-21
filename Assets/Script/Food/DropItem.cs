using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour{

    private GameObject dropObject;
    private bool isTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitObject(GameObject objectInit){
        dropObject =  Instantiate(objectInit, this.gameObject.transform);
        // Debug.Log(dropObject);
        dropObject.transform.localPosition = new Vector3(0, 0.5f, 0);
        dropObject.transform.localRotation = Quaternion.Euler(45, 0, 0);
    }

    public GameObject TakeItem () {
        if (isTaken) return null; // Prevent multiple calls
        isTaken = true; // Mark as taken

        if (dropObject != null)
        {
            dropObject.transform.SetParent(null);
            dropObject.transform.localRotation = Quaternion.identity;
        }

        Destroy(this.gameObject);

        return dropObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 10f * Time.deltaTime, 0f);
    }
}
