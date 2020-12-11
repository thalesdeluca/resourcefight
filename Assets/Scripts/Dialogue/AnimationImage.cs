using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationImage : MonoBehaviour {

  private PrologueController prologue;
  [SerializeField]
  private PrologueStage nextStage;

  void Start() {
    prologue = GameObject.Find("EventSystem").GetComponent<PrologueController>();
  }

  void StartDialog() {
    prologue.StartDialog();
    prologue.NextStage();
  }

  void HideDialog() {
    this.gameObject.SetActive(false);
  }


}
