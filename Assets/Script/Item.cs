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
  public GameObject objAfterPan;
  public float panTime;
  public bool canPot;
  public GameObject objAfterPot;
  public float potTime;
  public bool canCut;
  public GameObject objAfterCut;

  public bool canBeTrash;

  public Vector3 getHoldPosition() {
    if (isTool) {
      return new Vector3(0.75f, 0, 0);
    } else {
      return new Vector3(0, 0, 0.75f);
    }
  }

  public virtual GameObject Use() {
    Debug.Log("Item used but not implemented");
    return null;
  }

  void Start() {

  }

  void Update() {

  }

  public void Reset(){
    canPan = canPot = canCut = false;
  }
}
