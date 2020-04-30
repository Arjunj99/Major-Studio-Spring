using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSpawner : MonoBehaviour
{
    public GameObject lanternPrefab;
    public Vector3 zone;
    public int numOfLanterns;

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < numOfLanterns; i++) {
            Instantiate(lanternPrefab, this.transform.position + new Vector3(Random.Range(-zone.x, zone.x), Random.Range(-zone.y, zone.y), Random.Range(-zone.z, zone.z)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
