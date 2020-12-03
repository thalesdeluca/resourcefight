using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : Element {
  const float SIZE = 0.5F;

  [SerializeField]
  private bool attacked = false;
  [SerializeField]
  private float attackCooldown = 1f;
  private float attackTime = 0;
  [SerializeField]
  private float attackImpulse = 6f;

  [SerializeField]
  private bool blocked = false;
  [SerializeField]
  private float blockCooldown = 1f;
  private float blockTime = 0;

  [SerializeField]
  private bool dashed = false;
  [SerializeField]
  private float dashCooldown = 1f;
  private float dashTime = 0;

  private MovementScript movement;
  private Rigidbody2D rigidbody;
  private Vector2 directionAttack;
  private float angle = 0;
  // Start is called before the first frame update
  void Start() {
    movement = GetComponent<MovementScript>();
    rigidbody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update() {
    if (attacked && attackTime >= attackCooldown) {
      attacked = false;
      executing = false;
      attackTime = 0;
    } else if (attacked) {
      attackTime += Time.deltaTime;

      rigidbody.AddForce(directionAttack.normalized * (-attackImpulse), ForceMode2D.Force);
    }

    if (blocked && blockTime >= blockCooldown) {
      blocked = false;
      executing = false;
      blockTime = 0;
    } else if (blocked) {
      blockTime += Time.deltaTime;
    }

    if (dashed && dashTime >= dashCooldown) {
      dashed = false;
      executing = false;
      dashTime = 0;
    } else if (dashed) {
      dashTime += Time.deltaTime;
    }
  }


  public override void Attack(InputAction.CallbackContext context) {
    if (!attacked && !blocked) {

      if (context.canceled || context.performed) {
        executing = false;
        attackTime = 0;
        attacked = false;
      }


      if (context.started) {
        var teste = context.ReadValue<float>();

        var attackPoint = GameObject.Find("AttackPoint").GetComponent<Transform>();
        Vector3 position = attackPoint.position + (new Vector3(AttackRange / 2f, 0, 0) * movement.AttackDirectionX);

        angle = Vector2.Angle(movement.Direction, Vector2.right);
        angle = angle > 90 ? angle % 90 : angle;
        directionAttack = movement.Direction;

        attackPoint.parent.rotation = Quaternion.Euler(0, directionAttack.x > 0 ? 0 : 180, directionAttack.y > 0 ? angle : -angle);


        attacked = true;
        executing = true;
      }
    }
  }

  public override void Block(InputAction.CallbackContext context) {
    if (!blocked) {
      var attackPoint = GameObject.Find("AttackPoint").GetComponent<Transform>();
      Physics2D.OverlapCircle(this.transform.position, BlockRange);
      blocked = true;
      executing = true;
    } else {
      //Instantiate attack Ring
    }
  }

  public override void Dash(InputAction.CallbackContext context) {
    if (!dashed && !blocked) {
      var rigidbody = GetComponent<Rigidbody2D>();
      rigidbody.velocity = new Vector2(rigidbody.velocity.x * DashRange, 0);
      dashed = true;
      executing = true;
    }
  }


  void OnDrawGizmos() {
    Gizmos.color = Color.red;
    if (blocked) {
      Gizmos.DrawSphere(this.transform.position, BlockRange);
    }


  }
}
