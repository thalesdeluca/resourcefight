using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour {
  [SerializeField]
  private LinesSO lines;


  [SerializeField]
  private string line;

  [SerializeField]
  private float delay = 0.2f;

  private Queue<string> queue;

  private PrologueController prologueController;

  private TMPro.TextMeshProUGUI text;

  private Coroutine writingEffect;

  public bool writing { get; private set; }

  public int lineIndex { get; private set; }

  private float time = 0;

  [SerializeField]
  private float timeSkip = 0.2f;

  private GameObject nextBtn;
  // Start is called before the first frame update
  void Start() {
    prologueController = GameObject.Find("EventSystem").GetComponent<PrologueController>();
    queue = new Queue<string>(lines.GetLines(line));
    text = this.transform.Find("Text").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    nextBtn = this.transform.Find("Next").gameObject;

    writing = false;
    lineIndex = queue.Count - 1;
  }

  void Update() {
    if (writing) {
      if (time < timeSkip) {
        time += Time.deltaTime;
      } else {
        nextBtn.SetActive(true);
      }
    }
  }

  IEnumerator StartNewLine() {
    ToggleShow(true);
    text.text = "";
    writing = true;
    line = queue.Peek();
    for (int i = 0; i < line.Length; i++) {
      text.text += line[i];
      yield return new WaitForSeconds(delay);
    }
    writing = false;
  }

  public bool Submit() {
    if (queue.Count > 0) {
      if (writing) {
        if (time >= timeSkip) {

          writing = false;
          if (text.text.Length < queue.Peek().Length) {
            text.text = queue.Peek();
          }
          StopCoroutine(writingEffect);
          time = 0;
          nextBtn.SetActive(true);

          return false;
        }

      } else {
        ToggleShow(false);
      }
    }

    return true;
  }

  void ToggleShow(bool show) {

    this.GetComponent<Image>().enabled = show;
    text.enabled = show;

    if (!show) {
      nextBtn.SetActive(false);
      writing = false;
      lineIndex = queue.Count - 1;
      queue.Dequeue();
      time = 0;
    }

  }

  public void StartWriting() {
    if (!writing) {
      writingEffect = StartCoroutine(StartNewLine());
    }

  }

  public void ChangeLine(string newLine) {
    queue = new Queue<string>(lines.GetLines(newLine));
  }

}
