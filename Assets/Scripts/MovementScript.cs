using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour {
  // Start is called before the first frame update
  private Rigidbody2D rigidbody;

  public float speed = 10f;
  public float jumpForce = 4f;
  void Start() {
    rigidbody = GetComponent<Rigidbody2D>();
  }

  public void Move(InputAction.CallbackContext ctx) {
    if (ctx.canceled) {
      rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }

    if (ctx.performed) {
      rigidbody.velocity = new Vector2(ctx.ReadValue<Vector2>().x * speed, rigidbody.velocity.y);
    }
  }

  public void Jump(InputAction.CallbackContext ctx) {
    if (ctx.performed) {
      rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
  }
}
