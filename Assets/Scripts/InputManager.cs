using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
  private MovementScript movementScript;

  void Start() {
    movementScript = GetComponent<MovementScript>();
  }

  public void Move(InputAction.CallbackContext context) {
    movementScript.Move(context);

  }

  public void Jump(InputAction.CallbackContext context) {
    movementScript.Jump(context);
  }

  public void Dash(InputAction.CallbackContext context) {

  }

  public void Attack(InputAction.CallbackContext context) {

  }

  public void Block(InputAction.CallbackContext context) {

  }
}
