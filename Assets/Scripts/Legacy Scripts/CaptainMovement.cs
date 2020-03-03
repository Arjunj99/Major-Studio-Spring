// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CaptainMovement : MonoBehaviour {
//     public CameraMovement cameraMovement;
    
//     public float moveSpeed = 0;
//     public float rotationSpeed = -10;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update() {
//         if (cameraMovement.canDisembark) {
//             // this.transform.Translate(new Vector3(0,0,moveSpeed*Time.deltaTime));
//             if(Input.GetKey(KeyCode.W) && moveSpeed < 10f) {
//                 this.transform.Translate(new Vector3(-moveSpeed*Time.deltaTime,0,0));
//             } else if (Input.GetKey(KeyCode.S) && moveSpeed < 10f) {
//                 this.transform.Translate(new Vector3(moveSpeed*Time.deltaTime,0,0));
//             } else if (Input.GetKey(KeyCode.A) && moveSpeed < 10f) {
//                 this.transform.Translate(new Vector3(0,moveSpeed*Time.deltaTime,0));
//             } else if (Input.GetKey(KeyCode.D) && moveSpeed < 10f) {
//                 this.transform.Translate(new Vector3(0,-moveSpeed*Time.deltaTime,0));
//             }
        
//         // this.transform.Translate(new Vector3(0,0,moveSpeed*Time.deltaTime));

//         // if (moveSpeed > 0f)
//         //     moveSpeed -= 0.1f;

//         // if(Input.GetKey(KeyCode.W) && moveSpeed < 10f && !cameraMovement.isScouting) {
//         //     moveSpeed += 0.2f;
//         //     // this.transform.Translate(new Vector3(0,moveSpeed*Time.deltaTime,0));
//         //     // isInMotion = true;
//         // } else {
//         //     // isInMotion = false;
//         // }

//         // rotationSpeed = moveSpeed * -2f;


//         // if(Input.GetKey(KeyCode.S)) {
//         //     this.transform.Translate(new Vector3(0,-moveSpeed*Time.deltaTime,0));
//         // }
//         // if(Input.GetKey(KeyCode.A) && moveSpeed > 0 && !cameraMovement.isScouting) {
//         //     this.transform.Rotate(new Vector3(0,0,rotationSpeed*Time.deltaTime));
//         //     cameraMovement.SendMessage("Turn", rotationSpeed*Time.deltaTime);
//         // }
//         // if(Input.GetKey(KeyCode.D) && moveSpeed > 0 && !cameraMovement.isScouting) {
//         //     this.transform.Rotate(new Vector3(0,0,-rotationSpeed*Time.deltaTime));
//         //     cameraMovement.SendMessage("Turn", -rotationSpeed*Time.deltaTime);
//         // }
//         }
//     }
// }
