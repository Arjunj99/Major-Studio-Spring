using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour {
    public enum PlayTestModes {playTestMode1, playTestMode2, playTestMode3};
    public PlayTestModes playTestMode;

    public KeyCode forward = KeyCode.W; public KeyCode left = KeyCode.A; public KeyCode right = KeyCode.D; public KeyCode back = KeyCode.S;
    public CameraMovement cameraMovement;
    public Boat boat;
    // public bool isAnchored = false;
    // public SpringJoint springJoint;
    public CharacterController characterController;

    public float drag = 0.1f;
    public float maxSpeed = 10f;
    public float acceleration = 0.2f;
    public CameraMovement2 cameraMovement2;
    public GameObject cam;




    void Awake() { // Names Boat
        boat = new Boat("SS Imperial");
    }

    void Start() { // Gets the Spring Component of Anchor
        // springJoint = gameObject.GetComponentInChildren<SpringJoint>();
        characterController = gameObject.GetComponent<CharacterController>();

        if (playTestMode == PlayTestModes.playTestMode1) {
            cam.GetComponent<CameraMovement2>().enabled = false;
        } else if (playTestMode == PlayTestModes.playTestMode2) {
            cam.GetComponent<CameraMovement>().enabled = false;
        } else if (playTestMode == PlayTestModes.playTestMode3) {
            cam.GetComponent<CameraMovement>().enabled = false;
        }
    }


    public Vector3 getMoveVector() {
        return this.gameObject.transform.forward * boat.GetCurrentSpeed() * Time.deltaTime;
    }

    public void ApplyDrag() {
        if (boat.GetCurrentSpeed() > 0f) { // If Player is in Motion, apply Drag
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() - drag);
        }
    }

    public void RegulateForwardMovement() {
        if (isForward() && boat.GetCurrentSpeed() < maxSpeed && !cameraMovement.isScouting) {
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() + acceleration);
            boat.SetInMotion(true);
        } else if (isBack() && boat.GetCurrentSpeed() > 0f && !cameraMovement.isScouting) {
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() - acceleration);
            boat.SetInMotion(true);
        } else {
            boat.SetInMotion(false);
        }
    }

    public void SetRotationSpeed() {
        boat.SetCurrentRotation(boat.GetCurrentSpeed() * -2f);
    }

    public void RegulateRotation() {
        if (isleft() && boat.GetCurrentSpeed() > 0f && !cameraMovement.isScouting) {
            this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * Time.deltaTime, 0));
        } else if(isleft() && !cameraMovement.isScouting) {
            this.transform.Rotate(new Vector3(0, -20f * Time.deltaTime, 0));
        } if (isRight() && boat.GetCurrentSpeed() > 0f && !cameraMovement.isScouting) {
            this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * -Time.deltaTime, 0));
        } else if (isRight() && !cameraMovement.isScouting ) {
            this.transform.Rotate(new Vector3(0, -20f * -Time.deltaTime, 0));
        }
    }

    public void HandleAnchor() {
        // If its anchored, deaccelerate fast and lenghten the connecting spring joint 
        // if (isAnchored) {
        //     boat.SetCurrentSpeed(Mathf.Lerp(boat.GetCurrentSpeed(), 0f, 2f * Time.deltaTime));
        //     springJoint.anchor = Vector3.Lerp(springJoint.anchor, new Vector3(0,0,0), Time.deltaTime * 2f);
        // } else {
        //     springJoint.anchor = Vector3.Lerp(springJoint.anchor, new Vector3(0,-10f,0), Time.deltaTime * 2f);
        // }

        // // Anchors the boat if E is pressed
        // if (Input.GetKeyDown(KeyCode.E) && !isAnchored) {
        //     isAnchored = true;
        // } else if (Input.GetKeyDown(KeyCode.E) && isAnchored) {
        //     isAnchored = false;
        // }
    }

    // public void SetRotation() {
    //     gameObject.transform.rotation = 
    // }

    void Update() { // Always moves forward at current Speed
        if (playTestMode == PlayTestModes.playTestMode1) {
            characterController.Move(getMoveVector());
            ApplyDrag();
            RegulateForwardMovement();
            SetRotationSpeed();
            RegulateRotation();
        } else if (playTestMode == PlayTestModes.playTestMode2) {
            characterController.Move(getMoveVector());
            ApplyDrag();
            RegulateForwardMovement();
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, cameraMovement2.cameraRotation.y + 55.14f, transform.rotation.eulerAngles.z));
        } else if (playTestMode == PlayTestModes.playTestMode3) {
            characterController.Move(getMoveVector());
            ApplyDrag();
            RegulateForwardMovement();
            SetRotationSpeed();
            RegulateRotation();
        }


        

        // HandleAnchor();

        // i




        // this.transform.Translate(new Vector3(0, 0, boat.GetCurrentSpeed() * Time.deltaTime));

        // if (boat.GetCurrentSpeed() > 0f) { // Deacceleration Values
        //     boat.SetCurrentSpeed(boat.GetCurrentSpeed() - 0.1f);
        // }

        // // Allows ship to go forward if requirements are met
        // if (isForward() && boat.GetCurrentSpeed() < 10f && !cameraMovement.isScouting && !isAnchored) {
        //     boat.SetCurrentSpeed(boat.GetCurrentSpeed() + 0.2f);
        //     boat.SetInMotion(true);
        // } else {
        //     boat.SetInMotion(false);
        // }

        // // Rotation Speed is proportional to speed
        // boat.SetCurrentRotation(boat.GetCurrentSpeed() * -2f);

        // // How the boat turns, wether it is unanchored or anchored
        // if (Input.GetKey(left) && boat.GetCurrentSpeed() > 0f && !cameraMovement.isScouting && !isAnchored) {
        //     this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * Time.deltaTime, 0));
        // } else if(Input.GetKey(left) && !cameraMovement.isScouting && isAnchored) {
        //     this.transform.Rotate(new Vector3(0, -20f * Time.deltaTime, 0));
        // }
        // if (Input.GetKey(right) && boat.GetCurrentSpeed() > 0f && !cameraMovement.isScouting && !isAnchored) {
        //     this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * -Time.deltaTime, 0));
        // } else if (Input.GetKey(right) && !cameraMovement.isScouting && isAnchored) {
        //     this.transform.Rotate(new Vector3(0, -20f * -Time.deltaTime, 0));
        // }

        // // If its anchored, deaccelerate fast and lenghten the connecting spring joint 
        // if (isAnchored) {
        //     boat.SetCurrentSpeed(Mathf.Lerp(boat.GetCurrentSpeed(), 0f, 2f * Time.deltaTime));
        //     springJoint.anchor = Vector3.Lerp(springJoint.anchor, new Vector3(0,0,0), Time.deltaTime * 2f);
        // } else {
        //     springJoint.anchor = Vector3.Lerp(springJoint.anchor, new Vector3(0,-10f,0), Time.deltaTime * 2f);
        // }

        // // Anchors the boat if E is pressed
        // if (Input.GetKeyDown(KeyCode.E) && !isAnchored) {
        //     isAnchored = true;
        // } else if (Input.GetKeyDown(KeyCode.E) && isAnchored) {
        //     isAnchored = false;
        // }
    }

    // Timer Function (Unused)
    void waitTimer(float time) {
        float timer = 0;
        while (timer < time) {
            timer += Time.deltaTime;
        }
    }

    // Dies if it collides with a bomb (unused)
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bomb") {
            Destroy(gameObject);
        }
    }

    public bool isForward() {
        if (Input.GetKey(forward) || (Input.GetAxis("Vertical") > 0f)) {
            return true;
        } else {
            return false;
        }
    }

    public bool isRight() {
        if (Input.GetKey(right) || (Input.GetAxis("Horizontal") > 0f)) {
            return true;
        } else {
            return false;
        }
    }

    public bool isleft() {
        if (Input.GetKey(left) || (Input.GetAxis("Horizontal") < 0f)) {
            return true;
        } else {
            return false;
        }
    }

    public bool isBack() {
        if (Input.GetKey(back) || (Input.GetAxis("Vertical") < 0f)) {
            return true;
        } else {
            return false;
        }
    }
}



//     public KeyCode forward; public KeyCode left; public KeyCode right; public KeyCode back;
//     public CameraMovement cameraMovement;
//     public Boat boat;
//     public bool isAnchored = false;
//     public SpringJoint springJoint;

//     void Awake() { // Names Boat
//         boat = new Boat("SS Imperial");
//     }

//     void Start() { // Gets the Spring Component of Anchor
//         springJoint = gameObject.GetComponentInChildren<SpringJoint>();
//     }

//     void Update() { // Always moves forward at current Speed
//         this.transform.Translate(new Vector3(0, 0, boat.GetCurrentSpeed() * Time.deltaTime));

//         if (boat.GetCurrentSpeed() > 0f) { // Deacceleration Values
//             boat.SetCurrentSpeed(boat.GetCurrentSpeed() - 0.1f);
//         }

//         // Allows ship to go forward if requirements are met
//         if (Input.GetKey(forward) && boat.GetCurrentSpeed() < 10f && !cameraMovement.isScouting && !isAnchored) {
//             boat.SetCurrentSpeed(boat.GetCurrentSpeed() + 0.2f);
//             boat.SetInMotion(true);
//         } else {
//             boat.SetInMotion(false);
//         }

//         // Rotation Speed is proportional to speed
//         boat.SetCurrentRotation(boat.GetCurrentSpeed() * -2f);

//         // How the boat turns, wether it is unanchored or anchored
//         if (Input.GetKey(left) && boat.GetCurrentSpeed() > 0f && !cameraMovement.isScouting && !isAnchored) {
//             this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * Time.deltaTime, 0));
//         } else if(Input.GetKey(left) && !cameraMovement.isScouting && isAnchored) {
//             this.transform.Rotate(new Vector3(0, -20f * Time.deltaTime, 0));
//         }
//         if (Input.GetKey(right) && boat.GetCurrentSpeed() > 0f && !cameraMovement.isScouting && !isAnchored) {
//             this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * -Time.deltaTime, 0));
//         } else if (Input.GetKey(right) && !cameraMovement.isScouting && isAnchored) {
//             this.transform.Rotate(new Vector3(0, -20f * -Time.deltaTime, 0));
//         }

//         // If its anchored, deaccelerate fast and lenghten the connecting spring joint 
//         if (isAnchored) {
//             boat.SetCurrentSpeed(Mathf.Lerp(boat.GetCurrentSpeed(), 0f, 2f * Time.deltaTime));
//             springJoint.anchor = Vector3.Lerp(springJoint.anchor, new Vector3(0,0,0), Time.deltaTime * 2f);
//         } else {
//             springJoint.anchor = Vector3.Lerp(springJoint.anchor, new Vector3(0,-10f,0), Time.deltaTime * 2f);
//         }

//         // Anchors the boat if E is pressed
//         if (Input.GetKeyDown(KeyCode.E) && !isAnchored) {
//             isAnchored = true;
//         } else if (Input.GetKeyDown(KeyCode.E) && isAnchored) {
//             isAnchored = false;
//         }
//     }

//     // Timer Function (Unused)
//     void waitTimer(float time) {
//         float timer = 0;
//         while (timer < time) {
//             timer += Time.deltaTime;
//         }
//     }

//     // Dies if it collides with a bomb (unused)
//     void OnCollisionEnter(Collision collision) {
//         if (collision.gameObject.tag == "Bomb") {
//             Destroy(gameObject);
//         }
//     }
// }





// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BoatController : MonoBehaviour {
//     public float moveSpeed = 0;
//     public float rotationSpeed = 0;
//     private bool isInMotion = false;
//     public Rigidbody rb;
//     public TextMesh HintText;
//     private bool whileSailing = true;
//     private bool won = false;
//     public GameObject camera;
//     public CameraMovement cameraMovement;
//     public bool canQ;

//     public List<GameObject> IslandList = new List<GameObject>();

//     // Start is called before the first frame update
//     void Start() {
        
//     }

//     // Update is called once per frame
//     void Update() {
//         this.transform.Translate(new Vector3(0,moveSpeed*Time.deltaTime,0));
//         if (moveSpeed > 0f)
//             moveSpeed -= 0.1f;

//         if(Input.GetKey(KeyCode.W) && moveSpeed < 10f && !cameraMovement.isScouting) {
//             moveSpeed += 0.2f;
//             // this.transform.Translate(new Vector3(0,moveSpeed*Time.deltaTime,0));
//             isInMotion = true;
//         } else {
//             isInMotion = false;
//         }

//         rotationSpeed = moveSpeed * -2f;


//         // if(Input.GetKey(KeyCode.S)) {
//         //     this.transform.Translate(new Vector3(0,-moveSpeed*Time.deltaTime,0));
//         // }
//         if(Input.GetKey(KeyCode.A) && moveSpeed > 0 && !cameraMovement.isScouting) {
//             this.transform.Rotate(new Vector3(0,0,rotationSpeed*Time.deltaTime));
//             camera.SendMessage("Turn", rotationSpeed*Time.deltaTime);
//         }
//         if(Input.GetKey(KeyCode.D) && moveSpeed > 0 && !cameraMovement.isScouting) {
//             this.transform.Rotate(new Vector3(0,0,-rotationSpeed*Time.deltaTime));
//             camera.SendMessage("Turn", -rotationSpeed*Time.deltaTime);
//         }
//         if (whileSailing) {
//             if(Vector3.Distance(this.transform.position, IslandList[13].transform.position) < 50 && Input.GetKey(KeyCode.Space)) {
//                 won = true;
//                 whileSailing = false;
//                 // HintText.text = "YOU WON!!!";
//             // } else if(Input.GetKey(KeyCode.Space)) {
//             //     whileSailing = false;
//             //     // HintText.text = "This isn't the right city.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[0].transform.position) < 50) {
//                 HintText.text = "I have a quest for you. Find my City! \n It is found in a far off country among an invading force.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[1].transform.position) < 50) {
//                 HintText.text = "This statue marks the Kingdoms of Demons";
//             } else if(Vector3.Distance(this.transform.position, IslandList[2].transform.position) < 50) {
//                 HintText.text = "The Demon City of Azimir.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[3].transform.position) < 50) {
//                 HintText.text = "The Port City of Quazir.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[4].transform.position) < 50) {
//                 canQ = true;
//                 HintText.text = "A great battleground.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[5].transform.position) < 50) {
//                 HintText.text = "The Infernal Capital of Demons.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[6].transform.position) < 50) {
//                 HintText.text = "A nearby city is under seige by Knights.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[7].transform.position) < 50) {
//                 HintText.text = "Two giants battle here.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[8].transform.position) < 50) {
//                 HintText.text = "A Shipwreck.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[9].transform.position) < 50) {
//                 HintText.text = "The Shin Villages.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[10].transform.position) < 50) {
//                 HintText.text = "This statue marks the Kingdoms of the Knights.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[11].transform.position) < 50) {
//                 HintText.text = "The Knight City of Kholinar.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[12].transform.position) < 50) {
//                 HintText.text = "A nearby city is under seige by Demons.";
//             } else if(Vector3.Distance(this.transform.position, IslandList[13].transform.position) < 50) {
//                 HintText.text = "The Holy City of Urithru. \n Press Space to Dock into the City of Treasures";
//             } else {
//                 HintText.text = "";
//                 canQ = false;
//             }
//         } else {
//             if (won) {
//                 HintText.text = "!!!YOU WON!!!";
//             } else {
//                 // HintText.text = "THIS ISN'T THE RIGHT CITY";
//                 // System.Threading.Thread.Sleep(5000);
//                 // waitTimer(3);
//                 // StartCoroutine(wait());
//                 whileSailing = true;
                
//             }
//         }
        
//     }

//     void waitTimer(float time) {
//         float timer = 0;
//         while (timer < time) {
//             timer += Time.deltaTime;
//         }
//     }
//     // IEnumerator wait() {
//     //     yield return new WaitForSeconds(3);
//     // }

// }
