using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartProject3 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private bool notPressed = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && notPressed) {
            notPressed = false;
            audioSource.PlayOneShot(audioClip);

            StartCoroutine(SceneChange());
        }
    }


    IEnumerator SceneChange() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MachineScene");
    }
}

// Make
//Scrolls
//Spiral
//flag
//
