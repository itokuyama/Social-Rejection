using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

	// Use this for initialization
	void Awake() {
	    if (instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        musicSource.Play();
	}

    public void PlaySingle (AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx (params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
