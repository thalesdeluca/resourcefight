using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionComponent : MonoBehaviour {
  protected MissionType type;

  protected bool succeded;


  public abstract void OnSucceed();
}
