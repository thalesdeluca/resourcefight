using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputUIMissionsController : MonoBehaviour {

  private DialogueScript dialogueScript;

  private MissionComponent mission;

  void Start() {
    dialogueScript = GetComponent<DialogueScript>();
    mission = GetComponent<MissionComponent>();
  }

  public void Move(InputAction.CallbackContext context) {

  }

  public void onClick(InputAction.CallbackContext context) {
    if (context.started) {
      var startGame = dialogueScript.Submit();
      if (startGame) {
        mission.StartMission();

      }
    }

  }
}