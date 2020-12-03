using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour {
  // Start is called before the first frame update
  private Rigidbody2D rigidbody;
  private Vector2 direction = new Vector2(1, 0);
  private bool moving = true;
  public Vector2 Direction { get { return direction; } }

  [SerializeField]
  private float speed = 10f;
  [SerializeField]
  private float jumpForce = 4f;

  private SpriteRenderer sprite;

  private Element element;

  private GameObject attackPoint;
  private float lastDirection;

  public float AttackDirectionX { get { return lastDirection; } }

  void Start() {
    rigidbody = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    element = GetComponent<Element>();
    attackPoint = GameObject.Find("AttackPoint");
  }

  void Update() {
    if (moving) {
      rigidbody.velocity = new Vector2(direction.x * speed, rigidbody.velocity.y);
      if (!element.Executing) {
        sprite.flipX = direction.x < 0;

        var directionAttack = direction.normalized.x > 0 ? 1 : -1;
        if (lastDirection != 0) {
          if (lastDirection != directionAttack) {
            attackPoint.transform.parent.rotation = Quaternion.Euler(0, directionAttack > 0 ? 0 : 180, 0);
          }
        }
        lastDirection = directionAttack;
      } else {
        direction.y = 0;
      }
    }
  }

  public void Move(InputAction.CallbackContext ctx) {

    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    moving = false;
    if (ctx.performed) {
      if (ctx.ReadValue<Vector2>() != Vector2.zero) {
        moving = true;
        direction = ctx.ReadValue<Vector2>();
      }
    }


  }

  public void Jump(InputAction.CallbackContext ctx) {
    var isGrounded = this.transform.Find("Grounded").GetComponent<GroundedController>().isGrounded;
    if (ctx.performed && isGrounded) {
      rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
  }
}
