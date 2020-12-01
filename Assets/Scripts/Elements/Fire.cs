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
  private bool blocked = false;
  [SerializeField]
  private float blockCooldown = 1f;
  private float blockTime = 0;

  [SerializeField]
  private bool dashed = false;
  [SerializeField]
  private float dashCooldown = 1f;
  private float dashTime = 0;
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (attacked && attackTime >= attackCooldown) {
      attacked = false;
      attackTime = 0;
    } else if (attacked) {
      attackTime += Time.deltaTime;
    }

    if (blocked && blockTime >= blockCooldown) {
      blocked = false;
      blockTime = 0;
    } else if (blocked) {
      blockTime += Time.deltaTime;
    }

    if (dashed && dashTime >= dashCooldown) {
      dashed = false;
      dashTime = 0;
    } else if (dashed) {
      dashTime += Time.deltaTime;
    }
  }


  public override void Attack(InputAction.CallbackContext context) {
    if (!attacked && !blocked) {
      var attackPoint = GameObject.Find("AttackPoint").GetComponent<Transform>();
      Physics2D.OverlapBox(attackPoint.position, new Vector2(AttackRange, SIZE), 0);
      attacked = true;
    }

  }

  public override void Block(InputAction.CallbackContext context) {
    if (!blocked) {
      var attackPoint = GameObject.Find("AttackPoint").GetComponent<Transform>();
      Physics2D.OverlapCircle(this.transform.position, BlockRange);
      blocked = true;
    }
  }

  public override void Dash(InputAction.CallbackContext context) {
    if (!dashed && !blocked) {
      var rigidbody = GetComponent<Rigidbody2D>();
      rigidbody.velocity = new Vector2(rigidbody.velocity.x * DashRange, 0);
      dashed = true;
    }
  }


  void OnDrawGizmos() {
    Gizmos.color = Color.red;

    if (attacked) {
      var attackPoint = GameObject.Find("AttackPoint").GetComponent<Transform>();
      Gizmos.DrawCube((Vector3)attackPoint.position + new Vector3(AttackRange / 2f, 0, 0), new Vector3(AttackRange, SIZE, 0));
    }

  }
}
