using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateScript : MonoBehaviour {
 public Image arrow;
 public int currentPos;
 public BoatMovement boatMovement;
 public RectTransform rectTransform;

 public float[] rotations;

    // Start is called before the first frame update
    void Start() {
        // currentPos = boatMovement.levelSpeed;
    }

    // Update is called once per frame
    void Update() {
        currentPos = boatMovement.levelSpeed;
        rectTransform.rotation = Quaternion.Euler(Vector3.Lerp(rectTransform.eulerAngles, new Vector3(0f, 0f, rotations[currentPos + 2]), Time.deltaTime));
    }
}
