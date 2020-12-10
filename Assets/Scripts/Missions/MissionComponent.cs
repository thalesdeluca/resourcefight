using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionComponent : MonoBehaviour {
  protected MissionType type;

  protected bool succeded = false;

  protected float time;

  protected bool started = false;

  protected bool countdown = false;

  protected GameObject target;

  protected GameObject countdownText;

  public abstract void OnSucceed();


  protected void StartMission() {

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
