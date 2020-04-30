using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternManager : MonoBehaviour {
    [HideInInspector] public int totalLanterns;
    public List<Lantern> lanternList = new List<Lantern>();
    public TextMesh lanternText; 
    private bool hasLowered = false;
    public GameObject bridge;
    public Vector3 endPos;

    // Start is called before the first frame update
    void Start() {
        totalLanterns = lanternList.Count;
    }

    // Update is called once per frame
    void Update() {
        lanternText.text = totalLanterns.ToString();

        if (totalLanterns == 0 && hasLowered) {
            hasLowered = true;
            lanternText.text = "";
            StartCoroutine(lowerBridgeAnimation());
        }
    }

    public IEnumerator lowerBridgeAnimation() {
        while (totalLanterns == 0) {
            bridge.transform.position = Vector3.Lerp(bridge.transform.position, endPos, Time.deltaTime);
            yield return null;
        }
    }
}
