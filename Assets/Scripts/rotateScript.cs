using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateScript : MonoBehaviour {
 public Image box;
 public int currentPos;
 public BoatMovement boatMovement;
 public RectTransform rectTransform;
 public List<Vector3> positions = new List<Vector3>(); 
 public float increment = 20f;

 public float[] rotations;

    // Start is called before the first frame update
    void Start() {
        // currentPos = boatMovement.levelSpeed;
        positions[1] = rectTransform.localPosition;
        positions[0] = positions[1] + (Vector3.down * increment);
        positions[2] = positions[1] + (Vector3.up * increment);
        positions[3] = positions[1] + (Vector3.up * increment*2);
    }

    // Update is called once per frame
    void Update() {
        currentPos = boatMovement.levelSpeed;
        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, positions[currentPos + 1], Time.deltaTime);

        // rectTransform.rotation = Quaternion.Euler(Vector3.Lerp(rectTransform.eulerAngles, new Vector3(0f, 0f, rotations[currentPos + 2]), Time.deltaTime));
    }
}
