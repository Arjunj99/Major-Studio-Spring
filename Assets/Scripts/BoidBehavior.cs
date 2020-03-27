using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour {
    public BoidBrain boidBrain; // Each Boid answers to a BoidBrain. Each BoidBrain answers to a BoidHiveMind.
    Vector3 velocity; // Current Velocity
    public float speed; // Speed factor
    public float alignmentVal; // Alignment Ratio
    public float cohesionVal; // Cohesion Ratio
    public float seperationVal; // Seperation Ratio
    public float targetSeekVal; // Seek Ratio

    // Start is called before the first frame update
    void Start() {
        velocity = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), Random.Range(-1f,1f)); // Assign random 3 dimensional velocity
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPosition = this.transform.position;
        
        Vector3 cohesion = boidBrain.transform.position; // Vector3 for keeping the Boids Together
        Vector3 alignment = Vector3.zero; // Vector3 for keeping the boids facing the same direction
        Vector3 seperation = Vector3.zero; // Vector3 for keeping them close but not close together

        Vector3 targetSeek = boidBrain.transform.position - this.transform.position; // Keeps the void around the BoidBrain

        foreach (BoidBehavior boidFriend in boidBrain.boidList) { // Adjusts Boid Ratios
            if (boidFriend == this) continue;

            cohesion += boidFriend.transform.position;
            alignment += boidFriend.velocity;

            Vector3 directionalDifference = this.transform.position - boidFriend.transform.position;
            if (directionalDifference.magnitude > 0 && directionalDifference.magnitude < boidBrain.boidSeperationDifference) {
                seperation += boidBrain.boidSeperationDifference * (directionalDifference.normalized / directionalDifference.magnitude);
            }
        }


        // Assigns average ratios
        alignment /= (boidBrain.numOfBoids - 1);
        cohesion /= (boidBrain.numOfBoids - 1);
        cohesion = (cohesion - this.transform.position).normalized;
        seperation /= (boidBrain.numOfBoids - 1);

        // Adjusts velocity
        Vector3 newVelocity = Vector3.zero;
        newVelocity += alignment * alignmentVal;
        newVelocity += cohesion * cohesionVal;
        newVelocity += seperation * seperationVal;
        newVelocity += targetSeek * targetSeekVal;
        newVelocity.Normalize();

        velocity = MathUtility.Limit((velocity + newVelocity), 1f);

        // Make the boid point where its going
        this.transform.up = velocity.normalized;

        this.transform.position = currentPosition + velocity * (speed * Time.deltaTime);
    }
}

