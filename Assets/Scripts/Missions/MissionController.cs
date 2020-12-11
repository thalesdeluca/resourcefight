using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum MissionType {
  Hits,
  Hoops,
}

public class MissionController : MonoBehaviour {

  [SerializeField]
  private Missions missionSystem;

  private MissionComponent missionScript;

  [SerializeField]
  public Queue<MissionType> missions;

  [SerializeField]

  public List<Mission> missionsFinished = new List<Mission>();

  // Start is called before the first frame update
  void Start() {


    missions = new Queue<MissionType>();
    missions.Enqueue(MissionType.Hits);
    missions.Enqueue(MissionType.Hoops);

    UpdateMission();


  }

  void UpdateMission() {
    Debug.Log(missions.Count);
    switch (missions.Peek()) {

      case MissionType.Hits:
        missionScript = this.gameObject.AddComponent<Hits>();
        break;
      case MissionType.Hoops:
        missionScript = this.gameObject.AddComponent<Hoops>();
        break;
    }


  }

  public void FinishMission(bool succeeded, float time, int hits) {
    if (missions.Count > 0) {
      missionsFinished.Add(new Mission(missions.Peek(), succeeded, time, hits));
      missions.Dequeue();
      Destroy(missionScript);
      UpdateMission();
    }
  }

  public void FinishMission(bool succeeded, float time) {
    if (missions.Count > 0) {
      missionsFinished.Add(new Mission(missions.Peek(), succeeded, time));
      missions.Dequeue();
      Destroy(missionScript);
      UpdateMission();
    }
  }


}
