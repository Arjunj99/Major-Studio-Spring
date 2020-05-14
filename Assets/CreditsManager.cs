using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {
    public List<string> creditText = new List<string>();
    public TMP_Text text; 
    public LeanTweenType tweenType;
    public GameObject foreGround;
    public GameObject backGround;
    private int i = -1;
    public GameObject RetryButton;
    public GameObject QuitButton;
    private Vector3 retry;
    private Vector3 quit;
    // public PauseMenu pauseMenu;


    // Start is called before the first frame update
    void Start() {
        retry = RetryButton.transform.localScale;
        quit = QuitButton.transform.localScale;
        text.gameObject.SetActive(false);
        //SetUpCredits();
        // RunCredits();
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    public void RunCredits() {
        i++;
        if (i == creditText.Count) {
            SceneManager.LoadScene("MainMenuScene");
            // RetryButton.transform.localScale = retry;
            // QuitButton.transform.localScale = quit;
            // LeanTween.alpha(QuitButton.GetComponent<RectTransform>(), 1f, 2).setEase(tweenType);
            // LeanTween.alpha(RetryButton.GetComponent<RectTransform>(), 1f, 2).setEase(tweenType);
            return;
        }
        text.text = creditText[i];
        var seq = LeanTween.sequence();
        seq.append(LeanTween.alpha(backGround.gameObject.GetComponent<RectTransform>(), 0f, 2).setEase(tweenType).setDelay(10f));
        seq.append(LeanTween.alpha(backGround.gameObject.GetComponent<RectTransform>(), 1f, 2).setEase(tweenType).setDelay(10f).setOnComplete(RunCredits));


        // var seq = LeanTween.sequence();
        // for (int i = 0; i < creditText.Count; i++) {
        //     seq.append(LeanTween.alpha(text.gameObject, 1, 2).setEase(LeanTweenType.easeInCirc));
        //     seq.append(LeanTween.alpha(text.gameObject, 0, 2).setEase(LeanTweenType.easeInCirc));
        //     // text.text = creditText[i];
        // }
        // seq.
    }

    public void SetUpCredits() {
        // pauseMenu.canPause = false;
        LeanTween.alpha(backGround.gameObject.GetComponent<RectTransform>(), 1f, 5).setEase(tweenType);
        LeanTween.alpha(foreGround.gameObject.GetComponent<RectTransform>(), 1f, 5).setEase(tweenType).setOnComplete(SetUpHelper);
        LeanTween.alpha(QuitButton.GetComponent<RectTransform>(), 0f, 0.1f).setEase(tweenType);
        LeanTween.alpha(RetryButton.GetComponent<RectTransform>(), 0f, 0.1f).setEase(tweenType);
    }

    public void SetUpHelper() {
        text.gameObject.SetActive(true);
        RunCredits();
    }
}
