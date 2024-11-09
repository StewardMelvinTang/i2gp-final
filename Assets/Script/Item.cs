using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

  [Header("Item Settings")]
  public string itemName;
  public bool isTool;
  public bool canPlate;
  public bool canBackpack;
  
  [Header("Process Settings")]
  public bool canPan;
  public GameObject afterPan;
  public bool canPot;
  public GameObject afterPot;
  public bool canCut;
  public GameObject afterCut;

  [HideInInspector]
  public Vector3 holdPosition;

  void Start() {
    if (isTool) {
      holdPosition = new Vector3(0.75f, 0.0f, 0.0f);
    }
    else {
      holdPosition = new Vector3(0.0f, 0.0f, 0.75f);
    }
  }

  void Update() {

  }
}
