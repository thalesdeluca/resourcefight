using UnityEngine;
using UnityEngine.UI;

public enum PrologueStage {
  None,
  Earth,
  DestroyedEarth,
  Dome,
  Missions,
  CharacterSelect
}
public class PrologueController : MonoBehaviour {

  [SerializeField]
  private LinesSO lines;
  private bool selectedCharacter = false;

  private PrologueStage stage = PrologueStage.None;

  [SerializeField]
  private DialogueScript dialog;

  private GameObject earth, earthDestroyed;

  private GameObject dome, ball, flag, missions;

  private bool started = false;

  void Start() {
    earth = GameObject.Find("Earth");
    earthDestroyed = GameObject.Find("Earth Destroyed");
    earthDestroyed.GetComponent<Animator>().enabled = false;

    dome = GameObject.Find("Dome");
    missions = GameObject.Find("Missions");
    ball = missions.transform.Find("Ball").gameObject;
    flag = missions.transform.Find("Flag").gameObject;

    dome.SetActive(false);
    missions.SetActive(false);
    HideDialog();
  }

  private void SelectCharacter() {

  }

  private void StartGame() {

  }



  public void NextStage() {

    stage = stage + 1;

    switch (stage) {
      case PrologueStage.DestroyedEarth:
        ShowDestroyedEarth();
        break;
      case PrologueStage.Dome:
        ShowDome();
        break;
      case PrologueStage.Missions:
        ShowMissions();
        break;
      case PrologueStage.CharacterSelect:
        ShowCharacterSelect();
        break;
      default: break;
    }
  }

  public void StartDialog() {
    if (dialog.lineIndex > 0) {
      Debug.Log("start dialog");
      dialog.GetComponent<Image>().enabled = true;
      dialog.transform.Find("Text").gameObject.SetActive(true);
      dialog.StartWriting();
      started = true;
    }

  }

  public void HideDialog() {
    dialog.transform.Find("Text").gameObject.SetActive(false);
    dialog.GetComponent<Image>().enabled = false;

  }

  private void ShowDestroyedEarth() {
    earthDestroyed.GetComponent<Animator>().enabled = true;
    stage = PrologueStage.DestroyedEarth;
    earth.GetComponent<Animator>().Play("ImagePrologueFadeout");
  }

  private void ShowDome() {
    earth.SetActive(false);
    dome.SetActive(true);
    stage = PrologueStage.Dome;

    earthDestroyed.GetComponent<Animator>().Play("ImagePrologueFadeout");
    earthDestroyed.SetActive(false);
  }

  private void ShowMissions() {
    dome.SetActive(false);
    missions.SetActive(true);
    stage = PrologueStage.Missions;
    dome.GetComponent<Animator>().Play("ImagePrologueFadeout");
  }

  private void ShowCharacterSelect() {
    missions.SetActive(false);
    //selection.SetActive(true);
    stage = PrologueStage.CharacterSelect;
  }

  bool isAnimating() {
    var earthTime = earth.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime == 1;
    var destroyTime = earthDestroyed.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime == 1;
    var domeTime = dome.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime == 1;
    var ballTime = ball.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime == 1;
    var flagTime = flag.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime == 1;

    return earthTime || destroyTime || domeTime || ballTime || flagTime;
  }



  public void Submit() {
    if (started) {
      bool canProceed = dialog.Submit();

      if (canProceed && !dialog.writing && (dialog.lineIndex == 6 || dialog.lineIndex == 5 || dialog.lineIndex == 3 || dialog.lineIndex == 1)) {
        StartDialog();
        NextStage();

      } else if (canProceed) {
        StartDialog();
      }
    }

  }
}