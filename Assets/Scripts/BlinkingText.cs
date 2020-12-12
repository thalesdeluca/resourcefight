using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;


public class BlinkingText : MonoBehaviour {

  public float blinkingTime = 1f;

  private float time = 0;

  public GameObject text;

  private float waitTime = 0;

  const float TIME_MAX = 4f;

  private void Start() {
    Time.timeScale = 1;
    text.SetActive(false);
  }

  void Update() {
    time += Time.deltaTime;

    if (waitTime < TIME_MAX) {
      waitTime += Time.deltaTime;
    } else {
      if (time >= blinkingTime) {
        time = 0;
        text.SetActive(!text.active);
      }

    }
  }

  public void Quit(InputAction.CallbackContext context) {
    Application.Quit();
  }
}