using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ElementType {
  Fire,
  Water,
  Earth,
  Electric
}
public class GameController : MonoBehaviour {

  public SceneData sceneData;
  public Transform playerPosition;
  // Start is called before the first frame update

  void Awake() {
    if (sceneData.player) {
      var player = Instantiate(sceneData.player, playerPosition.position, Quaternion.identity);
      player.gameObject.name = "Player";
    } else {
      var player = Instantiate(sceneData.prefabList[0], playerPosition.position, Quaternion.identity);
      player.gameObject.name = "Player";

    }
  }
  void Start() {
    Application.targetFrameRate = 60;
    QualitySettings.vSyncCount = 0;


  }


}
