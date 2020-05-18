using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob2 : MonoBehaviour {

    public float x;
    public float y;
    public Vector3 rotation;
    public float delay;
    public bool isShip = false;


    // Start is called before the first frame update
    void Start()
    {   
        if (!isShip) { delay = Random.Range(0,2); }
        rotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, rotation.z + (x * Mathf.Sin((delay + Time.time) * y))));
        
        // transform.position = PointOnCircleWorldSpace(Mathf.Sin(Time.time), 0.1f);
        // transform.Rotate(new Vector3(0, 0, x * Mathf.Sin(Time.time * y)));
    }

    Vector3 PointOnCircle(float theta) {
        return new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
    }

    Vector3 PointOnCircleWorldSpace(float theta, float radius) {
        return (PointOnCircle(theta) * radius) + transform.position;
    }
}
