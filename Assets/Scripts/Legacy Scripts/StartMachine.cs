using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMachine : MonoBehaviour
{
    private bool hasStart = false;
    public Rigidbody rb;
    public Rigidbody cyl;

    public GameObject BIGball;
    public Rigidbody ball2;
    public Rigidbody ball3;
    public Rigidbody ball4;
    private bool part2;
    private bool part3;
    private bool part4;

    private bool part5;

    public GameObject floor;
    public GameObject path1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStart == false && Input.GetKey(KeyCode.Space)) {
            hasStart = true;
            rb.AddForce((this.transform.right * 5), ForceMode.Impulse);
            StartCoroutine(rollCyl());
        }
        if (part2 == true) {
            part2 = false;
            cyl.AddForce((this.transform.forward * 10), ForceMode.Impulse);
            StartCoroutine(destroyWorld());

        }

        if (part3 == true) {
            // Debug.Log("d");
            Destroy(path1);
            Destroy(floor);
            part3 = false;
            StartCoroutine(ballBoost());
        }

        if (part4 == true) {
            // Debug.Log("D");
            // part4 = false;
            BIGball.transform.position += new Vector3(2f,0f,0f);
            // ball2.AddForce((transform.right * 300), ForceMode.Force);
            part5 = true;
            // ball2.AddForce((transform.right * 180), ForceMode.Impulse);
        }

        if (part5 == true)
            BIGball.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        // if (part5 == true) {
        //     ball2.add
        // }

        if (Input.GetKey(KeyCode.Space)) {
            // ball2.AddForce((transform.right * 180), ForceMode.Impulse);

        }
    }

    IEnumerator rollCyl() {
        yield return new WaitForSeconds(5.6f);
        part2 = true;
        // cyl.AddForce((this.transform.forward), ForceMode.Impulse);
        // Debug.Log("d");
    }

    IEnumerator destroyWorld() {
        yield return new WaitForSeconds(2.6f);
        part3 = true;
        // cyl.AddForce((this.transform.forward), ForceMode.Impulse);
        // Debug.Log("d");
    }

    IEnumerator ballBoost() {
        yield return new WaitForSeconds(32f);
        part4 = true;
        // cyl.AddForce((this.transform.forward), ForceMode.Impulse);
        // Debug.Log("d");
    }
}
