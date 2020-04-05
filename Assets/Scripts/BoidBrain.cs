using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBrain : MonoBehaviour {
    public GameObject boidPrefab; // BoidPrefab to Instantiate
    public List<BoidBehavior> boidList = new List<BoidBehavior>(); // list of all BoidBehaviors
    public int numOfBoids; // Number of Boids that follow the boid brain
    public float boidSeperationDifference = 1; // Boid Seperation Difference
    public float alignmentValue; // Ratios for Boid
    public float cohesionValue;
    public float seperationValue;
    public float targetSeekValue;
    public float alignmentRange; // Range for Ratios for Boid
    public float cohesionRange;
    public float seperationRange;
    public float targetSeekRange;
    Vector3 startTransform; // Start transform for Brain
    public Vector3 deltaTransform; // Delta Transform for Brain
    public int delay; // When the boidBrain should choose a new movePoint
    Vector3 newTransform; // new MovePoint
    public int delayTime; // Current Frame
    public float inter = 0.2f;

    
    // Start is called before the first frame update
    void Start() {
        startTransform = gameObject.transform.position; // Sets currentPosition
        for (int i = 0; i < numOfBoids; i++) { // Creates Boids
            CreateBoid();
        }   
    }

    // Update is called once per frame
    void Update() {
        if (delay == delayTime) { // Moves BoidBrain (moves swarm)
            newTransform = startTransform + new Vector3(Random.Range(-deltaTransform.x, deltaTransform.x), Random.Range(-deltaTransform.y, deltaTransform.y), Random.Range(-deltaTransform.z, deltaTransform.z));
            delay = 0;
        } else {
            delay++;
        }

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newTransform, inter); // Lerps to new Position


        for (int i = 0; i < boidList.Count; i++) { // Changes the Ratios
            boidList[i].alignmentVal = Random.Range(alignmentValue, alignmentValue + alignmentRange);
            boidList[i].cohesionVal = Random.Range(cohesionValue, cohesionValue + cohesionRange);
            boidList[i].seperationVal = Random.Range(seperationValue, seperationValue + seperationRange);
            boidList[i].targetSeekVal = Random.Range(targetSeekValue, targetSeekValue + targetSeekRange);
        }
    }

    void CreateBoid() { // Creates a new Boid
        GameObject newBoid = Instantiate(boidPrefab, this.transform.position, Quaternion.identity);
        newBoid.transform.parent = this.transform;

        BoidBehavior newBoidBehavior = newBoid.GetComponent<BoidBehavior>();
        newBoidBehavior.boidBrain = this;
        newBoidBehavior.speed = Random.Range(1,5);

        boidList.Add(newBoidBehavior);
    }
}
