using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {
  private float fps = 0;
  private float time = 0;
  private Text text;
  // Start is called before the first frame update
  void Start() {
    text = GetComponent<Text>();
  }

  // Update is called once per frame
  void Update() {
    if (time >= 1) {
      text.text = "FPS: " + fps;
      time = 0;
      fps = 0;
    } else {
      fps++;
      time += Time.deltaTime;
    }
  }
}
