using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {
  // Start is called before the first frame update
  private GameObject mission;

  [SerializeField]
  private string layer;

  void Start() {
    mission = GameObject.Find("Controller");
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.layer == LayerMask.NameToLayer(layer)) {
      mission.GetComponent<MissionComponent>().OnSucceed();
    }
  }
}
