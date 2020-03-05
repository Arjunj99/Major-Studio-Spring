// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class DialogueManager : MonoBehaviour
// {
//     public GameObject fruitItems;
//     private PickUpManager pickUpManager; 
//     public Text npcText; 
//     public Panel dialogueBubble; 
//     public Image fruitImage; //add for each type of item to pick up  

//     void Start()
//     {
//         pickUpManager = fruitItems.GetComponent<PickUpManager>();
//         npcText = "";
//         dialogueBubble = Panel.setActive(false);
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     void OnTriggerEnter(Collider collider)
//     {
//         if(collider.gameObject.tag == "Player")
//         {
//             QuestRun();
//         }
//     }

//     void QuestRun()
//     {
//         if(gameObject.tag = "Main Swamp NPC") //need to tag the NPCs!
//         {
//             dialogueBubble = Panel.setActive(true);
//             npcText = fruitImage + " x " + pickUpManager.fruitItems; 
//         }
        
//     }
// }
