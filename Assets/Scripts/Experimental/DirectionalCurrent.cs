using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalCurrent : MonoBehaviour {
    public float currentSpeed = 2f;
    public GameObject player;
    public BoxCollider currentCollider;
    public bool inZone = false;


    // Start is called before the first frame update
    void Start() {
        currentCollider = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {
        if (inZone) {
            player.GetComponent<BoatMovement>().currentForce = this.transform.forward * currentSpeed;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            inZone = true;
            player.GetComponent<BoatMovement>().inCurrent = true;
        }        
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            inZone = false;
            player.GetComponent<BoatMovement>().inCurrent = false;
        } 
    }
}
