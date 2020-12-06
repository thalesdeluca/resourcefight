using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LookAtScript : MonoBehaviour {

  private Camera camera;

  [SerializeField]
  private GameObject pointerPrefab;

  private GameObject pointer;

  [SerializeField]
  private Transform target;

  private GameObject player;


  // Start is called before the first frame update
  void Start() {
    camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    player = GameObject.Find("Player");
  }

  // Update is called once per frame
  void Update() {
    Vector2 isVisible = camera.WorldToViewportPoint(target.position);
    if ((isVisible.x > 0 && isVisible.x <= 1) && (isVisible.y > 0 && isVisible.y <= 1)) {
      if (pointer) {
        Destroy(pointer);
      }
    } else {
      var angle = Vector2.Angle(target.position - player.transform.position, Vector2.up);

      Vector3 cameraPoint = isVisible;
      Debug.Log("camera point" + Math.Round(cameraPoint.x, 1) + "   " + cameraPoint + "   " + isVisible);

      if (Math.Round(cameraPoint.x, 1) > 0.5f) {
        if (cameraPoint.x < 0.7) {
          cameraPoint.x = cameraPoint.x - 0.3f;
        } else {
          cameraPoint.x = Math.Round(cameraPoint.x, 1) < 2.1 ? cameraPoint.x : 2.1f;
        }
      } else if (Math.Round(cameraPoint.x, 1) < 0.5f) {
        if (cameraPoint.x > -0.7) {
          cameraPoint.x = cameraPoint.x - 0.3f;
        } else {
          cameraPoint.x = Math.Round(cameraPoint.x, 1) >= 0 ? cameraPoint.x - 0.5f : -2.1f;
        }
      } else {
        cameraPoint.x = 0;
      }

      if (Math.Round(cameraPoint.y, 1) != 0.5f) {
        cameraPoint.y = Math.Round(cameraPoint.y, 1) > 1.2f ? 0.8f : cameraPoint.y - 0.5f;
      } else {
        cameraPoint.y = 0;
      }

      cameraPoint.z = 10f;


      if (!pointer) {
        pointer = Instantiate(pointerPrefab, cameraPoint, Quaternion.Euler(0, isVisible.x > 0.5 ? 180 : 0, angle), camera.transform);
      } else {
        pointer.transform.localPosition = cameraPoint;
        pointer.transform.rotation = Quaternion.Euler(0, isVisible.x > 0.5 ? 180 : 0, angle);
      }

    }

  }
}
