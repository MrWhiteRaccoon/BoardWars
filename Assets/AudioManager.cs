using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    AudioSource audioSource;
    int currentTrack=0;

    public AudioClip[] tracks;


	void Start () {
        audioSource = GetComponent<AudioSource>();
        currentTrack = 0;
        audioSource.clip = tracks[0];

        audioSource.Play();

        StartCoroutine(JukeBox());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator JukeBox()
    {
        while (true)
        {
            float songTime = audioSource.clip.length;

            while (songTime > 0)
            {
                //Debug.Log(songTime);
                songTime -= Time.deltaTime;

                yield return null;
            }
            if (currentTrack + 1 >= tracks.Length)
            {
                currentTrack = 0;
            }
            else { currentTrack++; }
            audioSource.Stop();
            audioSource.clip = tracks[currentTrack];
            audioSource.Play();
        }
    }
}
