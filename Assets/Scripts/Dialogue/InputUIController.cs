using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputUIController : MonoBehaviour {

  private PrologueController prologueController;

  void Start() {
    prologueController = GetComponent<PrologueController>();
  }

  public void Move(InputAction.CallbackContext context) {

  }

  public void onClick(InputAction.CallbackContext context) {
    if (context.started) {
      prologueController.Submit();
    }

  }
}