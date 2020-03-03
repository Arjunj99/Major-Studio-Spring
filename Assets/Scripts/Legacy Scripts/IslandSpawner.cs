using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour {
    public List<GameObject> islandList = new List<GameObject>();
    public List<Vector3> islandLocations = new List<Vector3>();
    public List<GameObject> spawnedIslands = new List<GameObject>();
    public bool gameWon;


    int islandElements = 5;


    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < islandElements; i++) { // Spawns X number of random islands
            int randomIsland = Random.Range(0,islandList.Count);
            spawnedIslands.Add(Instantiate(islandList[randomIsland], islandLocations[i], Quaternion.Euler(-90,0,0)));
            islandList.RemoveAt(randomIsland);
        }
    }

    // Update is called once per frame
    void Update() { // Checks if all spawnedIslands have been explored. If so, win the game.
        int i = 0;
        for (int j = 0; j < spawnedIslands.Count; j++) {
            if (spawnedIslands[j].GetComponentInChildren<CameraTrigger>().isLit) {
                i++;
            }
        }
        if (i == spawnedIslands.Count)
            gameWon = true;
        
    }
}
