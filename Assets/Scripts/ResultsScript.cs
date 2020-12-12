using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultsScript : MonoBehaviour {
  public TMPro.TextMeshProUGUI text;
  public Missions missionSystem;

  public GameObject food;
  public GameObject water;

  public GameObject tools;

  void Start() {
    int points = GeneratePoints();
    text.text = points + "";
    if (points >= 10) {
      water.SetActive(true);
    }
    if (points >= 25) {
      food.SetActive(true);
    }
    if (points >= 35) {
      tools.SetActive(true);
    }
  }

  int GeneratePoints() {
    int points = 0;
    missionSystem.missionsFinished.ForEach(delegate (Mission mission) {
      points += ((int)mission.time) + mission.hits;
      if (mission.hits == 0) {
        points += mission.succeeded ? 10 : 0;
      }
    });
    return points;
  }
}
