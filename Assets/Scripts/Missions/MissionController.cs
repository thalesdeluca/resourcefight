using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

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
  private GameObject[] resetObjects;

  // Start is called before the first frame update
  void Start() {


    missions = new Queue<MissionType>();
    missions.Enqueue(MissionType.Hits);
    missions.Enqueue(MissionType.Hoops);

    UpdateMission();

    resetObjects = new GameObject[2];
    resetObjects[0] = GameObject.Find("ball");
    resetObjects[1] = GameObject.Find("Player");

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
      missionSystem.missionsFinished.Add(new Mission(missions.Peek(), succeeded, time, hits));
      missions.Dequeue();
      Destroy(missionScript);
      ResetObjects();

      if (missions.Count > 0) {
        UpdateMission();
      } else {
        SceneManager.LoadScene("Results");
      }

    }
  }

  public void FinishMission(bool succeeded, float time) {
    if (missions.Count > 0) {
      missionSystem.missionsFinished.Add(new Mission(missions.Peek(), succeeded, time));
      missions.Dequeue();
      Destroy(missionScript);
      ResetObjects();

      if (missions.Count > 0) {
        UpdateMission();
      } else {
        SceneManager.LoadScene("Results");
      }
    }
  }

  void ResetObjects() {
    if (resetObjects != null) {
      foreach (var obj in resetObjects) {
        obj.GetComponent<ResetScript>().ResetPosition();
      }
    }

    GameObject.Find("Main Camera").GetComponent<CameraScript>().GoToPlayer();
    GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
  }

}
