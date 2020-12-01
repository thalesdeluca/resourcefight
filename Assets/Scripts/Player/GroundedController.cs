using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : MonoBehaviour {
  public bool isGrounded = false;
  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.layer == 8) {
      isGrounded = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.layer == 8) {
      isGrounded = false;
    }
  }
}
