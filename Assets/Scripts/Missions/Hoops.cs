using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hoops : MissionComponent {

  private DialogueScript dialogue;

  private TMPro.TextMeshProUGUI timeText;

  private GameObject ball;


  public override void OnSucceed() {
    this.GetComponent<MissionController>().FinishMission(true, time);
  }

  // Start is called before the first frame update
  void Start() {
    Debug.Log(
      "StartHoops"
    );
    dialogue = this.GetComponent<DialogueScript>();
    dialogue.ChangeLine("hoops");
    dialogue.StartWriting();
    time = maxTime;
    ball = GameObject.Find("ball");
    timeText = GameObject.Find("Time").GetComponent<TMPro.TextMeshProUGUI>();
  }

  // Update is called once per frame
  void Update() {
    if (started) {
      time -= Time.deltaTime;
      timeText.text = Math.Round(time, 2) + "";

      if (time <= 0) {
        time = 0;
        started = false;
        this.GetComponent<MissionController>().FinishMission(false, time);
      }
    } else {
      Time.timeScale = 0;
    }
  }

}
