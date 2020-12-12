using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour {

  public Image dialogue;

  void Update() {
    if (dialogue.enabled) {
      this.transform.localScale = Vector3.zero;
    } else {
      this.transform.localScale = new Vector3(1, 1, 1);
    }
  }
}
