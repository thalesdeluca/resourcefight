using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LinesSO", menuName = "ScriptableObjects/LinesSO")]
public class LinesSO : ScriptableObject {

  [SerializeField]
  private string[] prologueLines;
  [SerializeField]
  private string[] hoopsLines;
  [SerializeField]
  private string[] escapeLines;
  [SerializeField]
  private string[] hitsLines;


  [SerializeField]

  private string[] hoopsFinishLines;
  [SerializeField]
  private string[] hitsFinishLines;


  private Dictionary<string, Queue<string>> lines;


  public Queue<string> GetLines(string lineFound) {
    if (lineFound != null && lineFound.Length > 0) {
      if (lines == null) {
        lines = new Dictionary<string, Queue<string>>();
      }


      if (prologueLines.Length > 0 && !lines.ContainsKey("prologue")) {
        var linesToAdd = new string[prologueLines.Length];
        System.Array.Copy(prologueLines, linesToAdd, prologueLines.Length);
        Queue<string> prologue = new Queue<string>(linesToAdd);
        lines.Add("prologue", prologue);
      }

      if (hoopsLines.Length > 0 && !lines.ContainsKey("hoops")) {
        var linesToAdd = new string[hoopsLines.Length];
        System.Array.Copy(hoopsLines, linesToAdd, hoopsLines.Length);
        Queue<string> prologue = new Queue<string>(linesToAdd);
        lines.Add("hoops", prologue);
      }


      if (hitsLines.Length > 0 && !lines.ContainsKey("hits")) {
        var linesToAdd = new string[hitsLines.Length];
        System.Array.Copy(hitsLines, linesToAdd, hitsLines.Length);
        Queue<string> prologue = new Queue<string>(linesToAdd);
        lines.Add("hits", prologue);
      }

      if (hoopsLines.Length > 0 && !lines.ContainsKey("hoops_finish")) {
        var linesToAdd = new string[hoopsFinishLines.Length];
        System.Array.Copy(hoopsFinishLines, linesToAdd, hoopsFinishLines.Length);
        Queue<string> prologue = new Queue<string>(linesToAdd);
        lines.Add("hoops_finish", prologue);
      }


      if (hitsFinishLines.Length > 0 && !lines.ContainsKey("hits_finish")) {
        var linesToAdd = new string[hitsFinishLines.Length];
        System.Array.Copy(hitsFinishLines, linesToAdd, hitsFinishLines.Length);
        Queue<string> prologue = new Queue<string>(linesToAdd);
        lines.Add("hits_finish", prologue);
      }


      return lines[lineFound];
    }
    return new Queue<string>();
  }



}
