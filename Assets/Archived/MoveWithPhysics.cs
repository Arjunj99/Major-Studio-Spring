using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPhysics : MonoBehaviour {
    public float moveSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() { // UPDATE IS CALLED A RESERVED FUNCTION
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(this.transform.forward, ForceMode.Force);
            // rb.AddForce(this.transform.forward)
            // this.transform.forward gets a local forward vector of the object
            // rb.AddRelativeForce can work as well
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-this.transform.forward, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddTorque(this.transform.up, ForceMode.Force);
        }
    }

    
}
