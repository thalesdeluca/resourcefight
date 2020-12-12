using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MissionComponent {

  private float maxTime = 30f;

  void Start() {

    Time.timeScale = 0;
    countdownText = GameObject.Find("Countdown");
  }


  void Update() {

  }

  public override void OnSucceed() {
    var inTime = time <= maxTime;
    this.GetComponent<MissionController>().FinishMission(inTime, time);
  }

  public override void ChangeToNext() {
    throw new System.NotImplementedException();
  }

  public override void OnTarget() {
    throw new System.NotImplementedException();
  }
}