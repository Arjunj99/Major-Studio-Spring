using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {
    public GameObject top;
    public GameObject bottom;
    public BoxCollider boxCollider;
    public Vector3 displacement = new Vector3(0,3.5f,0);
    public bool discovered = false;
    public bool bob = false;
    public float amplitude, speed;
    public ParticleSystem ps;
    public bool firstTime = false;
    public Light pointLight;
    public float maxBrightness = 60;
    public AudioSource audioSource;
    public AudioClip burnClip;
    public LanternManager lanternManager;


    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(raiseTop());
    }

    // Update is called once per frame
    void Update() {
        if (bob) {
            top.transform.localPosition = new Vector3(0f, Mathf.Sin(Time.time * speed) * amplitude, 0f) + displacement;
        }
        
    }

    public IEnumerator raiseTop() {
        audioSource.PlayOneShot(burnClip);
        lanternManager.totalLanterns--;
        for (int i = 0;  i < 30; i++) {
            top.transform.localPosition = Vector3.Lerp(top.transform.localPosition, bottom.transform.localPosition + displacement, Time.deltaTime);
            ps.emissionRate = Mathf.Lerp(ps.emissionRate, 30f, Time.deltaTime);
            pointLight.intensity = Mathf.Lerp(pointLight.intensity, maxBrightness, Time.deltaTime * 3f);
            // top.transform.position += top.transform.up * 0.05f;
            // top.transform.Rotate(Vector3.up * 1f);
            yield return 0;
        }
        bob = true;
        displacement = top.transform.localPosition;
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            if (!firstTime) {
                StartCoroutine(raiseTop());
                firstTime = true;
            }
            Debug.Log("COLLISION DETECTED");
        }
    }
}
