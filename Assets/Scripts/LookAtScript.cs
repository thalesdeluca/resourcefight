using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LookAtScript : MonoBehaviour {
  private const float BOUND_X = 2.9f;
  private const float BOUND_Y = 1.5f;

  private const float BOUND_MAX_X = 3.2f;
  private const float BOUND_MAX_Y = 1.8f;
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
    if ((isVisible.x > 0 && isVisible.x < 1) && (isVisible.y > 0 && isVisible.y < 1)) {
      if (pointer) {
        Destroy(pointer);
      }
    } else {
      var angle = Vector2.Angle(target.position - player.transform.position, Vector2.up);

      Vector3 cameraPoint = isVisible;
      Debug.Log("camera point" + Math.Round(cameraPoint.x, 1) + "   " + cameraPoint.x + "   " + isVisible);

      //RIGHT SIDE
      if (cameraPoint.x > 0.5) {

        if (cameraPoint.x < 1) {
          cameraPoint.x -= 0.5f;
          cameraPoint.x = (BOUND_X * cameraPoint.x) / 0.5f;
        } else {
          cameraPoint.x = BOUND_X;
        }

      } else if (cameraPoint.x < 0.5) {//LEFT SIDE
        if (cameraPoint.x > 0) {
          cameraPoint.x = (BOUND_X * cameraPoint.x) / 0.5f;
          cameraPoint.x += -BOUND_X;
        } else {
          cameraPoint.x = -BOUND_X;
        }
      }

      if (cameraPoint.y >= 0.5) {
        if (cameraPoint.y < 1) {
          cameraPoint.y -= 0.5f;
          cameraPoint.y = (BOUND_Y * cameraPoint.y) / 0.5f;
        } else {
          cameraPoint.y = BOUND_Y;
        }

      } else if (cameraPoint.y < 0.5) {//LEFT SIDE
        if (cameraPoint.y > 0) {
          cameraPoint.y = (BOUND_Y * cameraPoint.y) / 0.5f;
          cameraPoint.y += -BOUND_Y;
        } else {
          cameraPoint.y = -BOUND_Y;

        }
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
