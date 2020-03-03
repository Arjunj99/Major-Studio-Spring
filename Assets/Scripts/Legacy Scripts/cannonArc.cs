using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonArc : MonoBehaviour {
    public Vector3 boatPos;
    public Vector3 impact;
    private Vector3 arcPoint;
    private bool arcComplete = false;
    private Rigidbody bombRB;


    // Start is called before the first frame update
    void Start() {
        bombRB = gameObject.GetComponent<Rigidbody>();
        arcPoint = new Vector3((boatPos.x), 100f, (boatPos.z));
        
        bombRB.AddExplosionForce(0.1f, new Vector3(Random.Range(-1,1), 2, Random.Range(-1,1)), 0.1f, 0.1f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update() {
        // if (Input.GetKey(KeyCode.Q))
                // bombRB.AddExplosionForce(8f, new Vector3(1,1,1), 3f, 3f, ForceMode.Impulse);

        
        // if (arcComplete == false) {
        //     this.transform.position = Vector3.Lerp(this.transform.position, arcPoint, 3f * Time.deltaTime);
        //     if (this.transform.position == arcPoint) {
        //         arcComplete = true;
        //     }
        // } else {
        //     this.transform.position = Vector3.Lerp(this.transform.position, impact, 3f * Time.deltaTime);
        //     if (this.transform.position == impact) {
        //         Destroy(gameObject);
        //     }
        // }
    }
}
