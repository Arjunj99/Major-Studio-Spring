using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject retryButton;
    public GameObject quitButton;
    public LeanTweenType easeIn = LeanTweenType.easeInBounce;
    public LeanTweenType easeOut = LeanTweenType.easeInBounce;
    public bool pause = false;
    private Vector3 retry;
    private Vector3 quit;
    public float speed = 1f;



    // Start is called before the first frame update
    void Start() {
        retry = retryButton.transform.localScale;
        quit = quitButton.transform.localScale;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseScreen();
        }
    }

    void PauseScreen() {
        if (!pause) {
            LeanTween.scale(retryButton, retry, speed).setEase(easeIn);
            LeanTween.scale(quitButton, quit, speed).setDelay(0.5f).setEase(easeIn);
        } else {
            LeanTween.scale(retryButton, Vector3.zero, speed).setEase(easeOut);
            LeanTween.scale(quitButton, Vector3.zero, speed).setDelay(0.5f).setEase(easeOut);
        }
    }
}
