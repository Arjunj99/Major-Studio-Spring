﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> MonoBehavior Class that allows for Boat Movement. Requires. </summary>
public class BoatMovement : MonoBehaviour {
    [HideInInspector] public KeyCode forward = KeyCode.W, left = KeyCode.A, right = KeyCode.D, back = KeyCode.S; // Player Key Inputs for Boat
    [HideInInspector] public CharacterController characterController; // CharacterController of the Boat Object
    [HideInInspector] public Boat boat; // Boat Instance that stores Movement Data
    [HideInInspector] public CameraController cam; // CameraController for the Main Camera 
    [SerializeField] private float deacceleration; // Rate of Deacceleration
    [SerializeField] private float acceleration; // Rate of Acceleration
    [SerializeField] private float maxSpeed; // Max Speed of Boat
    [SerializeField] private float rotationLerpSpeed; // Lerp Speed between Rotations
    public int levelSpeed = 0;
    public float rotationVal;

    float yEuler, yEulerPast;

    public Vector3 currentForce = Vector3.zero;

    public bool inCurrent = false;

    public float gravity = 0f;

    float rotationTime = 0;
    public float maxTime = 0.4f;

    public int rotlimit = 300;

    public int[] speeds;
    public int[] waterArr;
    public int[] smokeArr;
    public int[] emberArr;
    public ParticleSystem water;
    public ParticleSystem smoke;
    public ParticleSystem ember;
    public bool lookUp;
    public Vector3 moveVector; // Vector used for current frame of movement

    public float grav;
    public bool cutscene;
    public Vector3 finalPosition;
    public Vector3 finalRotation;
    public float finalMod = 6f;
    public bool isChanging;
    public AudioSource audioSource;
    public AudioClip honkNoise;



    /// <summary> Checks for any Forward Input. </summary>
    /// <return> Returns true if there is currently a forward input. False if not. </return>
    public bool isForward() {
        if (Input.GetKey(forward) || (Input.GetAxis("Vertical") > 0f) || Input.GetKey(KeyCode.UpArrow)) { return true; } 
        else { return false; }
    }

    /// <summary> Checks for any Right Input. </summary>
    /// <return> Returns true if there is currently a right input. False if not. </return>
    public bool isRight() {
        if (Input.GetKey(right) || (Input.GetAxis("Horizontal") > 0f) || Input.GetKey(KeyCode.RightArrow)) { return true; } 
        else { return false; }
    }

    /// <summary> Checks for any Left Input. </summary>
    /// <return> Returns true if there is currently a left input. False if not. </return>
    public bool isleft() {
        if (Input.GetKey(left) || (Input.GetAxis("Horizontal") < 0f) || Input.GetKey(KeyCode.LeftArrow)) { return true; } 
        else { return false; }
    }

    /// <summary> Checks for any Back Input. </summary>
    /// <return>  Returns true if there is currently a back input. False if not. </return>
    public bool isBack() {
        if (Input.GetKey(back) || (Input.GetAxis("Vertical") < 0f) || Input.GetKey(KeyCode.DownArrow)) { return true; } 
        else { return false; }
    }

    // Start is called before the first frame update
    void Start() { 
        // Initializes a Boat Instance (YOU CAN NAME THE BOAT)
        InitializeBoat("The River Express"); 
        boat.SetCurrentRotation(gameObject.transform.eulerAngles.y); //Could be the source of the bug
    } 

    // Update is called once per frame

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            audioSource.PlayOneShot(honkNoise);
        }
    }

    void FixedUpdate() {
        if (!cutscene) {
            MoveBoat(boat.GetCurrentSpeed(), boat.GetCurrentRotation()); // Applies Velocity and Rotation to this GameObject
            ApplyDragToBoat(deacceleration);
            UpdateCurrentSpeed(this.maxSpeed, this.acceleration);
            UpdateRotation(rotationLerpSpeed, rotationVal);

            water.emissionRate = Mathf.Lerp(water.emissionRate, waterArr[levelSpeed + 1], Time.deltaTime);
            smoke.emissionRate = Mathf.Lerp(smoke.emissionRate, smokeArr[levelSpeed + 1], Time.deltaTime);
            ember.emissionRate = Mathf.Lerp(ember.emissionRate, emberArr[levelSpeed + 1], Time.deltaTime);




            if (Input.GetKey(KeyCode.LeftShift)) {
                lookUp = true;
            } else {
                lookUp = false;
            }

            if (lookUp) {
                cam.playerHeight = Mathf.Lerp(cam.playerHeight, 13, Time.deltaTime);
                cam.yMinLimit = Mathf.Lerp(cam.yMinLimit, -30, Time.deltaTime);
                cam.yMaxLimit = Mathf.Lerp(cam.yMaxLimit, -30, Time.deltaTime);
            } else {
                cam.playerHeight = Mathf.Lerp(cam.playerHeight, 7, Time.deltaTime);
                cam.yMinLimit = Mathf.Lerp(cam.yMinLimit, 10, Time.deltaTime);
                cam.yMaxLimit = Mathf.Lerp(cam.yMaxLimit, 10, Time.deltaTime);
            }


            // if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene("ArjunScene"); }
        } else {
            this.transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime/finalMod);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finalRotation), Time.deltaTime/finalMod);
        }
    }

    /// <summary> Initializes a new Boat Object. This GameObject requires a Character Controller Component. </summary>
    /// <param name="boatName">  Name of the Boat you wish to Initialize. </param>
    private void InitializeBoat(string boatName) {
        // Instantiates a new Boat
         boat = new Boat(boatName); 
        // Gets the Character Controller Component (BOAT REQUIRES CHARACTER CONTROLLER)
         characterController = this.gameObject.GetComponent<CharacterController>();
         cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    /// <summary> Transforms Boat Position in World Space with a Local Vector 3. </summary>
    /// <param name="speed"> Magnitude in local space should the player move during on frame. </param>
    /// <param name="rotation"> Magnitude in local space should the player move during on frame. </param>
    private void MoveBoat(float speed, float rotation) {
        // Debug.Log("SPEED: " + speed);
        // Debug.Log("MAGNITUDE: " + moveVector.magnitude);
        // Debug.Log("MAGNITUDE C: " + currentForce.magnitude);
        moveVector = Vector3.Normalize(new Vector3(transform.forward.x, 0, transform.forward.z));
        moveVector *= (speed * Time.deltaTime);

        Vector3 rotationQuaternion = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z); // Boats Current Rotation is the rotation parameter.
        moveVector = applyGravity(moveVector); // Applies Gravity to the Boat

        characterController.Move(moveVector + currentForce); // Move Player using Character Controller
        transform.rotation = Quaternion.Euler(rotationQuaternion);
    }

    /// <summary> Applies drag to Boat forward motion. </summary>
    /// <param name="drag"> Brake speed of the boat. </param>
    private void ApplyDragToBoat(float drag) {
        if (boat.GetCurrentSpeed() > 0f) { // If Boat is Moving
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() - drag); // Apply Drag
        }
    }

    //BIG SAD
    /// <summary> Updates Boat's Current Speed based on Button Inputs. </summary>
    /// <param name="maxSpeed"> Max speed of the Boat. </param>
    /// <param name="acceleration"> Acceleration of the Boat. </param>
    private void UpdateCurrentSpeed(float maxSpeed, float acceleration) {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
            isChanging = false;
        }


        // Adjust speed
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && levelSpeed < 2) {
            if (!isChanging) {
                // Debug.Log("W");
                levelSpeed += 1; 
                isChanging = true;
            }
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && levelSpeed > -1) {
            if (!isChanging) {
                // Debug.Log("W");
                levelSpeed -= 1; 
                isChanging = true;
            }
        }


        // if ((Input.GetButtonDown("joystick button 4") || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && levelSpeed > -1) { levelSpeed -= 1; }

        boat.SetCurrentSpeed(Mathf.Lerp(boat.GetCurrentSpeed(), speeds[levelSpeed + 1], Time.deltaTime));
        
        if (levelSpeed != 0) { boat.SetInMotion(true); } 
        else { boat.SetInMotion(false); }
    }
    
    /// <summary> Updates Boat's Current Rotation based on Button Inputs. </summary>
    /// <param name="transitionSpeed"> Transition Speed between player rotation and camera rotation. </param> 
    /// <param name="rotationSpeed"> Transition Speed between player rotation and camera rotation. </param> 
    private void UpdateRotation(float transitionSpeed, float rotationSpeed) {
        if (Input.GetButton("joystick button 7") || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            rotationTime = Mathf.Lerp(rotationTime, 1, maxTime);
        } else if (Input.GetButton("joystick button 6") || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            rotationTime = Mathf.Lerp(rotationTime, -1, maxTime);
        }

        float rotatationSpeed = boat.GetCurrentSpeed() * rotationTime;   


        if (Input.GetButton("joystick button 7") || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            boat.SetCurrentRotation(boat.GetCurrentRotation() + (rotationSpeed / rotlimit));
        } else if (Input.GetButton("joystick button 6") || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            boat.SetCurrentRotation(boat.GetCurrentRotation() - (rotationSpeed / rotlimit));
        }

        float yRotation = gameObject.transform.rotation.eulerAngles.y;

        if (!cam.GetInMotion()) {
            yEuler = gameObject.transform.rotation.eulerAngles.y;
            cam.xDeg = Mathf.Lerp(cam.xDeg, boat.GetCurrentRotation(), transitionSpeed);
            cam.yDeg = Mathf.Lerp(cam.yDeg, 20f, transitionSpeed);
        }
    }

    private Vector3 applyGravity(Vector3 movement) {
        if (!characterController.isGrounded) {
            gravity += grav;
            movement.y -= gravity;
        } else {
            gravity = 0f;
        }
        return movement;
    }
}











// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class BoatMovement : MonoBehaviour {
//     public KeyCode forward = KeyCode.W, left = KeyCode.A, right = KeyCode.D, back = KeyCode.S; // Player Key Inputs for Boat
//     public enum playTestMode { playTestMode1, playTestMode2, playTestMode3 }; // Possible PlayTestModes (Use only for Debug)
//     public playTestMode playTest; // Current PlayTest Mode
//     [HideInInspector] public CharacterController characterController; // CharacterController of the Boat Object
//     [HideInInspector] public Boat boat; // Boat Instance that stores Movement Data
//     [HideInInspector] public CameraController cam; // CameraController for the Main Camera 
//     [HideInInspector] public GameObject camObject; // Main Camera Object
//     [SerializeField] private float deacceleration; // Rate of Deacceleration
//     [SerializeField] private float acceleration; // Rate of Acceleration
//     [SerializeField] private float maxSpeed; // Max Speed of Boat
//     [SerializeField] private float rotationLerpSpeed; // Lerp Speed between Rotations
//     int turns = 0;
//     public int levelSpeed = 0;
//     public float rotationVal;

//     float yEuler, yEulerPast;

//     public Vector3 currentForce = Vector3.zero;

//     public bool inCurrent = false;

//     public float gravity = 3f;

//     float rotationTime = 0;
//     public float maxTime = 0.4f;



//     /// <summary>
//     /// Checks for any Forward Input.
//     /// </summary>
//     /// <return>
//     /// Returns true if there is currently a forward input. False if not.
//     /// </return>
//     public bool isForward() {
//         if (Input.GetKey(forward) || (Input.GetAxis("Vertical") > 0f)) { return true; } 
//         else { return false; }
//     }

//     /// <summary>
//     /// Checks for any Right Input.
//     /// </summary>
//     /// <return>
//     /// Returns true if there is currently a right input. False if not.
//     /// </return>
//     public bool isRight() {
//         if (Input.GetKey(right) || (Input.GetAxis("Horizontal") > 0f)) { return true; } 
//         else { return false; }
//     }

//     /// <summary>
//     /// Checks for any Left Input.
//     /// </summary>
//     /// <return>
//     /// Returns true if there is currently a left input. False if not.
//     /// </return>
//     public bool isleft() {
//         if (Input.GetKey(left) || (Input.GetAxis("Horizontal") < 0f)) { return true; } 
//         else { return false; }
//     }

//     /// <summary>
//     /// Checks for any Back Input.
//     /// </summary>
//     /// <return>
//     /// Returns true if there is currently a back input. False if not.
//     /// </return>
//     public bool isBack() {
//         if (Input.GetKey(back) || (Input.GetAxis("Vertical") < 0f)) { return true; } 
//         else { return false; }
//     }

//     // Start is called before the first frame update
//     void Start() { 
//         playTest = playTestMode.playTestMode2;
        
//         InitializeBoat("The River Express"); boat.SetCurrentRotation(gameObject.transform.eulerAngles.y); } // Initializes a Boat Instance (YOU CAN NAME THE BOAT)

//     // Update is called once per frame
//     void Update() {
//         MoveBoat(boat.GetCurrentSpeed(), boat.GetCurrentRotation()); // Applies Velocity and Rotation to this GameObject
//         ApplyDragToBoat(deacceleration);
//         UpdateCurrentSpeed(this.maxSpeed, this.acceleration);
//         UpdateRotation(rotationLerpSpeed, rotationVal);

//         // if (Input.GetKeyDown(KeyCode.Alpha1)) { playTest = playTestMode.playTestMode1; }
//         // else if (Input.GetKeyDown(KeyCode.Alpha2)) { playTest = playTestMode.playTestMode2; }


//                 float yEuler = gameObject.transform.rotation.eulerAngles.y;
        

//                 // cam.xDeg = yEuler; // LISTEN IF SOMETHING BUGS OUT BECAUSE U ADDED A NEW MODEL. ITS BECAUSE OF THIS STATEMENT.
//                 if (Input.GetKeyDown(KeyCode.C)) {
//                     Debug.Log("X Degress: " + cam.xDeg);
//                     Debug.Log("Y Rotation: " + yEuler);
//                 }

//         if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene("ArjunScene"); }

//         // if (!inCurrent) { currentForce = Vector3.zero; }
//     }

//     /// <summary>
//     /// Initializes a new Boat Object. This GameObject requires a Character Controller Component.
//     /// </summary>
//     /// <param name="boatName">
//     /// Name of the Boat you wish to Initialize.
//     /// </param>
//     private void InitializeBoat(string boatName) {
//         // Instantiates a new Boat
//          boat = new Boat(boatName); 
//         // Gets the Character Controller Component (BOAT REQUIRES CHARACTER CONTROLLER)
//          characterController = this.gameObject.GetComponent<CharacterController>();
//          cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
//          camObject = GameObject.Find("Main Camera");
//     }

//     /// <summary>
//     /// Transforms Boat Position in World Space with a Local Vector 3.
//     /// </summary>
//     /// <param name="moveVector">
//     /// How many units in local space should the player move during on frame.
//     /// </param>
//     private void MoveBoat(float speed, float rotation) {
//         Vector3 moveVector; // Vector used for current frame of movement
//         Vector3 orientationVector;
//         if (playTest == playTestMode.playTestMode1) { orientationVector = Vector3.Normalize(new Vector3(camObject.transform.forward.x, 0, camObject.transform.forward.z)); } // Correctly Orients what "Forward" is.
//         else { orientationVector = Vector3.Normalize(new Vector3(gameObject.transform.forward.x, 0, gameObject.transform.forward.z)); }
//         if (playTest == playTestMode.playTestMode1) {
//             moveVector = gameObject.transform.forward * speed * Time.deltaTime;
//         } else if (playTest == playTestMode.playTestMode2) {
//             moveVector = orientationVector * speed * Time.deltaTime;
//         } else {
//             moveVector = Vector3.zero;
//             Debug.Log("ERROR");
//         }

//         Vector3 rotationQuaternion = new Vector3(this.gameObject.transform.eulerAngles.x, rotation, this.gameObject.transform.eulerAngles.z); // Boats Current Rotation is the rotation parameter.
//         applyGravity(moveVector); // Applies Gravity to the Boat
//         characterController.Move(moveVector + currentForce); // Move Player using Character Controller
//         this.gameObject.transform.rotation = Quaternion.Euler(rotationQuaternion);
//     }

//     /// <summary>
//     /// Applies drag to Boat forward motion.
//     /// </summary>
//     /// <param name="drag">
//     /// Brake speed of the boat.
//     /// </param>
//     private void ApplyDragToBoat(float drag) {
//         if (boat.GetCurrentSpeed() > 0f) { // If Boat is Moving
//             boat.SetCurrentSpeed(boat.GetCurrentSpeed() - drag); // Apply Drag
//         }
//     }

//     /// <summary>
//     /// Updates Boat's Current Speed based on Button Inputs.
//     /// </summary>
//     /// <param name="maxSpeed">
//     /// Max speed of the Boat.
//     /// </param>
//     /// <param name="acceleration">
//     /// Acceleration of the Boat.
//     /// </param>
//     private void UpdateCurrentSpeed(float maxSpeed, float acceleration) {
//         if (playTest == playTestMode.playTestMode1) {
//             if (isForward() && boat.GetCurrentSpeed() < maxSpeed) { // If Forward Input and boat is not at max speed
//                 boat.SetCurrentSpeed(boat.GetCurrentSpeed() + acceleration); // Add Acceleration
//                 boat.SetInMotion(true);
//             } else if (isBack() && boat.GetCurrentSpeed() > 0f) { // If Back Input and boat is not at 0
//                 boat.SetCurrentSpeed(boat.GetCurrentSpeed() - deacceleration); // Deaccelerate
//                 boat.SetInMotion(true);
//             } else {
//                 boat.SetInMotion(false); // Handles the In Motion Bool
//             }
//         } else if (playTest == playTestMode.playTestMode2) {
//             if ((Input.GetButtonDown("joystick button 5") || (Input.GetKeyDown(KeyCode.W)) && levelSpeed < 2)) { levelSpeed += 1; } 
//             if ((Input.GetButtonDown("joystick button 4") || (Input.GetKeyDown(KeyCode.S)) && levelSpeed > -1)) { levelSpeed -= 1; }

//             boat.SetCurrentSpeed(Mathf.Lerp(boat.GetCurrentSpeed(), levelSpeed * 10, Time.deltaTime));
//             if (levelSpeed != 0) { boat.SetInMotion(true); }
//         }
//     }
    
//     /// <summary>
//     /// Updates Boat's Current Rotation based on Button Inputs.
//     /// </summary>
//     /// <param name="transitionSpeed">
//     /// Transition Speed between player rotation and camera rotation.
//     /// </param> 
//     private void UpdateRotation(float transitionSpeed, float rotationSpeed) {
//         // bool directionRight;
//         if (playTest == playTestMode.playTestMode1) {
//             if (isRight()) {
//                 boat.SetCurrentRotation(boat.GetCurrentRotation() + (rotationSpeed / 100));
//             } else if (isleft()) {
//                 boat.SetCurrentRotation(boat.GetCurrentRotation() - (rotationSpeed / 100));
//             }

//             float yRotation = gameObject.transform.rotation.eulerAngles.y;

//             if (!cam.GetInMotion()) {
//                 yEuler = gameObject.transform.rotation.eulerAngles.y;
//                 cam.xDeg = Mathf.Lerp(cam.xDeg, boat.GetCurrentRotation(), transitionSpeed);
//                 if (Input.GetKeyDown(KeyCode.C)) {
//                     Debug.Log("X Degress: " + cam.xDeg);
//                     Debug.Log("Y Rotation: " + boat.GetCurrentRotation());
//                 }
//                 cam.yDeg = Mathf.Lerp(cam.yDeg, 20f, transitionSpeed);
//             }
//         } else if (playTest == playTestMode.playTestMode2) {
//             if (Input.GetButton("joystick button 7") || Input.GetKey(KeyCode.D)) {
//                 rotationTime = Mathf.Lerp(rotationTime, 1, maxTime);
//             } else if (Input.GetButton("joystick button 6") || Input.GetKey(KeyCode.A)) {
//                 rotationTime = Mathf.Lerp(rotationTime, -1, maxTime);
//             }

//             float rotatationSpeed = boat.GetCurrentSpeed() * rotationTime;   




//             if (Input.GetButton("joystick button 7") || Input.GetKey(KeyCode.D)) {
//                 Debug.Log("7");
//                 boat.SetCurrentRotation(boat.GetCurrentRotation() + (rotationSpeed / 500));
//             } else if (Input.GetButton("joystick button 6") || Input.GetKey(KeyCode.A)) {
//                 Debug.Log("6");
//                 boat.SetCurrentRotation(boat.GetCurrentRotation() + (rotationSpeed / 500));
//             }

//             float yRotation = gameObject.transform.rotation.eulerAngles.y;

//             if (!cam.GetInMotion()) {
//                 yEuler = gameObject.transform.rotation.eulerAngles.y;
//                 cam.xDeg = Mathf.Lerp(cam.xDeg, boat.GetCurrentRotation(), transitionSpeed);
//                 if (Input.GetKeyDown(KeyCode.C)) {
//                     Debug.Log("X Degress: " + cam.xDeg);
//                     Debug.Log("Y Rotation: " + boat.GetCurrentRotation());
//                 }
//                 cam.yDeg = Mathf.Lerp(cam.yDeg, 20f, transitionSpeed);
//             }
//         } else {
//             if (isRight()) {
//                 boat.SetCurrentRotation(boat.GetCurrentRotation() + (boat.GetCurrentSpeed() * rotationSpeed / 100));
//             } else if (isleft()) {
//                 boat.SetCurrentRotation(boat.GetCurrentRotation() - (boat.GetCurrentSpeed() * rotationSpeed / 100));
//             }
//         }








//         // if (boat.GetInMotion()) {
//         //     boat.SetCurrentRotation(Mathf.Lerp(boat.GetCurrentRotation(), cam.gameObject.transform.rotation.eulerAngles.y, transitionSpeed));
//         // }

//         // if (boat.GetInMotion() && !cam.inMotion) {
//         //     if (isleft() && boat.GetCurrentSpeed() > 0f) {
//         //     this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * Time.deltaTime, 0));
//         // } else if(isleft()) {
//         //     this.transform.Rotate(new Vector3(0, -20f * Time.deltaTime, 0));
//         // } if (isRight() && boat.GetCurrentSpeed() > 0f) {
//         //     this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * -Time.deltaTime, 0));
//         // } else if (isRight() ) {
//         //     this.transform.Rotate(new Vector3(0, -20f * -Time.deltaTime, 0));
//         // }
//         // } else {
//         //     boat.SetCurrentRotation(Mathf.Lerp(boat.GetCurrentRotation(), cam.gameObject.transform.rotation.eulerAngles.y, transitionSpeed));
//         // }

//         // if (isleft() && boat.GetCurrentSpeed() > 0f) {
//         //     boat.SetCurrentRotation(boat.GetCurrentRotation() + (boat.GetCurrentRotation() * Time.deltaTime));
//         // } else if(isleft()) {
//         //     boat.SetCurrentRotation(boat.GetCurrentRotation() + (20f * Time.deltaTime));
//         // } if (isRight() && boat.GetCurrentSpeed() > 0f) {
//         //     boat.SetCurrentRotation(boat.GetCurrentRotation() - (boat.GetCurrentRotation() * Time.deltaTime));
//         // } else if (isRight()) {
//         //     boat.SetCurrentRotation(boat.GetCurrentRotation() - (20f * Time.deltaTime));
//         // }




//         // boat.SetCurrentRotation(Mathf.Lerp(boat.GetCurrentRotation(), cam.gameObject.transform.rotation.eulerAngles.y, transitionSpeed));
//     }


//     private void applyGravity(Vector3 movement) {
//         if (!characterController.isGrounded) {
//             characterController.Move(Vector3.down * gravity);
//         }
//     }
// }