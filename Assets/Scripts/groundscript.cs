using UnityEngine;
using System.Collections;
using System.Reflection;

public class groundscript : MonoBehaviour {



	public int floor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

//	void OnCollisionStay2 (Collision other) {
//
//		if (gameObject.layer == 14) return;
//
//		//Debug.Log (other.gameObject.name);
//		if (other.gameObject.name == "horse" && other.gameObject.GetComponent<horse>().state != "running") {
//
//
//			if (other.gameObject.transform.position.y < transform.position.y) return;
//			//Debug.Log("running");
//
//			other.gameObject.GetComponent<horse>().state = "running";
//
//			other.gameObject.GetComponent<ImageSequenceSingleTexture>().jumping = false;
//
//			other.gameObject.transform.Rotate(new Vector3(0,+15,0));
//
//
//
//		}
//
//
//
//	}

	void OnCollisionEnter (Collision other) {

		if (floor == 0 && other.gameObject.name == "horse") { 

			other.gameObject.GetComponent<horse>().dust1.Play(); 
			other.gameObject.GetComponent<horse>().landingParticle.Play(); 

			if (game.sfxON) AudioSource.PlayClipAtPoint(other.gameObject.GetComponent<horse>().thud, other.gameObject.transform.position, 0.8f);

			other.gameObject.GetComponent<horse>().lastTouchedFloor = this.transform.gameObject;
		}


		if (other.gameObject.name == "horse" && other.gameObject.GetComponent<horse>().state != "running") {
			
			
			//if (other.gameObject.transform.position.y < transform.position.y) return;
			//Debug.Log("running1");
			
			other.gameObject.GetComponent<horse>().resetHorsy();

            other.gameObject.GetComponent<ImageSequenceSingleTexture>().jumping = false;
			
			other.gameObject.transform.Rotate(new Vector3(0,+15,0));

			other.gameObject.GetComponent<horse>().lastTouchedFloor = this.transform.gameObject;

			
		}





    }




}
