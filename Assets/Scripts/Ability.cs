using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public enum ElementType {
  Fire,
  Water,
  Earth,
  Electric
}

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class Ability : ScriptableObject {
  public GameObject prefabAttack;
  public GameObject prefabDash;
  public GameObject prefabBlock;

  public ElementType element;
}
