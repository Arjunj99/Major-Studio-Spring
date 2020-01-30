using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPa : MonoBehaviour
{
    public Vector3 CameraPostition;
    public Quaternion CameraRotation;
    private bool start;
    // bool hasStart = false;
    // Start is called before the first frame update
    void Start()
    {
        CameraPostition = new Vector3(3.028141f, -4.146518f, -5.198347f);
        CameraRotation = Quaternion.Euler(14.474f, -60.202f, 0.005f);
        // Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);


        // CameraRotation = new Vector3(this.transform.rotation.x, this.transform.position.y, this.transform.position.z);
        // StartCoroutine(cameraPan());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, CameraPostition.x, 0.8f * Time.deltaTime), Mathf.Lerp(this.transform.position.y, CameraPostition.y, 0.8f * Time.deltaTime), Mathf.Lerp(this.transform.position.z, CameraPostition.z, 0.8f * Time.deltaTime));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, CameraRotation, 0.8f * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && start == false) {
                    StartCoroutine(cameraPan());
                    start = true;
        }
        
        
        
        
        // Rotate(new Vector3(Mathf.Lerp(this.transform.rotation.x, CameraRotation.x, 0.8f * Time.deltaTime), Mathf.Lerp(this.transform.rotation.y, CameraRotation.y, 0.8f * Time.deltaTime), Mathf.Lerp(this.transform.rotation.z, CameraRotation.z, 0.8f * Time.deltaTime)), Space.World);
            
            
            
            // Mathf.Lerp(this.transform.rotation.x, CameraRotation.x, 0.2f), Mathf.Lerp(this.transform.rotation.y, CameraRotation.y, 0.2f), Mathf.Lerp(this.transform.rotation.z, CameraRotation.z, 0.2f), Mathf.Lerp(this.transform.rotation.w, CameraRotation.w, 0.2f));
    }

    IEnumerator cameraPan() {
        yield return new WaitForSeconds(2.3f);
        // CameraPostition = new Vector3(10.92905f, -0.7161083f, 1.388544f);
        // CameraRotation = Quaternion.Euler(32.178f, -101.281f, 0.007f);
        // yield return new WaitForSeconds(2f);
        CameraPostition = new Vector3(12.7344f, -1.429325f, -0.4688387f);
        CameraRotation = Quaternion.Euler(20.318f, -37.169f, 0.006f);
        yield return new WaitForSeconds(2.5f);
        CameraPostition = new Vector3(-1.449368f, -0.3674088f, 24.04811f);
        CameraRotation = Quaternion.Euler(23.928f, 146.475f, 0.008f);

        yield return new WaitForSeconds(5f);
        CameraPostition = new Vector3(7.531769f, -6.414612f, -10.09956f);
        CameraRotation = Quaternion.Euler(25.131f, -43.975f, 0.009f);
        yield return new WaitForSeconds(10f);
        CameraPostition = new Vector3(12.2393f, -12.09284f, -17.3308f);
        CameraRotation = Quaternion.Euler(22.896f, -51.538f, 0.012f);
        yield return new WaitForSeconds(15f);
        CameraPostition = new Vector3(-6.62301f, -23.67169f, -3.682536f);
        CameraRotation = Quaternion.Euler(16.193f, 90.26801f, 0.013f);
        yield return new WaitForSeconds(8f);
        CameraPostition = new Vector3(528.1527f, 111.3167f, -182.1593f);
        CameraRotation = Quaternion.Euler(22.38f, 30.454f, 0.015f);
        yield return new WaitForSeconds(8f);
        CameraPostition = new Vector3(636.8702f, -72.85394f, -5.076998f);
        CameraRotation = Quaternion.Euler(7.083f, 0.264f, 0.032f);
        
        
        
        // Quaternion.Euler(32.178f, -101.281f, 0.007f);
        
        
        // new Quaternion(32.178f, -101.281f, 0.007f, ); 
    }
}
