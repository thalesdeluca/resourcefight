using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class InputUIController : MonoBehaviour {

  private PrologueController prologueController;

  public SceneData sceneData;
  private GameObject player;
  void Start() {
    prologueController = GetComponent<PrologueController>();
    player = GameObject.Find("Player");
  }

  public void Move(InputAction.CallbackContext context) {
    if (context.started) {
      var value = context.ReadValue<Vector2>();
      player.GetComponent<SpriteRenderer>().flipX = value.x < 0;
    }
  }

  public void onClick(InputAction.CallbackContext context) {
    if (context.started) {
      if (prologueController.stage != PrologueStage.CharacterSelect) {
        prologueController.Submit();
      } else {
        if (!GameObject.Find("Dialogue").GetComponent<DialogueScript>().writing) {
          sceneData.SetPlayer(player.GetComponent<SpriteRenderer>().flipX ? ElementType.Electric : ElementType.Fire);

          SceneManager.LoadScene("SampleScene");
        }


      }


    }

  }
}