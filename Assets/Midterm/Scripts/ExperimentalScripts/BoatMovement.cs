using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {
    public KeyCode forward = KeyCode.W, left = KeyCode.A, right = KeyCode.D, back = KeyCode.S;
    public CharacterController characterController;
    [HideInInspector] public Boat boat;
    [HideInInspector] public CameraController cam;
    [HideInInspector] public GameObject camObject;

    [SerializeField] private float deacceleration;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationLerpSpeed;

    /// <summary>
    /// Checks for any Forward Input.
    /// </summary>
    /// <return>
    /// Returns true if there is currently a forward input. False if not.
    /// </return>
    public bool isForward() {
        if (Input.GetKey(forward) || (Input.GetAxis("Vertical") > 0f)) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Checks for any Right Input.
    /// </summary>
    /// <return>
    /// Returns true if there is currently a right input. False if not.
    /// </return>
    public bool isRight() {
        if (Input.GetKey(right) || (Input.GetAxis("Horizontal") > 0f)) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Checks for any Left Input.
    /// </summary>
    /// <return>
    /// Returns true if there is currently a left input. False if not.
    /// </return>
    public bool isleft() {
        if (Input.GetKey(left) || (Input.GetAxis("Horizontal") < 0f)) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Checks for any Back Input.
    /// </summary>
    /// <return>
    /// Returns true if there is currently a back input. False if not.
    /// </return>
    public bool isBack() {
        if (Input.GetKey(back) || (Input.GetAxis("Vertical") < 0f)) {
            return true;
        } else {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start() { 
        InitializeBoat("The River Express"); }

    // Update is called once per frame
    void Update() {
        MoveBoat(boat.GetCurrentSpeed(), boat.GetCurrentRotation());
        ApplyDragToBoat(deacceleration);
        UpdateCurrentSpeed(this.maxSpeed, this.acceleration);
        UpdateRotation(rotationLerpSpeed, 5f);
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
        Vector3 moveVector;
        if (cam.inMotion) { moveVector = camObject.transform.forward * speed * Time.deltaTime; }
        else { 
            moveVector = gameObject.transform.forward * speed * Time.deltaTime; 
            }
        Vector3 rotationQuaternion = new Vector3(this.gameObject.transform.eulerAngles.x, rotation, this.gameObject.transform.eulerAngles.z);
        applyGravity(moveVector);
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
        if (isForward() && boat.GetCurrentSpeed() < maxSpeed) {
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() + acceleration);
            boat.SetInMotion(true);
        } else if (isBack() && boat.GetCurrentSpeed() > -maxSpeed) {
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() - acceleration);
            boat.SetInMotion(true);
        } else {
            boat.SetInMotion(false);
        }
    }
    
    /// <summary>
    /// Updates Boat's Current Rotation based on Button Inputs.
    /// </summary>
    /// <param name="transitionSpeed">
    /// Transition Speed between player rotation and camera rotation.
    /// </param> 
    private void UpdateRotation(float transitionSpeed, float rotationSpeed) {
        if (Input.GetKey(right)) {
            boat.SetCurrentRotation(boat.GetCurrentRotation() + (boat.GetCurrentSpeed() * rotationSpeed / 100));
        } else if (Input.GetKey(left)) {
            boat.SetCurrentRotation(boat.GetCurrentRotation() - (boat.GetCurrentSpeed() * rotationSpeed / 100));
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
