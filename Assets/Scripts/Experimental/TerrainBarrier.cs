using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Land")
        {
            Debug.Log("You shall not pass!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Land")
        {
            Debug.Log("Leave this place!");
        }
    }
}
