using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHelper : MonoBehaviour {
    public MusicManager.AudioSourceType audioSourceType;
    public MusicManager musicManager;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            musicManager.currentType = audioSourceType;
        }
    }
}
