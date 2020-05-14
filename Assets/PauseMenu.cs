using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseBar;
    public GameObject resumeButton;
    public GameObject retryButton;
    public GameObject quitButton;
    public GameObject greyScreen;
    public GameObject resumeButtonText;
    public GameObject retryButtonText;
    public GameObject quitButtonText;
    // public LeanTweenType easeIn = LeanTweenType.easeInBounce;
    // public LeanTweenType easeOut = LeanTweenType.easeInBounce;
    public bool pause = false;
    public bool canPause = true;
    // private Vector3 retry;
    // private Vector3 quit;
    // public float speed = 1f;



    // Start is called before the first frame update
    void Start() {
        pauseBar.GetComponent<Image>().color = new Color(1,1,1,0);
        resumeButton.GetComponent<Image>().color = new Color(1,1,1,0);
        retryButton.GetComponent<Image>().color = new Color(1,1,1,0);
        quitButton.GetComponent<Image>().color = new Color(1,1,1,0);
        greyScreen.GetComponent<Image>().color = new Color(0,0,0,0);
        resumeButtonText.GetComponent<TMP_Text>().color = new Color(0,0,0,0);
        retryButtonText.GetComponent<TMP_Text>().color = new Color(0,0,0,0);
        quitButtonText.GetComponent<TMP_Text>().color = new Color(0,0,0,0);


        pauseBar.SetActive(false);
        resumeButton.SetActive(false);
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        greyScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause) {
            StopAllCoroutines();
            PauseScreen();
            pause = !pause;
        }
    }

    void PauseScreen() {
        StartCoroutine(Interact(pauseBar, pause));
        StartCoroutine(Interact(resumeButton, pause));
        StartCoroutine(Interact(retryButton, pause));
        StartCoroutine(Interact(quitButton, pause));
        StartCoroutine(Interact(greyScreen, pause, new Color(0,0,0,0.6f)));
        StartCoroutine(Interact(resumeButtonText.GetComponent<TMP_Text>(), pause));
        StartCoroutine(Interact(retryButtonText.GetComponent<TMP_Text>(), pause));
        StartCoroutine(Interact(quitButtonText.GetComponent<TMP_Text>(), pause));



        // if (!pause) {
        //     LeanTween.scale(retryButton, retry, speed).setEase(easeIn);
        //     LeanTween.scale(quitButton, quit, speed).setDelay(speed/2).setEase(easeIn);
        // } else {
        //     LeanTween.scale(retryButton, Vector3.zero, speed).setEase(easeOut);
        //     LeanTween.scale(quitButton, Vector3.zero, speed).setDelay(speed/2).setEase(easeOut);
        // }
    }

    public IEnumerator Interact(GameObject image, bool on) {
        if (!on) {
            image.SetActive(true);
            for (int i = 0; i < 80; i++) {
                image.GetComponent<Image>().color = Color.Lerp(image.GetComponent<Image>().color, Color.white, Time.deltaTime);
                yield return null;
            }
        } else {
            for (int i = 0; i < 80; i++) {
                image.GetComponent<Image>().color = Color.Lerp(image.GetComponent<Image>().color, new Color(1,1,1,0), Time.deltaTime);
                yield return null;
            }
            image.SetActive(false);
        }
    }

    public IEnumerator Interact(TMP_Text image, bool on) {
        if (!on) {
            // image.SetActive(true);
            for (int i = 0; i < 80; i++) {
                image.color = Color.Lerp(image.color, Color.black, Time.deltaTime);
                yield return null;
            }
        } else {
            for (int i = 0; i < 80; i++) {
                image.color = Color.Lerp(image.color, new Color(0,0,0,0), Time.deltaTime);
                yield return null;
            }
            // image.SetActive(false);
        }
    }

    public IEnumerator Interact(GameObject image, bool on, Color color) {
        if (!on) {
            image.SetActive(true);
            for (int i = 0; i < 80; i++) {
                image.GetComponent<Image>().color = Color.Lerp(image.GetComponent<Image>().color, color, Time.deltaTime);
                yield return null;
            }
        } else {
            for (int i = 0; i < 80; i++) {
                image.GetComponent<Image>().color = Color.Lerp(image.GetComponent<Image>().color, new Color(0,0,0,0), Time.deltaTime);
                yield return null;
            }
            image.SetActive(false);
        }
    }

    public void Resume() {
        StopAllCoroutines();
        PauseScreen();
        pause = !pause;
    }
}
