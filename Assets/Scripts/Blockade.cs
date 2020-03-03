using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour {
    public int FruitLimit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (PickUpManager.getFruits() == FruitLimit) {
            Destroy(gameObject);
        }
    }
}
