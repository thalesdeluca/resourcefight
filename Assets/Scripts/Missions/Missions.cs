using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Missions", menuName = "ScriptableObjects/Missions")]

public class Missions : ScriptableObject {
  public Queue<MissionType> missions = new Queue<MissionType>();

  public List<Mission> missionsFinished = new List<Mission>();
}