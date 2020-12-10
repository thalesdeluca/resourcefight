using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MissionComponent {

  private float maxTime = 30f;

  void Start() {
    type = MissionType.Escape;
    Time.timeScale = 0;
    countdownText = GameObject.Find("Countdown");
  }


  void Update() {

  }

  public override void OnSucceed() {
    var inTime = time <= maxTime;
    this.GetComponent<MissionController>().FinishMission(inTime, time);
  }
}