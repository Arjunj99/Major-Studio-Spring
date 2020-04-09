using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour {
    public BoatMovement boatMovement;
    public float multi = 0.5f;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        this.gameObject.transform.Rotate(new Vector3(0f, 0f, this.boatMovement.boat.GetCurrentSpeed() + boatMovement.currentForce.magnitude * multi));
    }
}
