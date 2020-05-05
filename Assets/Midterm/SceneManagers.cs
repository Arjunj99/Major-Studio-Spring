using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers:MonoBehaviour {

    // Start is called before the first frame update
    void Start()
    {
        // SceneManager.LoadScene("ArjunScene");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            LoadScene();
        }
    }

    public void LoadScene() {
        Debug.Log("MOVE");
        SceneManager.LoadScene("ArjunScene");
    }

    public void Quit() {
        Application.Quit();
    }
}
