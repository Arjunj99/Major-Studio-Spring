using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsManager : MonoBehaviour {
    public List<string> creditText = new List<string>();
    public TMP_Text text; 
    public LeanTweenType tweenType;
    public GameObject foreGround;
    public GameObject backGround;
    private int i = -1;


    // Start is called before the first frame update
    void Start() {
        text.gameObject.SetActive(false);
        // SetUpCredits();
        // RunCredits();
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    public void RunCredits() {
        i++;
        if (i == creditText.Count) {
            return;
        }
        text.text = creditText[i];
        var seq = LeanTween.sequence();
        seq.append(LeanTween.alpha(backGround.gameObject.GetComponent<RectTransform>(), 0f, 2).setEase(tweenType));
        seq.append(LeanTween.alpha(backGround.gameObject.GetComponent<RectTransform>(), 1f, 2).setEase(tweenType).setDelay(6f).setOnComplete(RunCredits));


        // var seq = LeanTween.sequence();
        // for (int i = 0; i < creditText.Count; i++) {
        //     seq.append(LeanTween.alpha(text.gameObject, 1, 2).setEase(LeanTweenType.easeInCirc));
        //     seq.append(LeanTween.alpha(text.gameObject, 0, 2).setEase(LeanTweenType.easeInCirc));
        //     // text.text = creditText[i];
        // }
        // seq.
    }

    public void SetUpCredits() {
        LeanTween.alpha(backGround.gameObject.GetComponent<RectTransform>(), 1f, 5).setEase(tweenType);
        LeanTween.alpha(foreGround.gameObject.GetComponent<RectTransform>(), 1f, 5).setEase(tweenType).setOnComplete(SetUpHelper);
    }

    public void SetUpHelper() {
        text.gameObject.SetActive(true);
        RunCredits();
    }
}
