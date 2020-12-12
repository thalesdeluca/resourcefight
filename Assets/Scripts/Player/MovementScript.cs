using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour {
  // Start is called before the first frame update
  private Rigidbody2D rigidbody;
  private Vector2 direction = new Vector2(0, 0);
  private bool moving = true;
  public Vector2 Direction { get { return direction; } }

  [SerializeField]
  private float speed = 10f;
  [SerializeField]
  private float airSpeed = 4f;
  [SerializeField]
  private float jumpForce = 4f;

  private SpriteRenderer sprite;

  private Element element;

  private GameObject attackPoint;
  private float lastDirection;

  public float AttackDirectionX { get { return lastDirection; } }

  private GroundedController grounded;

  void Start() {
    rigidbody = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    element = GetComponent<Element>();
    attackPoint = GameObject.Find("AttackPoint");
    grounded = this.transform.Find("Grounded").GetComponent<GroundedController>();
  }

  void Update() {

    if (moving) {
      if (!element.Executing) {
        sprite.flipX = direction.x < 0;

        var directionAttack = direction.normalized.x > 0 ? 1 : -1;
        if (lastDirection != 0) {
          if (lastDirection != directionAttack) {
            attackPoint.transform.parent.rotation = Quaternion.Euler(0, directionAttack > 0 ? 0 : 180, 0);
          }
        }
        lastDirection = directionAttack;
        var targetSpeed = grounded.isGrounded ? speed : airSpeed;
        rigidbody.velocity = new Vector2(direction.x * targetSpeed, rigidbody.velocity.y);
        if (grounded.isGrounded && rigidbody.velocity != Vector2.zero) {
          this.gameObject.GetComponent<Animator>().Play("walk");
        }
      }

    } else if (!element.Executing) {
      rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

      if (grounded.isGrounded) {
        this.gameObject.GetComponent<Animator>().Play("idle");

      }

    }
  }

  public void ResetYDirection() {
    direction.y = 0;
  }

  public void Move(InputAction.CallbackContext ctx) {
    moving = false;
    if (ctx.performed) {
      if (ctx.ReadValue<Vector2>() != Vector2.zero) {
        moving = true;
        direction = ctx.ReadValue<Vector2>();
      }
    }


  }

  public void Jump(InputAction.CallbackContext ctx) {
    if (ctx.performed && grounded.isGrounded) {
      rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
  }
}
