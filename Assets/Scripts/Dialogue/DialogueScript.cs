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

  private Coroutine writingEffect;

  public bool writing { get; private set; }

  public int lineIndex { get; private set; }

  private float time = 0;

  [SerializeField]
  private float timeSkip = 0.2f;

  private GameObject nextBtn;
  // Start is called before the first frame update
  void Start() {
    if (line != null) {
      queue = new Queue<string>(lines.GetLines(line));
      lineIndex = queue.Count - 1;
    }

    nextBtn = GameObject.Find("Dialogue").transform.Find("Next").gameObject;

    writing = false;

  }

  void Update() {
    if (writing) {
      if (time < timeSkip) {
        time += Time.unscaledDeltaTime;
      } else {
        GameObject.Find("Dialogue").transform.Find("Next").gameObject.SetActive(true);

      }
    }
  }

  IEnumerator StartNewLine() {
    ToggleShow(true);
    var text = GameObject.Find("Dialogue").transform.Find("Text").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    text.text = "";
    writing = true;
    var lineText = queue.Peek();
    for (int i = 0; i < lineText.Length; i++) {
      text.text += lineText[i];
      yield return new WaitForSecondsRealtime(delay);
    }
    writing = false;
  }

  public bool Submit() {
    if (queue.Count > 0) {
      if (writing) {
        if (time >= timeSkip) {
          var text = GameObject.Find("Dialogue").transform.Find("Text").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
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

    GameObject.Find("Dialogue").GetComponent<Image>().enabled = show;
    GameObject.Find("Dialogue").transform.Find("Text").gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = show;



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
    Debug.Log(newLine);
    queue = new Queue<string>(lines.GetLines(newLine));
  }

}
