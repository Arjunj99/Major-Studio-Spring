using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHelper : MonoBehaviour
{
    public SFXManager manager;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.source.Stop();
            manager.source.clip = clip;
            manager.source.Play();
        }
    }
}
