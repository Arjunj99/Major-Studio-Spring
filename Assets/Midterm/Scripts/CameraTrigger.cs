using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
    public GameObject mainCamera;
    public GameObject anchorObject;
    public GameObject beacons;
    public TextMesh text;
    public bool playerIn;
    public float focalLength;
    public float focusDistance;
    public float aperture;
    public bool isLit;


    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") { // If player enters zone, edit cinematic mode, fade in text and light the beacon
            mainCamera.GetComponent<CameraMovement>().cameraPosition = anchorObject.transform.position + cameraPosition;
            mainCamera.GetComponent<CameraMovement>().cameraRotationEuler = cameraRotation;
            mainCamera.GetComponent<CameraMovement>().cinematicMode = true;
            mainCamera.GetComponent<CameraMovement>().focalLength = focalLength;
            mainCamera.GetComponent<CameraMovement>().focusDistance = focusDistance;
            mainCamera.GetComponent<CameraMovement>().aperture = aperture;
            beacons.SetActive(true);
            isLit = true;
            playerIn = true;
        }
    }

    void OnTriggerExit(Collider other) { // fade out text when player leaves
        if (other.tag == "Player") {
            mainCamera.GetComponent<CameraMovement>().cinematicMode = false;
            playerIn = false;
        }
    }
    

    // Start is called before the first frame update
    void Start() {
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update() { // Text fade
        if (playerIn) {
            text.color = Color32.Lerp(text.color, new Color32(255,255,255,255), Time.deltaTime * 0.5f);
        } else {
            text.color = Color32.Lerp(text.color, new Color32(255,255,255,0), Time.deltaTime * 0.5f);
        }
    }
}
