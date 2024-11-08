using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /* 
        #===================================#
        #=========[PLAYER MOVEMENT]=========#
        #===================================#
    */
    void Update()
    {
        UpdatePlayerMovement();
    }

    void UpdatePlayerMovement(){
        Vector3 moveVector = new Vector3(0.0f, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W)) moveVector.x += 2.5f * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) moveVector.x -= 2.5f * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) moveVector.z += 2.5f * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) moveVector.z -= 2.5f * Time.deltaTime;
        transform.position += moveVector;
    }
}
