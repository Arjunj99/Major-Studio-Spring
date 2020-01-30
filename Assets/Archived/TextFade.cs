using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour {
    public bool cinematicMode = false;
    private GameObject cameraHinge;
    public CameraMovement cameraMovement;

    void Start() {
        cameraHinge = GameObject.Find("Camera Hinge");
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update() {
        // if (cameraMovement.cinematicMode) {
        //     gameObject.GetComponent<TextMesh>().color = Color32.Lerp(gameObject.GetComponent<TextMesh>().color, new Color32(255,255,255,255), Time.deltaTime * 0.5f);
        // } else {
        //     if (Input.GetKey(KeyCode.LeftShift)) {
        //         gameObject.GetComponent<TextMesh>().color = Color32.Lerp(gameObject.GetComponent<TextMesh>().color, new Color32(255,255,255,255), Time.deltaTime * 0.5f);
        //     } else {
        //         gameObject.GetComponent<TextMesh>().color = Color32.Lerp(gameObject.GetComponent<TextMesh>().color, new Color32(255,255,255,0), Time.deltaTime * 0.5f);
        //     }
        // }

        // transform.LookAt(cameraHinge.transform);
        // transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z + 180f);
        
        
        


        
    }
}
