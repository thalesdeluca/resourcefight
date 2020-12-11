using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionComponent : MonoBehaviour {
  protected MissionType type;

  protected bool succeded = false;

  protected float time;

  protected float maxTime = 30f;

  protected bool started = false;

  protected bool countdown = false;

  protected GameObject target;

  protected GameObject countdownText;

  protected float maxCountdown = 3f;

  public abstract void OnSucceed();


  public void StartMission() {
    countdown = true;
    time = maxCountdown;
     countdownText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
  }

  protected void PointToTarget() {
    //Create/Enable new camera
    //Move towards target
    //Wait
    // go back
    //disable
    //start countdown;
  }
}
