using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this should go on the Boat/Player game object
//if not a line taking in the player game obejct needs to be added. 
public class DialogueManager : MonoBehaviour
{
    //public GameObject fruitItems;
    //public GameObject festivalVillager;
    //public GameObject questVillager;
    //private PickUpManager pickUpManager; 
    //public Text questText; //this is for the quest giving NPC
    //public Text villagerText; //eventually these will cycle through random lines
    //public Panel dialogueBubble; 

    //public bool playerHasTalked;
    //public bool playerHasItem; //this taken through pick up manager instead maybe? 
    //// public Image fruitImage; DON'T NEED THESE ANYMORE? 

    //void Start()
    //{
    //    pickUpManager = fruitItems.GetComponent<PickUpManager>();
    //    questText = ""; 
    //    villagerText = "";
    //    dialogueBubble = Panel.setActive(false); //this is where the text will appear
    //    playerHasTalked = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(this.GameObject.transform.position.x <= questVillager.transform.position.x)
    //    {
    //        dialogueBubble = true; 
    //        //this is taking a lot of if statements, there's a better way to do this but idk what that is? 
    //        if(playerHasTalked = false)
    //        {
    //            questText = "Welcome traveller! Will you join us at the festival?"; //this can be changed. i need to look at something like ink or fungus. 
    //            playerHasTalked; 
    //        }else{
    //            questText = "Enjoy the festival!"; //changed to something else later
    //        }
    //    }else{
    //        dialogueBubble = false;
    //        questText = "";
    //    }

    //    if(this.GameObject.transform.position.x <= festivalVillager.transform.position.x)
    //    {
    //        dialogueBubble = true;
    //        villagerText = "Hi!"; 
    //    }else{
    //        dialogueBubble = false;
    //        villagerText = "";
    //    }
    //}

    ////okay so here's the decision i need to figure out: 
    ////do we want it based on triggers or position? 
    ////are we going to have so many lines from NPCs that it makes more sense to use a parser? 
    ////which parser works best for quest-style dialogue? 

    //void OnTriggerEnter(Collider collider) //want it to be when player is in a certain range, getting there. 
    //{
    //    if(collider.gameObject.tag == "Player")
    //    {
    //        QuestRun();
    //    }
    //}

    //void QuestRun()
    //{
    //    if(gameObject.tag = "Main Swamp NPC") //better way to call these ?? help? 
    //    {
    //        dialogueBubble = Panel.setActive(true); //displays the dialogue box
            
    //        //the questText should display certain things IF player has item, IF playerHasTalked = true
    //        //and IF hasNoItem
    //        //just the one to get main NPC talking, the others will need something else, still working it out, 
    //        //might be simplistic lines through Ink or serialized fields.
            
    //        questText = fruitImage + " x " + pickUpManager.fruitItems; 
            
             
    //    }
        
    //}
}
