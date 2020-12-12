using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionComponent : MonoBehaviour {
  public MissionType type;

  protected bool succeded = false;

  protected float time;

  protected float timeFinished = 0;

  protected float maxTime = 30f;

  protected bool started = false;

  protected bool finish = false;
  protected bool countdown = false;

  protected GameObject target;

  protected GameObject countdownText;

  protected float maxCountdown = 3.4f;

  protected float maxFinish = 2f;

  protected TMPro.TextMeshProUGUI missionLabel;
  protected bool finishDialog = false;

  protected GameObject ball;


  public abstract void OnSucceed();

  public abstract void OnTarget();


  public void StartMission() {


    if (finishDialog) {
      Debug.Log("Finish Dialog");
      if (missionLabel.text == "HITS") {
        this.GetComponent<MissionController>().FinishMission(true, timeFinished, ball.GetComponent<BallController>().hits);
      } else {
        this.GetComponent<MissionController>().FinishMission(false, timeFinished);
      }

    }

    if (!started && !countdown && !finish) {

      Debug.Log("Finish Dialog");

      countdown = true;
      time = maxCountdown;
      countdownText = GameObject.Find("Countdown");
      countdownText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;

      countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = (maxCountdown) + "";
    }


  }

  public abstract void ChangeToNext();

  protected void PointToTarget() {
    //Create/Enable new camera
    //Move towards target
    //Wait
    // go back
    //disable
    //start countdown;
  }
}
