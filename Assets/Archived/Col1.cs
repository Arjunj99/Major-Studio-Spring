﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col1 : MonoBehaviour
{
    public BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void onTriggerEnter(Collider other) {
        BroadcastMessage("BeginNextStage");
        Debug.Log("Next Stage");
    }
}
