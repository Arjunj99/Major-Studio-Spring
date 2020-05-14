using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour {
    public float opacity = 1;
    public Image image;
    private Color color;
    public float dip = 0.01f; 
    public bool whichOne = false;
    public bool quit = false;
    public bool buffer = false;

    // Start is called before the first frame update
    void Start() {
        color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }

    // Update is called once per frame
    void Update() {
        if (!whichOne) {
            if (opacity <= 0) {
                Destroy(gameObject);
            }
            image.color = color;
            opacity = Mathf.Lerp(opacity, -0.1f, Time.deltaTime);
            color = new Color(image.color.r, image.color.g, image.color.b, opacity);
        } else {
            if (buffer) {
                 if (opacity >= 1) {
                    if (quit) {
                        Application.Quit();
                    } else {
                        SceneManager.LoadScene("ArjunScene");
                    }
                }
                image.color = color;
                opacity = Mathf.Lerp(opacity, 1.1f, Time.deltaTime);
                color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            }
           
        }
    }
}
