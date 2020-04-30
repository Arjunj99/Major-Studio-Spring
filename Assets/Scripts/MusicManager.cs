using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public AudioSource peace;
    public AudioSource tense;
    public AudioSource festival;
    public enum AudioSourceType {peace, tense, festival};
    public AudioSourceType currentType;
    public int maxMusic = 1;
    public int speedMod = 2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (currentType == AudioSourceType.peace) {
            peace.volume = Mathf.Lerp(peace.volume, maxMusic, Time.deltaTime * speedMod);
            tense.volume = Mathf.Lerp(tense.volume, 0, Time.deltaTime * speedMod);
            festival.volume = Mathf.Lerp(festival.volume, 0, Time.deltaTime * speedMod);
        } else if (currentType == AudioSourceType.tense) {
            peace.volume = Mathf.Lerp(peace.volume, 0, Time.deltaTime * speedMod);
            tense.volume = Mathf.Lerp(tense.volume, maxMusic, Time.deltaTime * speedMod);
            festival.volume = Mathf.Lerp(festival.volume, 0, Time.deltaTime * speedMod);
        } else if (currentType == AudioSourceType.festival) {
            peace.volume = Mathf.Lerp(peace.volume, 0, Time.deltaTime * speedMod);
            tense.volume = Mathf.Lerp(tense.volume, 0, Time.deltaTime * speedMod);
            festival.volume = Mathf.Lerp(festival.volume, maxMusic, Time.deltaTime * speedMod);
        }
    }
}
