using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hits : MissionComponent {
  private DialogueScript dialogue;

  private TMPro.TextMeshProUGUI timeText;

  private GameObject ball;


  public override void OnSucceed() {
    this.GetComponent<MissionController>().FinishMission(true, time, ball.GetComponent<BallController>().hits);
  }

  // Start is called before the first frame update
  void Start() {
    Debug.Log(
      "StartHits"
    );
    dialogue = this.GetComponent<DialogueScript>();
    dialogue.ChangeLine("hits");
    dialogue.StartWriting();
    time = maxTime;
    
    ball = GameObject.Find("ball");
    timeText = GameObject.Find("Time").GetComponent<TMPro.TextMeshProUGUI>();
    countdownText = GameObject.Find("Countdown");
  }

  // Update is called once per frame
  void Update() {
    Debug.Log(time);
    
    if (started) {
      time -= Time.deltaTime;
      timeText.text = "Time: " + Math.Round(time, 2);

      if (time <= 0) {
        time = 0;
        started = false;
        OnSucceed();
      }
    } else {
       if(countdown) {
         time -= Time.unscaledDeltaTime;
         if(time  <= 0) {
           countdown = false;
           started = true;
           var text = countdownText.GetComponent<TMPro.TextMeshProUGUI>();
           text.enabled = false;
           text.text = Math.Round(time, 0f) + "";
           time = maxTime;
           Time.timeScale = 1;
         }
      }
      Time.timeScale = 0;
    }


   
  }
}
