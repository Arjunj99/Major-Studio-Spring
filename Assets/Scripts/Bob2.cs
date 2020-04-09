using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob2 : MonoBehaviour {

    public float x;
    public float y;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, x * Mathf.Sin(Time.time * y)));
    }
}
