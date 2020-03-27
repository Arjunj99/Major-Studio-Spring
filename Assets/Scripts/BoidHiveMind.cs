using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidHiveMind : MonoBehaviour {
    public Vector2 numberRange = new Vector2(20f,40f);
    public Vector2 boidSeperationRange = new Vector2(10,30);
    public Vector2 alignmentRange = new Vector2(0.1f,0.3f);
    public Vector2 coheseionRange = new Vector2(0.2f,0.4f);
    public Vector2 seperationRange = new Vector2(0.05f,0.25f);
    public Vector2 targetSeekRange = new Vector2(0.2f,0.4f);
    public Vector2 delayRange = new Vector2(15f,30f);

    public List<BoidBrain> boidBrain = new List<BoidBrain>();

    void Awake() {
        for (int i = 0; i < boidBrain.Count; i++) {
            boidBrain[i].numOfBoids = (int) Random.Range(numberRange.x, numberRange.y);
            boidBrain[i].boidSeperationDifference = Random.Range(boidSeperationRange.x, boidSeperationRange.y);
            boidBrain[i].alignmentValue = Random.Range(alignmentRange.x, alignmentRange.y);
            boidBrain[i].cohesionValue = Random.Range(coheseionRange.x, coheseionRange.y);
            boidBrain[i].seperationValue = Random.Range(seperationRange.x, seperationRange.y);
            boidBrain[i].targetSeekValue = Random.Range(targetSeekRange.x, targetSeekRange.y);
            boidBrain[i].delay = (int) Random.Range(delayRange.x, delayRange.y);
            boidBrain[i].delayTime = boidBrain[i].delay;
        }
    }
}
