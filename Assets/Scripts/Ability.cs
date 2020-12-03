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
  public GameObject fireAttack;
  public GameObject fireDash;
  public GameObject fireBlock;

  public GameObject waterAttack;
  public GameObject waterDash;
  public GameObject waterBlock;

  public GameObject electricAttack;
  public GameObject electricDash;
  public GameObject electricBlock;

  public GameObject earthAttack;
  public GameObject earthDash;
  public GameObject earthBlock;

  public ElementType element;
}
