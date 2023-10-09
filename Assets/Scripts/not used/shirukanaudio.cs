using UnityEngine;
using System.Collections;

public class shirukanaudio : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	
		if (game.sfxON) GetComponent<AudioSource>().Play();

	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
