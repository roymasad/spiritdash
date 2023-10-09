using UnityEngine;
using System.Collections;

public class music : MonoBehaviour {


	public AudioClip music1;
	public AudioClip music2;
	public AudioClip music3;
	//public AudioClip music4;

	public GameObject maingame;

    // Use this for initialization
    void Start() {



        //int selection = Random.Range(1,3);

        //if (selection == 1) this.transform.GetComponent<AudioSource>().clip = music1;
        //if (selection == 2) this.transform.GetComponent<AudioSource>().clip = music2;

        
        this.transform.GetComponent<AudioSource>().volume = 0.8f;

        if (maingame.GetComponent<game>().backgroundname == "duneday") { this.transform.GetComponent<AudioSource>().clip = music1; this.transform.GetComponent<AudioSource>().volume = 0.8f; }
        if (maingame.GetComponent<game>().backgroundname == "dunenight") this.transform.GetComponent<AudioSource>().clip = music2;
        if (maingame.GetComponent<game>().backgroundname == "dark") { this.transform.GetComponent<AudioSource>().clip = music1; this.transform.GetComponent<AudioSource>().volume = 1.0f; }
        if (maingame.GetComponent<game>().backgroundname == "waterday") this.transform.GetComponent<AudioSource>().clip = music2;

        //this.transform.GetComponent<AudioSource>().GetComponent<AudioSource>().loop=true;


        if (game.musicON) this.transform.GetComponent<AudioSource>().Play();
        else this.transform.GetComponent<AudioSource>().Pause();
		


	
	}
	
	// Update is called once per frame
	void Update () {

        StartCoroutine("checkMusic", 1f);


	}

    IEnumerator checkMusic(float time)
    {

        yield return new WaitForSeconds(time);


        if (game.musicON)
        {

            if (!this.transform.GetComponent<AudioSource>().isPlaying)
            {
                this.transform.GetComponent<AudioSource>().clip = music3;
                this.transform.GetComponent<AudioSource>().Play();
                this.transform.GetComponent<AudioSource>().GetComponent<AudioSource>().loop = true;

            }


        }

            StopCoroutine("checkMusic");

    }

}
