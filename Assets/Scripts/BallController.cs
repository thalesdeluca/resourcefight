using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

  private Rigidbody2D rigidbody;
  public int hits { get; private set; }

  private bool hit;
  [SerializeField]
  private float hitMaxTime;
  [SerializeField]
  private float hitTime = 0;

  private Vector2 direction = Vector2.zero;
  private Element element;

  private Collider2D lastCollider;

  void Start() {
    rigidbody = GetComponent<Rigidbody2D>();
    element = GameObject.Find("Player").GetComponent<Element>();
    hits = 0;
  }

  void Update() {
    if (hit && hitTime >= hitMaxTime) {
      hit = false;
      Time.timeScale = 1;
    } else if (hit) {
      Time.timeScale = 0.1f;
      hitTime += Time.unscaledDeltaTime;
    } else if (!hit && hitTime > 0) {
      hitTime -= Time.deltaTime;
      hitTime = 0;
    }
  }

  void Hit() {
    if (!hit && hitTime == 0) {
      hitTime = 0;
      hit = true;

      rigidbody.velocity = new Vector2(0, 0);
      var force = new Vector2(direction.x * element.Knockback, direction.y * element.Knockback);
      Debug.Log(force + " " + direction);
      rigidbody.AddForce(force, ForceMode2D.Impulse);
      hitMaxTime = element.Knockback / 15f;
      hits++;
    }

  }

  // Start is called before the first frame update
  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.layer == 10 && lastCollider != other) {
      lastCollider = other;
      if (element.DirectionAttack == Vector2.zero) {
        direction = this.transform.position - element.transform.position;
        direction *= element.Knockback;
        direction = direction.normalized;
      } else {
        direction = (Vector3)element.DirectionAttack;

      }
      Hit();
    }
  }
}
