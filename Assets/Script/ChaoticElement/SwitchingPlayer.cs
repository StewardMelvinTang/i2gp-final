using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchingPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;
    [SerializeField] private float MaxTimer;
    [SerializeField] private float timer;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        timer = MaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0.0f){
            Debug.Log("Swaping...");
            timer = MaxTimer;
            GameObject temp = player1.Swap(null);
            GameObject temp2 = player2.Swap(temp);
            player1.Swap(temp2);

            Vector3 tempPosition = player1.transform.position;
            Quaternion tempRotation = player1.transform.rotation;

            player1.transform.position = player2.transform.position;
            player1.transform.rotation = player2.transform.rotation;

            player2.transform.position = tempPosition;
            player2.transform.rotation = tempRotation;
        }
        else if(timer <= 1.0f){
            text.text = "1";
        }
        else if(timer <= 2.0f){
            text.text = "2";
        }
        else if(timer <= 3.0f){
            text.text = "3";
        }
        else {
            text.text = "";
        }
    }
}
