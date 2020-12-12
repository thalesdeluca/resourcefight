using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;


enum PlayerState {
  Attacking,
  Knockback,
  Moving,
  Jumping
}
public class PlayerScript : MonoBehaviour {

  void Start() {
    this.gameObject.name = "Player";
  }

  // Update is called once per frame
  void Update() {

  }
}
