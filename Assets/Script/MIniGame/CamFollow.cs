using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform sheep; // Reference to the sheep's transform
    private Vector3 offset; // Offset between the camera and the sheep

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the initial offset
        offset = transform.position - sheep.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Update the camera's position based on the sheep's X position only
        transform.position = new Vector3(sheep.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
