using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class CameraMovement : MonoBehaviour {
    private PostProcessVolume volume = null;
    private Vignette vignette = null;
    private DepthOfField depthOfField = null;
    public GameObject hinge;
    public bool cinematicMode = false;
    public float focalLength;
    public float focusDistance;
    public float aperture;
    public Vector3 cameraPosition;
    private Quaternion cameraRotation;
    public Vector3 cameraRotationEuler;
    public GameObject player;
    public Camera cam;
    private int zoomInFOV = 5;
    private int zoomOutFOV = 60;
    public bool isScouting = false;
    public int rotationSpeed = -20;
    public GameObject ppm;

    public bool canDisembark;


    void InitializePostProcessing(float intensity, float focusDistance, float aperture, float focalLength) {
        volume = ppm.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);
        volume.profile.TryGetSettings(out depthOfField);

        vignette.enabled.value = true;
        vignette.intensity.value = intensity;

        depthOfField.enabled.value = true;
        depthOfField.focusDistance.value = focusDistance;
        depthOfField.aperture.value = aperture;
        depthOfField.focalLength.value = focalLength;
    }

    void Start() { // Sets up Post Processing Values
        InitializePostProcessing(0f, 10f, 17.2f, 159f);
    }

    void Update() {
        if (!cinematicMode) { // If it isn't in cinematic mode, allow for player-follow camera
            if (!canDisembark) { // Disembark mechanic was scrapped early in design process
                // Sets camera position and rotation each frame (keep in mind it uses hinge)
                this.cameraPosition = player.transform.position + (player.transform.up * 6) + (player.transform.forward * -20);
                if (!isScouting) {
                    this.cameraRotation = player.transform.rotation;
                    hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, cameraRotation, 0.8f * Time.deltaTime);
                }

                // Spyglass mode
                if (Input.GetKey(KeyCode.LeftShift)) {
                    cam.fieldOfView = Mathf.Lerp(this.cam.fieldOfView, zoomInFOV, 2f * Time.deltaTime);
                    vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.76f, 0.7f * Time.deltaTime);
                    depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value, 1f, 0.3f * Time.deltaTime);
                    isScouting = true;
                } else { // Normal fog effect
                    cam.fieldOfView = Mathf.Lerp(this.cam.fieldOfView, zoomOutFOV, 2f * Time.deltaTime);
                    vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0f, 0.7f * Time.deltaTime);
                    depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value, 159f, 1.8f * Time.deltaTime);
                    depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, 10f, 1.8f * Time.deltaTime);
                    depthOfField.aperture.value = Mathf.Lerp(depthOfField.aperture.value, 17.2f, 1.8f * Time.deltaTime);
                    isScouting = false;
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.transform.parent.rotation, Time.deltaTime * 0.5f);
                }

                // Camera controls for spyglass mode
                if(isScouting && Input.GetKey(KeyCode.A)) {
                    this.transform.Rotate(new Vector3(0, rotationSpeed*Time.deltaTime, 0));
                }
                if(isScouting && Input.GetKey(KeyCode.D)) {
                    this.transform.Rotate(new Vector3(0, -rotationSpeed*Time.deltaTime, 0));
                }
                if(isScouting && Input.GetKey(KeyCode.W)) {
                    this.transform.Rotate(new Vector3(rotationSpeed*Time.deltaTime, 0, 0));
                }
                if(isScouting && Input.GetKey(KeyCode.S)) {
                    this.transform.Rotate(new Vector3(-rotationSpeed*Time.deltaTime, 0, 0));
                }   
            }
            hinge.transform.position = Vector3.Lerp(hinge.transform.position, player.transform.position, 2f * Time.deltaTime);
        } else {
            // Cinematic mode is when the player gets close to an island. Its values depend on the corresponding island. 
            depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value, focalLength, 3f * Time.deltaTime);
            depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, focusDistance, 3f * Time.deltaTime);
            depthOfField.aperture.value = Mathf.Lerp(depthOfField.aperture.value, aperture, 3f * Time.deltaTime);
            hinge.transform.position = Vector3.Lerp(hinge.transform.position, cameraPosition, 0.6f * Time.deltaTime);
            hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, Quaternion.Euler(cameraRotationEuler), 0.6f * Time.deltaTime);
        } 

    }
}

// public class CameraMovement : MonoBehaviour {
//     private PostProcessVolume volume = null;
//     private Vignette vignette = null;
//     private DepthOfField depthOfField = null;
//     public GameObject hinge;
//     public bool cinematicMode = false;
//     public float focalLength;
//     public float focusDistance;
//     public float aperture;
//     public Vector3 cameraPosition;
//     private Quaternion cameraRotation;
//     public Vector3 cameraRotationEuler;
//     public GameObject player;
//     public Camera cam;
//     private int zoomInFOV = 5;
//     private int zoomOutFOV = 60;
//     public bool isScouting = false;
//     public int rotationSpeed = -20;
//     public GameObject ppm;

//     public bool canDisembark;
//     void Start() { // Sets up Post Processing Values
//         volume = ppm.GetComponent<PostProcessVolume>();
//         volume.profile.TryGetSettings(out vignette);
//         volume.profile.TryGetSettings(out depthOfField);

//         vignette.enabled.value = true;
//         vignette.intensity.value = 0f;

//         depthOfField.enabled.value = true;
//         depthOfField.focusDistance.value = 10f;
//         depthOfField.aperture.value = 17.2f;
//         depthOfField.focalLength.value = 159f;
//     }

//     void Update() {
//         if (!cinematicMode) { // If it isn't in cinematic mode, allow for player-follow camera
//             if (!canDisembark) { // Disembark mechanic was scrapped early in design process
//                 // Sets camera position and rotation each frame (keep in mind it uses hinge)
//                 this.cameraPosition = player.transform.position + (player.transform.up * 6) + (player.transform.forward * -20);
//                 if (!isScouting) {
//                     this.cameraRotation = player.transform.rotation;
//                     hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, cameraRotation, 0.8f * Time.deltaTime);
//                 }

//                 // Spyglass mode
//                 if (Input.GetKey(KeyCode.LeftShift)) {
//                     cam.fieldOfView = Mathf.Lerp(this.cam.fieldOfView, zoomInFOV, 2f * Time.deltaTime);
//                     vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.76f, 0.7f * Time.deltaTime);
//                     depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value, 1f, 0.3f * Time.deltaTime);
//                     isScouting = true;
//                 } else { // Normal fog effect
//                     cam.fieldOfView = Mathf.Lerp(this.cam.fieldOfView, zoomOutFOV, 2f * Time.deltaTime);
//                     vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0f, 0.7f * Time.deltaTime);
//                     depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value, 159f, 1.8f * Time.deltaTime);
//                     depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, 10f, 1.8f * Time.deltaTime);
//                     depthOfField.aperture.value = Mathf.Lerp(depthOfField.aperture.value, 17.2f, 1.8f * Time.deltaTime);
//                     isScouting = false;
//                     this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.transform.parent.rotation, Time.deltaTime * 0.5f);
//                 }

//                 // Camera controls for spyglass mode
//                 if(isScouting && Input.GetKey(KeyCode.A)) {
//                     this.transform.Rotate(new Vector3(0, rotationSpeed*Time.deltaTime, 0));
//                 }
//                 if(isScouting && Input.GetKey(KeyCode.D)) {
//                     this.transform.Rotate(new Vector3(0, -rotationSpeed*Time.deltaTime, 0));
//                 }
//                 if(isScouting && Input.GetKey(KeyCode.W)) {
//                     this.transform.Rotate(new Vector3(rotationSpeed*Time.deltaTime, 0, 0));
//                 }
//                 if(isScouting && Input.GetKey(KeyCode.S)) {
//                     this.transform.Rotate(new Vector3(-rotationSpeed*Time.deltaTime, 0, 0));
//                 }   
//             }
//             hinge.transform.position = Vector3.Lerp(hinge.transform.position, player.transform.position, 2f * Time.deltaTime);
//         } else {
//             // Cinematic mode is when the player gets close to an island. Its values depend on the corresponding island. 
//             depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value, focalLength, 3f * Time.deltaTime);
//             depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, focusDistance, 3f * Time.deltaTime);
//             depthOfField.aperture.value = Mathf.Lerp(depthOfField.aperture.value, aperture, 3f * Time.deltaTime);
//             hinge.transform.position = Vector3.Lerp(hinge.transform.position, cameraPosition, 0.6f * Time.deltaTime);
//             hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, Quaternion.Euler(cameraRotationEuler), 0.6f * Time.deltaTime);
//         } 

//     }
// }