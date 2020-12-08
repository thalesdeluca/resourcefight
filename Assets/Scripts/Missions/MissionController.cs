using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum MissionType {
  Escape,
  Hits,
  Hoops,
}

public class MissionController : MonoBehaviour {

  [SerializeField]
  private Missions missionSystem;

  private MissionComponent missionScript;

  // Start is called before the first frame update
  void Start() {
    if (missionSystem) {
      if (missionSystem.missions.Count == 0) {
        missionSystem.missions = this.GenerateMissions();
        UpdateMission();
      }
    }
  }

  void UpdateMission() {
    switch (missionSystem.missions.Peek()) {
      case MissionType.Escape:
        missionScript = this.gameObject.AddComponent<Escape>();
        break;
      case MissionType.Hits:
        missionScript = this.gameObject.AddComponent<Hits>();
        break;
      case MissionType.Hoops:
        missionScript = this.gameObject.AddComponent<Hoops>();
        break;
    }
  }

  void FinishMission(bool succeeded, float time) {
    if (missionSystem.missions.Count > 0) {
      missionSystem.missionsFinished.Add(new Mission(missionSystem.missions.Peek(), succeeded, time));
      missionSystem.missions.Dequeue();
      Destroy(missionScript);
      UpdateMission();
    }
  }

  Queue<MissionType> GenerateMissions() {
    Queue<MissionType> queue = new Queue<MissionType>();
    List<MissionType> remaining = Enum.GetValues(typeof(MissionType)).Cast<MissionType>().ToList<MissionType>();

    int random = UnityEngine.Random.Range(0, 2);

    queue.Enqueue(remaining[random]);
    remaining.RemoveAt(random);

    random = UnityEngine.Random.Range(0, 1);

    queue.Enqueue(remaining[random]);
    remaining.RemoveAt(random);

    queue.Enqueue(remaining[0]);
    remaining.RemoveAt(0);


    return queue;
  }


}
