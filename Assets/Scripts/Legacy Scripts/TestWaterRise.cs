using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaterRise : MonoBehaviour {
    // Start is called before the first frame update
    public Vector3 high;
    public Vector3 low;
    private float Timer = 0;
    private float TimeLimit = 7f;
    private bool isHigh = true;

    void Start() {
        high = this.transform.position + new Vector3(0f,0.3f,0f);
        low = this.transform.position - new Vector3(0f,0.8f,0f);
        
    }

    // Update is called once per frame
    void Update() {
        if (Timer < TimeLimit) { // Rises and Lowers water periodically
            if (isHigh == true)
                this.transform.position = Vector3.Lerp(this.transform.position, high, 0.2f * Time.deltaTime);
            else
                this.transform.position = Vector3.Lerp(this.transform.position, low, 0.2f * Time.deltaTime);
            Timer += Time.deltaTime;
        } else {
            Timer = 0;
            if (isHigh)
                isHigh = false;
            else 
                isHigh = true;
        }
        
    }
}
