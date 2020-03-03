using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullAI : MonoBehaviour {
    public float random;


    // Start is called before the first frame update
    void Start() {
        random = Random.Range(-5f,5f);
        
    }

    // Update is called once per frame
    void Update() {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(random, random, random), ForceMode.Impulse);
        
    }
}
