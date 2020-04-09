using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClean : MonoBehaviour {
    public Transform cameraTransform;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start() {
        cameraTransform = transform;
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        // if (Physics.Raycast(transform.position, playerTransform.position - cameraTransform.position, out hit, (playerTransform.position - cameraTransform.position).magnitude)) {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //     hit.collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        //     Debug.Log("Did Hit");
        // }
    }
}
