using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float horizontalLookSpeed = 10f;
    public float verticalLookSpeed = 5f;

    float verticalAngle;

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.parent.Rotate(0f, mouseX * horizontalLookSpeed * Time.deltaTime, 0f);
        transform.parent.Find("Barrel").Rotate(0f, 0f, mouseY * -verticalLookSpeed/2 * Time.deltaTime);

        verticalAngle -= mouseY * verticalLookSpeed * Time.deltaTime;
        verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);

        transform.localEulerAngles = new Vector3(verticalAngle, transform.localEulerAngles.y, 0f);
    }
}
