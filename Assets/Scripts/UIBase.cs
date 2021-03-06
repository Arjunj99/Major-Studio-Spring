﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [Header ("General Settings")]
    public float delay = 0.5f;
    public float speed = 0.5f;
    private bool beenClicked = false;

    [Header ("On Hover Settings")]
    public Vector3 deltaScale = Vector3.zero;
    private Vector3 onHoverScale = Vector3.one;
    public LeanTweenType onHover = LeanTweenType.easeInElastic;

    [Header ("Off Hover Settings")]
    private Vector3 offHoverScale = Vector3.one;
    public LeanTweenType offHover = LeanTweenType.easeOutElastic;

    [Header ("On Click Settings")]
    public Vector3 onClickScale = Vector3.zero;
    public Vector3 deltaPosition = Vector3.zero;
    private Vector3 onClickPosition = Vector3.zero;
    public LeanTweenType onClick = LeanTweenType.easeOutElastic;
    public LeanTweenType onClickMove = LeanTweenType.easeOutElastic;

    public GameObject menuTransitions;

    // Start is called before the first frame update
    void Start() {
        offHoverScale = transform.localScale;
        onHoverScale = offHoverScale + deltaScale;
        onClickPosition = transform.position + deltaPosition;
        menuTransitions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData data) {
        if (!beenClicked)
            LeanTween.scale(gameObject, onHoverScale, speed).setDelay(delay).setEase(onHover);
    }

    public void OnPointerExit(PointerEventData data) {
        if (!beenClicked)
            LeanTween.scale(gameObject, offHoverScale, speed).setDelay(delay).setEase(offHover);
    }

    public void OnClick(string scene) {
        // LeanTween.scale(gameObject, onClickScale, speed).setDelay(delay).setEase(onClick);
        menuTransitions.SetActive(true);
        menuTransitions.GetComponent<MenuStart>().buffer = true;
        menuTransitions.GetComponent<MenuStart>().quit = false;
        // GameObject end = Instantiate(menuTransitions, Vector3.zero, Quaternion.identity);
        // end.GetComponent<MenuStart>().quit = false;
        // SceneManager.LoadScene(scene);
        beenClicked = true;
    }

    public void OnClick() {
        // LeanTween.scale(gameObject, onClickScale, speed).setDelay(delay).setEase(onClick);
        menuTransitions.SetActive(true);
        menuTransitions.GetComponent<MenuStart>().buffer = true;
        menuTransitions.GetComponent<MenuStart>().quit = true;
        // Application.Quit();
        beenClicked = true;
    }
}
