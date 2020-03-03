using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class6SoundScript : MonoBehaviour {
    public static Class6SoundScript musicManager;
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Awake() {
        if (musicManager == null) {
            musicManager = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        audioSource.PlayOneShot(audioClip);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
