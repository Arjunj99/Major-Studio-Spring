using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // public string pickUpName;
    public PickUpManager.PickUpType objectType;



    // public 
    // Start is called before the first frame update
    void Start() {
        
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
            Debug.Log(PickUpManager.Fruits);
            Destroy(gameObject);
        }
    }
}
