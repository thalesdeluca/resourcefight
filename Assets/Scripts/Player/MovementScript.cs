using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour {
  // Start is called before the first frame update
  private Rigidbody2D rigidbody;
  private float direction = 0;

  [SerializeField]
  private float speed = 10f;
  [SerializeField]
  private float jumpForce = 4f;

  private SpriteRenderer sprite;

  void Start() {
    rigidbody = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
  }

  void Update() {
    if (direction != 0) {
      rigidbody.velocity = new Vector2(direction * speed, rigidbody.velocity.y);
      sprite.flipY = direction < 0;
    }

  }

  public void Move(InputAction.CallbackContext ctx) {

    direction = 0;
    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

    if (ctx.performed) {
      Debug.Log("Moving");
      direction = ctx.ReadValue<Vector2>().x;
    }
  }

  public void Jump(InputAction.CallbackContext ctx) {
    var isGrounded = this.transform.Find("Grounded").GetComponent<GroundedController>().isGrounded;
    if (ctx.performed && isGrounded) {
      rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
  }
}
