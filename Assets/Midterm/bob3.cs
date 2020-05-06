using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bob3 : MonoBehaviour {
    public Vector3 pos;
    public float speed;
    public float amp;
    public float delay;

    // Start is called before the first frame update
    void Start() {
        pos = transform.position;
        delay = Random.Range(-Mathf.PI,Mathf.PI);
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.Lerp(transform.position, pos + (Vector3.down * Mathf.Sin((Time.time * speed) + delay) * amp), Time.deltaTime / 2);
    }
}
