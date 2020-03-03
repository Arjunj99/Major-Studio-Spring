using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonScript : MonoBehaviour
{
    //public Transform reticle;

    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ray mouseRay = Camera.main.ScreenPointToRay(reticle.position);

        //float mouseRayDist = 5.0f; // raycast distance (field of view)

        //RaycastHit hit = new RaycastHit();

        //Debug.DrawRay(mouseRay.origin, mouseRay.direction * mouseRayDist, Color.yellow); //visualize raycast

        transform.LookAt(cam);
    }
}
