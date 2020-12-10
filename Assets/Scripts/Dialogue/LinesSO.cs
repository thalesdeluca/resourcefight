using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LinesSO", menuName = "ScriptableObjects/LinesSO")]
public class LinesSO : ScriptableObject {

  [SerializeField]
  private string[] prologueLines;

  private Dictionary<string, Queue<string>> lines;


  public Queue<string> GetLines(string lineFound) {
    if (lines == null) {
      lines = new Dictionary<string, Queue<string>>();
    }


    if (prologueLines.Length > 0) {
      var linesToAdd = new string[prologueLines.Length];
      System.Array.Copy(prologueLines, linesToAdd, prologueLines.Length);
      Queue<string> prologue = new Queue<string>(linesToAdd);
      lines.Add("prologue", prologue);
    }

    return lines[lineFound];
  }
}
