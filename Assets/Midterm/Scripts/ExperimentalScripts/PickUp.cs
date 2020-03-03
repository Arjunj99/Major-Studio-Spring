using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    // public string pickUpName;
    public PickUpManager.PickUpType objectType;
    public Text itemText; //top right corner text of HUD


    // public 
    // Start is called before the first frame update
    void Start() {
        itemText = "";
    }

    // Update is called once per frame
    void Update() {        
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            switch (this.objectType) {
                case 0:
                    PickUpManager.Fruits++;
                    break;
                default:
                    break;
            }
            itemText = PickUpManager.Fruits + "/ 5"; 
            Debug.Log(PickUpManager.Fruits);
            Destroy(gameObject);
        }
    }
}
