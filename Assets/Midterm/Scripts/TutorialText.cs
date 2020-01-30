using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour {
    public TextMeshProUGUI text;
    private string[] textChoice = new string[7];
    private KeyCode[] keyCodeChoice = new KeyCode[7];
    private IslandSpawner islandSpawner; 
    private int i = 0;

    // Start is called before the first frame update
    void Start() {
        islandSpawner = GameObject.Find("IslandSpawner").GetComponent<IslandSpawner>();

        textChoice[0] = "Welcome to the Mind Sea";
        textChoice[1] = "Sail forward with W";
        textChoice[2] = "Steer using A and D";
        textChoice[3] = "Hoist the Anchor with E";
        textChoice[4] = "Scan the Horizon with Shift";
        textChoice[5] = "Light all the Island Beacons";
        textChoice[6] = "";

        keyCodeChoice[0] = KeyCode.Space;
        keyCodeChoice[1] = KeyCode.W;
        keyCodeChoice[2] = KeyCode.D;
        keyCodeChoice[3] = KeyCode.E;
        keyCodeChoice[4] = KeyCode.LeftShift;
        keyCodeChoice[5] = KeyCode.Space;
        keyCodeChoice[6] = KeyCode.Space;
    }

    // Update is called once per frame
    void Update() {
        text.text = textChoice[i];

        if (Input.GetKeyDown(keyCodeChoice[i])) { // Cycles throw tutorial when correct keys are pressed.
            if (i < 6) {
                i++;
            }
        }

        if (islandSpawner.gameWon) {
            text.text = "Space to Restart";
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene("PiratesAdventure");
        }
            
    }
}
