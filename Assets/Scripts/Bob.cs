using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour {
    public float bobAngle;
    public float bobSpeed;
    public BoatMovement boatMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localEulerAngles = new Vector3(0, 0, bobAngle  * Mathf.Sin(Time.time / bobSpeed));
    }
}
