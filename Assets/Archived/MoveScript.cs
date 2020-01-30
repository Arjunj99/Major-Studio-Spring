using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public int moveSpeed = 0;
    public int rotationSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) {
            this.transform.Translate(new Vector3(0,moveSpeed*Time.deltaTime,0));
        }
        if(Input.GetKey(KeyCode.S)) {
            this.transform.Translate(new Vector3(0,-moveSpeed*Time.deltaTime,0));
        }
        if(Input.GetKey(KeyCode.Q)) {
            this.transform.Translate(new Vector3(0,0,moveSpeed*Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.E)) {
            this.transform.Translate(new Vector3(0,0,-moveSpeed*Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.A)) {
            this.transform.Rotate(new Vector3(0,0,rotationSpeed*Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.D)) {
            this.transform.Rotate(new Vector3(0,0,-rotationSpeed*Time.deltaTime));
        }
    }
}
