using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/SceneData")]

public class SceneData : ScriptableObject {
  public GameObject player { get; private set; }

  public GameObject[] prefabList = new GameObject[4];

  public void SetPlayer(ElementType type) {
    switch (type) {
      case ElementType.Electric:
        player = prefabList[1];
        break;
      case ElementType.Fire:
        player = prefabList[0];
        break;

      default:
        break;
    }
  }
}
