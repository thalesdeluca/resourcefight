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
  private float attackImpulse = 4f;

  [SerializeField]
  private bool blocked = false;
  [SerializeField]
  private float blockCooldown = 1f;
  private float blockTime = 0;

  [SerializeField]
  private bool dashed = false;
  [SerializeField]
  private bool throwed = false;
  private Vector2 throwPoint = Vector2.zero;
  [SerializeField]
  private float dashCooldown = 1f;
  private float dashTime = 0;

  private MovementScript movement;
  private Rigidbody2D rigidbody;
  private Vector2 directionAttack;
  private float angle = 0;

  private GameObject effect;
  // Start is called before the first frame update
  void Start() {
    movement = GetComponent<MovementScript>();
    rigidbody = GetComponent<Rigidbody2D>();

  }

  // Update is called once per frame
  void Update() {
    if (attacked && attackTime >= attackCooldown) {
      CancelAttack();
    } else if (attacked) {
      attackTime += Time.deltaTime;
      effect.transform.localScale = new Vector3(SIZE, attackTime * SIZE * 2, 1);
      Debug.Log(directionAttack.normalized * (-attackImpulse));
    }

    if (blocked && blockTime >= blockCooldown) {
      CancelBlock();
    } else if (blocked) {
      blockTime += Time.deltaTime;
      if (!throwed) {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
      }
    }

    if (throwed) {
      effect.transform.position = Vector2.MoveTowards(effect.transform.position, throwPoint, blockTime);

      if ((Vector2)effect.transform.position == throwPoint) {
        CancelBlock();
      }
    }

    if (dashed && dashTime >= dashCooldown) {
      dashed = false;
      if (effect) {
        Destroy(effect);
      }
      executing = false;
      dashTime = 0;
    } else if (dashed) {
      dashTime += Time.deltaTime;

      rigidbody.MovePosition(throwPoint);
    }
  }

  void CancelAttack() {
    attacked = false;
    executing = false;
    if (effect) {
      Destroy(effect);
    }

    attackTime = 0;
  }

  void CancelBlock() {
    blocked = false;
    if (effect) {
      Destroy(effect);
    }
    executing = false;
    throwed = false;
    blockTime = 0;
  }


  public override void Attack(InputAction.CallbackContext context) {
    if (context.canceled || context.performed) {
      executing = false;
    }

    if (!attacked && !blocked) {
      if (context.started) {
        var teste = context.ReadValue<float>();

        var attackPoint = GameObject.Find("AttackPoint").GetComponent<Transform>();
        Vector3 position = attackPoint.position + (new Vector3(AttackRange / 2f, 0, 0));

        angle = Vector2.Angle(movement.Direction, Vector2.right);
        angle = angle > 90 ? angle % 90 : angle;
        directionAttack = movement.Direction;

        attackPoint.parent.rotation = Quaternion.Euler(0, directionAttack.x > 0 ? 0 : 180, directionAttack.y > 0 ? angle : -angle);
        effect = Instantiate(abilities.fireAttack, attackPoint.position, Quaternion.identity, attackPoint);
        effect.transform.localRotation = Quaternion.Euler(0, directionAttack.x > 180 ? 0 : 180, -90);
        effect.transform.localScale = Vector2.zero;

        rigidbody.AddForce(directionAttack.normalized * (-attackImpulse), ForceMode2D.Impulse);

        attacked = true;
        executing = true;
      }
    }
  }

  public override void Block(InputAction.CallbackContext context) {
    if (context.started) {
      if (!blocked) {

        if (effect) {
          Destroy(effect);
        }
        effect = Instantiate(abilities.fireBlock, this.transform.position, Quaternion.identity, this.transform);


        blocked = true;
        executing = true;
      } else if (!throwed && effect) {
        var attackPoint = GameObject.Find("AttackPoint").GetComponent<Transform>();

        rigidbody.AddForce(movement.Direction.normalized * (-attackImpulse / 2), ForceMode2D.Impulse);
        throwPoint = attackPoint.position + (new Vector3(AttackRange * movement.Direction.x, AttackRange * movement.Direction.y, 0));
        effect.transform.parent = null;
        blockTime = 0;
        throwed = true;
      }
    }

  }

  public override void Dash(InputAction.CallbackContext context) {
    if (context.canceled) {
      dashed = false;
      executing = false;
      if (effect) {
        Destroy(effect);
      }
      dashTime = 0;
    }

    if (context.started) {
      if (!dashed && !blocked) {
        var rigidbody = GetComponent<Rigidbody2D>();
        throwPoint = rigidbody.position + (new Vector2(DashRange * movement.Direction.x, DashRange * movement.Direction.y));
        Debug.Log(throwPoint);
        dashed = true;
        executing = true;
      }
    }

  }


  void OnDrawGizmos() {
    Gizmos.color = Color.red;
    if (blocked) {
      Gizmos.DrawSphere(this.transform.position, BlockRange);
    }


  }
}
