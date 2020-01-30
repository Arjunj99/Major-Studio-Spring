using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAI : MonoBehaviour {
    public Boat boat;
    private float maxDist = 70f;
    private float maxSpeed = 8f;
    private bool dir;

    void Awake() { // Initializes a Boat Ai with a boat Object and a random Direction Value for the rest of the Game
        boat = new Boat();
        int dirint = Random.Range(0,1);
        if (dirint == 0)
            dir = false;
        else
            dir = true;
    }


    // Start is called before the first frame update
    void Start() { // Boat is always inMotion
        boat.SetInMotion(true);
    }

    // Update is called once per frame
    void Update() { // Checks if something is nearby with Spherecast. If so, attempts a turn.
        Ray navRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.cyan, 10f);

        this.transform.Translate(new Vector3(0, 0, boat.GetCurrentSpeed() * Time.deltaTime));
        boat.SetCurrentRotation(boat.GetCurrentSpeed() * -2f);

        if (Physics.SphereCast(navRay, 4f, maxDist)) {
            if (dir)
                this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * Time.deltaTime, 0));
            else
                this.transform.Rotate(new Vector3(0, boat.GetCurrentRotation() * -Time.deltaTime, 0));
        }

        if (boat.GetCurrentSpeed() < maxSpeed) // Boat always increases speed unless its at max
            boat.SetCurrentSpeed(boat.GetCurrentSpeed() + 0.2f);
    }
}
