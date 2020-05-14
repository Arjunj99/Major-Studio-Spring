using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLanternSpawner : MonoBehaviour {
    public GameObject lanternPrefab;
    public Vector2 radius;
    public Vector2Int thethas = new Vector2Int(0, 180);
    public List<LanternLighting> lanterns;
    private bool turnedOn = false;
    private float time;
    public Material off;
    public Material on;
    public Renderer rend;
    public ParticleSystem fire;
    public ParticleSystem smoke;
    public ParticleSystem embers;
    public CreditsManager creditsManager;
    public MusicManager musicManager;

    // Start is called before the first frame update
    void Start() {
        // rend = GetComponent<Renderer>();
        fire.emissionRate = 0f;
        smoke.emissionRate = 0f;
        embers.emissionRate = 0f;

        rend.materials[0] = off;
        int num = (thethas.y - thethas.x);
        for (int i = thethas.x; i < num; i++) {
            GameObject current = Instantiate(lanternPrefab, PointOnCircleWorldSpace(i * Mathf.Deg2Rad, Random.Range(radius.x, radius.y)), Quaternion.identity);
            current.GetComponent<LanternLighting>().yAxis = Random.Range(50, 180);
            lanterns.Add(current.GetComponent<LanternLighting>());
        }

        
    }

    // Update is called once per frame
    void Update() {
        if (turnedOn) {
            if (time < 1f) { time += Time.deltaTime/24; }
            if (time > 1f) { time = 1f; }
            fire.emissionRate = Mathf.Lerp(fire.emissionRate, 40f, Time.deltaTime/24);
            smoke.emissionRate = Mathf.Lerp(smoke.emissionRate, 20f, Time.deltaTime/24);
            embers.emissionRate = Mathf.Lerp(embers.emissionRate, 20f, Time.deltaTime/24);


            rend.materials[0].Lerp(off, on, time);            
        }
    }
    
    Vector3 PointOnCircle(float theta) {
        return new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
    }

    Vector3 PointOnCircleWorldSpace(float theta, float radius) {
        return (PointOnCircle(theta) * radius) + transform.position;
    }

    public IEnumerator RaiseAllLanters() {
        turnedOn = true;
        musicManager.currentType = MusicManager.AudioSourceType.end;
        musicManager.end.PlayScheduled(0);
        for (int i = 0; i < lanterns.Count; i++) {
            lanterns[i].turnedOn = true;
            musicManager.end.volume = Mathf.Lerp(musicManager.end.volume, 1f, Time.deltaTime);
            yield return new WaitForSeconds(0.045f);
        }

        // yield return new WaitForSeconds(5f);
        creditsManager.SetUpCredits();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            StartCoroutine(RaiseAllLanters());
            other.GetComponent<BoatMovement>().cutscene = true;
        }
    }
}

