using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {
        Camera targetCamera = Camera.main;
        if (targetCamera != null) {
            // Calculate the direction vector from the Canvas to the Camera
            Vector3 direction = targetCamera.transform.position - transform.position;

            // Make the Canvas face the camera by aligning its forward vector
            // direction.y *= -1;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
