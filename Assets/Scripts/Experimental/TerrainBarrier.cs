using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBarrier : MonoBehaviour
{
    BoatMovement movement;

    private void Start()
    {
        movement = GetComponent<BoatMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Land")
        {
            Debug.Log("You shall not pass!");
            movement.gravity = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Land")
        {
            Debug.Log("Leave this place!");
            movement.gravity = 3;
        }
    }
}
