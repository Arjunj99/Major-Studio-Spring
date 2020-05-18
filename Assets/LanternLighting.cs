using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternLighting : MonoBehaviour {
    public Material off;
    public Material on;
    Renderer rend;
    private float time;
    public bool turnedOn = false;
    public float yAxis = 0;
    public float speed;
    public float amp;
    public float delay;
    // public ParticleSystem fire;
    Vector3 pos;
    public Vector2 randomHeight = new Vector2(10, 20);

    // public Color color;

    // Start is called before the first frame update
    void Start() {
        yAxis = transform.position.y + Random.Range(randomHeight.x, randomHeight.y);
        rend = GetComponent<Renderer>();
        rend.materials[1] = off;
        pos = new Vector3(transform.position.x, yAxis, transform.position.z);
        delay = Random.Range(-Mathf.PI,Mathf.PI);
        // fire.emissionRate = 0;
    }

    // Update is called once per frame
    void Update() {
        if (turnedOn) {
            if (time < 1f) { time += Time.deltaTime/2; }
            if (time > 1f) { time = 1f; }

            rend.materials[1].Lerp(off, on, time);
            gameObject.transform.position = Vector3.Lerp(transform.position, pos + (Vector3.down * Mathf.Sin((Time.time * speed) + delay) * amp), Time.deltaTime / 2);
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            turnedOn = true;
            //fire.emissionRate = 5;
        }
    }
}
