using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hits : MissionComponent {
  private DialogueScript dialogue;

  private TMPro.TextMeshProUGUI timeText;

  private TMPro.TextMeshProUGUI hitsCount;




  public override void OnSucceed() {
    Debug.Log("suceed");
    finish = true;
    timeText.text = "0";
    countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = "Finish";
    countdownText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
    timeFinished = time;
    time = maxFinish;
    Time.timeScale = 0;


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
    type = MissionType.Hits;

    ball = GameObject.Find("ball");
    timeText = GameObject.Find("Time").GetComponent<TMPro.TextMeshProUGUI>();
    countdownText = GameObject.Find("Countdown");
    countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    hitsCount = GameObject.Find("HitsCount").GetComponent<TMPro.TextMeshProUGUI>();

    hitsCount.enabled = true;

    missionLabel = GameObject.Find("MissionLabel").GetComponent<TMPro.TextMeshProUGUI>();
    missionLabel.text = "HITS";

    ball.GetComponent<BallController>().ChangeToHits();
    maxTime = 30f;
  }

  public override void ChangeToNext() {
    this.GetComponent<MissionController>().FinishMission(true, timeFinished);
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
        Debug.Log("Finish Change Line");
        if (!finishDialog) {
          dialogue.ChangeLine("hits_finish");
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
