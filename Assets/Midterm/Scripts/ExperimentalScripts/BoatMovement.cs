using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {
    public KeyCode forward = KeyCode.W, left = KeyCode.A, right = KeyCode.D, back = KeyCode.S; // Player Key Inputs for Boat
    public enum playTestMode { playTestMode1, playTestMode2, playTestMode3 }; // Possible PlayTestModes (Use only for Debug)
    public playTestMode playTest; // Current PlayTest Mode
    [HideInInspector] public CharacterController characterController; // CharacterController of the Boat Object
    [HideInInspector] public Boat boat; // Boat Instance that stores Movement Data
    [HideInInspector] public CameraController cam; // CameraController for the Main Camera 
    [HideInInspector] public GameObject camObject; // Main Camera Object
    [SerializeField] private float deacceleration; // Rate of Deacceleration
    [SerializeField] private float acceleration; // Rate of Acceleration
    [SerializeField] private float maxSpeed; // Max Speed of Boat
    [SerializeField] private float rotationLerpSpeed; // Lerp Speed between Rotations

    /// <summary>
    /// Checks for any Forward Input.
    /// </summary>
    /// <return>
    /// Returns true if there is currently a forward input. False if not.
    /// </return>
    public bool isForward() {
        if (Input.GetKey(forward) || (Input.GetAxis("Vertical") > 0f)) { return true; } 
        else { return false; }
    }

    /// <summary>
    /// Checks for any Right Input.
    /// </summary>
    /// <return>
    /// Returns true if there is currently a right input. False if not.
    /// </return>
    public bool isRight() {
        if (Input.GetKey(right) || (Input.GetAxis("Horizontal") > 0f)) { return true; } 
        else { return false; }
    }

    /// <summary>
    /// Checks for any Left Input.
    /// </summary>
    /// <return>
    /// Returns true if there is currently a left input. False if not.
    /// </return>
    public bool isleft() {
        if (Input.GetKey(left) || (Input.GetAxis("Horizontal") < 0f)) { return true; } 
        else { return false; }
    }

    /// <summary>
    /// Checks for any Back Input.
    /// </summary>
    /// <return>
    /// Returns true if there is currently a back input. False if not.
    /// </return>
    public bool isBack() {
        if (Input.GetKey(back) || (Input.GetAxis("Vertical") < 0f)) { return true; } 
        else { return false; }
    }

    // Start is called before the first frame update
    void Start() { InitializeBoat("The River Express"); } // Initializes a Boat Instance (YOU CAN NAME THE BOAT)

    // Update is called once per frame
    void Update() {
        MoveBoat(boat.GetCurrentSpeed(), boat.GetCurrentRotation()); // Applies Velocity and Rotation to this GameObject
        ApplyDragToBoat(deacceleration);
        UpdateCurrentSpeed(this.maxSpeed, this.acceleration);
        UpdateRotation(rotationLerpSpeed, 5f);

        if (Input.GetKeyDown(KeyCode.Alpha1)) { playTest = playTestMode.playTestMode1; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { playTest = playTestMode.playTestMode2; }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { playTest = playTestMode.playTestMode3; }
    }

    /// <summary>
    /// Initializes a new Boat Object. This GameObject requires a Character Controller Component.
    /// </summary>
    /// <param name="boatName">
    /// Name of the Boat you wish to Initialize.
    /// </param>
    private void InitializeBoat(string boatName) {
        // Instantiates a new Boat
         boat = new Boat(boatName); 
        // Gets the Character Controller Component (BOAT REQUIRES CHARACTER CONTROLLER)
         characterController = this.gameObject.GetComponent<CharacterController>();
         cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
         camObject = GameObject.Find("Main Camera");
    }

    /// <summary>
    /// Transforms Boat Position in World Space with a Local Vector 3.
    /// </summary>
    /// <param name="moveVector">
    /// How many units in local space should the player move during on frame.
    /// </param>
    private void MoveBoat(float speed, float rotation) {
        Vector3 moveVector; // Vector used for current frame of movement
        Vector3 orientationVector = Vector3.Normalize(new Vector3(camObject.transform.forward.x, 0, camObject.transform.forward.z)); // Correctly Orients what "Forward" is.
        if (playTest == playTestMode.playTestMode1) {
            moveVector = gameObject.transform.forward * speed * Time.deltaTime;
            // if (cam.inMotion) { moveVector = orientationVector * speed * Time.deltaTime; }
            // else { moveVector = gameObject.transform.forward * speed * Time.deltaTime; }
        } else if (playTest == playTestMode.playTestMode2) {
            moveVector = orientationVector * speed * Time.deltaTime;
        } else if (playTest == playTestMode.playTestMode3) {
            moveVector = gameObject.transform.forward * speed * Time.deltaTime;
        } else {
            moveVector = Vector3.zero;
            Debug.Log("ERROR");
        }

        Vector3 rotationQuaternion = new Vector3(this.gameObject.transform.eulerAngles.x, rotation, this.gameObject.transform.eulerAngles.z); // Boats Current Rotation is the rotation parameter.
        applyGravity(moveVector); // Applies Gravity to the Boat
        characterController.Move(moveVector); // Move Player using Character Controller
        this.gameObject.transform.rotation = Quaternion.Euler(rotationQuaternion);
    }

    /// <summary>
    /// Applies drag to Boat forward motion.
    /// </summary>
    /// <param name="drag">
    /// Brake speed of the boat.
    /// </param>
    private void ApplyDragToBoat(float drag) {
        if (boat.GetCurrentSpeed() > 0f) { // If Boat is Moving
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() - drag); // Apply Drag
        }
    }

    /// <summary>
    /// Updates Boat's Current Speed based on Button Inputs.
    /// </summary>
    /// <param name="maxSpeed">
    /// Max speed of the Boat.
    /// </param>
    /// <param name="acceleration">
    /// Acceleration of the Boat.
    /// </param>
    private void UpdateCurrentSpeed(float maxSpeed, float acceleration) {
        if (isForward() && boat.GetCurrentSpeed() < maxSpeed) { // If Forward Input and boat is not at max speed
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() + acceleration); // Add Acceleration
            boat.SetInMotion(true);
        } else if (isBack() && boat.GetCurrentSpeed() > 0f) { // If Back Input and boat is not at 0
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() - deacceleration); // Deaccelerate
            boat.SetInMotion(true);
        } else {
            boat.SetInMotion(false); // Handles the In Motion Bool
        }
    }
    
    /// <summary>
    /// Updates Boat's Current Rotation based on Button Inputs.
    /// </summary>
    /// <param name="transitionSpeed">
    /// Transition Speed between player rotation and camera rotation.
    /// </param> 
    private void UpdateRotation(float transitionSpeed, float rotationSpeed) {

        if (playTest == playTestMode.playTestMode1) {
            if (isRight()) {
                boat.SetCurrentRotation(boat.GetCurrentRotation() + (boat.GetCurrentSpeed() * rotationSpeed / 100));
            } else if (isleft()) {
                boat.SetCurrentRotation(boat.GetCurrentRotation() - (boat.GetCurrentSpeed() * rotationSpeed / 100));
            }
            if (boat.GetCurrentRotation() > 360) {
                boat.SetCurrentRotation(boat.GetCurrentRotation() - 360f);
            } else if (boat.GetCurrentRotation() < 0) {
                boat.SetCurrentRotation(boat.GetCurrentRotation() + 360f);
            }

            // if (gameObject.transform.rotation.eulerAngles.y > 360) {
            //     gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y - 360, gameObject.transform.rotation.eulerAngles.z);
            // } else if (gameObject.transform.rotation.eulerAngles.y < 360) {
            //     gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y + 360, gameObject.transform.rotation.eulerAngles.z);
            // }

            float yRotation = gameObject.transform.rotation.eulerAngles.y;
            if (yRotation > 360) { yRotation -= 360; }
            else if (yRotation < 0) { yRotation += 360; }

            if (!cam.GetInMotion()) {
                cam.xDeg = Mathf.Lerp(cam.xDeg, gameObject.transform.rotation.eulerAngles.y, transitionSpeed); // LISTEN IF SOMETHING BUGS OUT BECAUSE U ADDED A NEW MODEL. ITS BECAUSE OF THIS STATEMENT.
                cam.yDeg = Mathf.Lerp(cam.yDeg, 20f, transitionSpeed);
                // camObject.transform.rotation = Quaternion.Slerp(camObject.transform.rotation, gameObject.transform.rotation, transitionSpeed);
                Debug.Log("REORIENTING");
            }
            

            // if (!cam.GetInMotion()) {
            //     if (isRight()) {
            //         boat.SetCurrentRotation(boat.GetCurrentRotation() + (boat.GetCurrentSpeed() * rotationSpeed / 100));
            //     } else if (isleft()) {
            //         boat.SetCurrentRotation(boat.GetCurrentRotation() - (boat.GetCurrentSpeed() * rotationSpeed / 100));
            //     }
            // } else {
            //     boat.SetCurrentRotation(Mathf.Lerp(boat.GetCurrentRotation(), cam.gameObject.transform.rotation.eulerAngles.y, transitionSpeed));
            // }
        } else if (playTest == playTestMode.playTestMode2) {
            boat.SetCurrentRotation(Mathf.Lerp(boat.GetCurrentRotation(), cam.gameObject.transform.rotation.eulerAngles.y, transitionSpeed));
        } else {
            if (isRight()) {
                boat.SetCurrentRotation(boat.GetCurrentRotation() + (boat.GetCurrentSpeed() * rotationSpeed / 100));
            } else if (isleft()) {
                boat.SetCurrentRotation(boat.GetCurrentRotation() - (boat.GetCurrentSpeed() * rotationSpeed / 100));
            }
        }








        // if (boat.GetInMotion()) {
        //     boat.SetCurrentRotation(Mathf.Lerp(boat.GetCurrentRotation(), cam.gameObject.transform.rotation.eulerAngles.y, transitionSpeed));
        // }

        // if (boat.GetInMotion() && !cam.inMotion) {
        //     if (isleft() && boat.GetCurrentSpeed() > 0f) {
        //     this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * Time.deltaTime, 0));
        // } else if(isleft()) {
        //     this.transform.Rotate(new Vector3(0, -20f * Time.deltaTime, 0));
        // } if (isRight() && boat.GetCurrentSpeed() > 0f) {
        //     this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * -Time.deltaTime, 0));
        // } else if (isRight() ) {
        //     this.transform.Rotate(new Vector3(0, -20f * -Time.deltaTime, 0));
        // }
        // } else {
        //     boat.SetCurrentRotation(Mathf.Lerp(boat.GetCurrentRotation(), cam.gameObject.transform.rotation.eulerAngles.y, transitionSpeed));
        // }

        // if (isleft() && boat.GetCurrentSpeed() > 0f) {
        //     boat.SetCurrentRotation(boat.GetCurrentRotation() + (boat.GetCurrentRotation() * Time.deltaTime));
        // } else if(isleft()) {
        //     boat.SetCurrentRotation(boat.GetCurrentRotation() + (20f * Time.deltaTime));
        // } if (isRight() && boat.GetCurrentSpeed() > 0f) {
        //     boat.SetCurrentRotation(boat.GetCurrentRotation() - (boat.GetCurrentRotation() * Time.deltaTime));
        // } else if (isRight()) {
        //     boat.SetCurrentRotation(boat.GetCurrentRotation() - (20f * Time.deltaTime));
        // }




        // boat.SetCurrentRotation(Mathf.Lerp(boat.GetCurrentRotation(), cam.gameObject.transform.rotation.eulerAngles.y, transitionSpeed));
    }


    private void applyGravity(Vector3 movement) {
        if (characterController.isGrounded) {
            movement -= Vector3.down * 3;
        }
    }
}
