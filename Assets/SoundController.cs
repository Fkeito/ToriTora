using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    private AudioSource audio;

    [SerializeField]
    private bool play;

    [SerializeField]
    private bool pause;

    [SerializeField]
    private float interval;

	void Start () {
        audio = this.GetComponent<AudioSource>();
	}
	
	void Update () {
        if (play) Play();
        if (pause) Pause();



	}

    public void Play()
    {
        audio.Play();
        play = false;
    }

    private void Pause()
    {
        audio.Pause();
        pause = false;
    }
}
