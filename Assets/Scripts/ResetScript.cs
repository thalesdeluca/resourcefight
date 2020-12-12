using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour {

  private Vector2 initialPosition;
  void Start() {
    initialPosition = this.transform.position;
  }
  // Start is called before the first frame update
  public void ResetPosition() {


    this.transform.position = initialPosition;

  }
}


