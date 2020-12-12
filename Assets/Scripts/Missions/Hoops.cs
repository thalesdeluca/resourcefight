using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hoops : MissionComponent {

  private DialogueScript dialogue;

  private TMPro.TextMeshProUGUI timeText;



  private TMPro.TextMeshProUGUI hitsCount;


  public override void OnSucceed() {

    finish = true;
    timeText.text = "0";
    countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = "Finish";
    countdownText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
    timeFinished = time;
    time = maxFinish;
    Time.timeScale = 0;


  }
  public override void ChangeToNext() {
    this.GetComponent<MissionController>().FinishMission(true, timeFinished);
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
    type = MissionType.Hoops;

    ball = GameObject.Find("ball");
    timeText = GameObject.Find("Time").GetComponent<TMPro.TextMeshProUGUI>();
    countdownText = GameObject.Find("Countdown");
    countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    hitsCount = GameObject.Find("HitsCount").GetComponent<TMPro.TextMeshProUGUI>();

    hitsCount.enabled = false;

    missionLabel = GameObject.Find("MissionLabel").GetComponent<TMPro.TextMeshProUGUI>();
    missionLabel.text = "HOOPS";

    ball.GetComponent<BallController>().ChangeToHoops();
    maxTime = 60f;
  }

  // Update is called once per frame
  void Update() {
    if (started) {
      time -= Time.deltaTime;
      hitsCount.text = ball.GetComponent<BallController>().hits + "";
      timeText.text = "Time: " + Math.Round(time, 2);
      if (time <= maxTime - 1) {
        var text = countdownText.GetComponent<TMPro.TextMeshProUGUI>();
        text.enabled = false;
        text.text = "";
      }

      if (time <= 0) {
        time = 0;
        started = false;
        OnSucceed();
      }
    } else if (finish) {
      if (time <= 0) {
        if (!finishDialog) {
          dialogue.ChangeLine("hoops_finish");
          dialogue.StartWriting();
        }

        finishDialog = true;
      } else {
        time -= Time.unscaledDeltaTime;
      }
    } else {
      if (countdown) {
        time -= Time.unscaledDeltaTime;

        var text = countdownText.GetComponent<TMPro.TextMeshProUGUI>();

        if (Math.Round(time) < 1) {
          text.text = "GO";
          Time.timeScale = 1;
          countdown = false;
          started = true;
          time = maxTime;
          return;
        } else {
          text.text = (Math.Round(time)) + "";
        }
        return;
      }
      Time.timeScale = 0;
    }
  }

  public override void OnTarget() {
    time = 0;
    started = false;
    OnSucceed();
  }
}
