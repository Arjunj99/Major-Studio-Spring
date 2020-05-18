using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanternManager : MonoBehaviour {
    public int totalLanterns;
    public List<Lantern> lanternList = new List<Lantern>();
    public TMP_Text lanternTextPro;
    public TextMesh lanternTextMesh; 
    private bool hasLowered = false;
    public GameObject bridge;
    public Vector3 endPos;
    int toPrint;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start() {
        totalLanterns = lanternList.Count - (int) (lanternList.Count * 0.25f);
        endPos = bridge.transform.position - endPos;
    }

    // Update is called once per frame
    void Update() {
        toPrint = totalLanterns;
        if (toPrint <= 0) { 
            lanternTextMesh.text = " ";
            lanternTextPro.text = "0";
        } else {
            lanternTextMesh.text = totalLanterns.ToString();
            lanternTextPro.text = totalLanterns.ToString();
        }

        if (totalLanterns <= 0 && !hasLowered) {
            hasLowered = true;
            audioSource.PlayOneShot(audioClip);
            StartCoroutine(lowerBridgeAnimation());
        }
    }

    public IEnumerator lowerBridgeAnimation() {
        while (totalLanterns <= 0) {
            bridge.transform.position = Vector3.Lerp(bridge.transform.position, endPos, Time.deltaTime);
            yield return null;
        }
    }
}
