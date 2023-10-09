using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mover : MonoBehaviour {

	public Vector3 axe;
	private Vector3 newaxe;

	public bool homeinmode;
	public GameObject target;

	public string originaltag;

	public static float movingspeed = 0.85f;

	// Use this for initialization
	void Start () {

        //movingspeed = 1.0f/2.0f;


    }
	
	// Update is called once per frame
	void Update () {
	
		if (game.paused == true || game.ended == true) return;

		if (homeinmode != true)
		{
			//newaxe = new Vector3(axe.x * Time.deltaTime,axe.y *Time.deltaTime,axe.z *Time.deltaTime);
			newaxe = new Vector3(axe.x * Time.deltaTime*movingspeed*1.1f,0,0);
			
			transform.Translate(newaxe.x,newaxe.y,newaxe.z);
            
		}
		else {


			newaxe = new Vector3(1.3f*axe.x * Time.deltaTime,axe.y *Time.deltaTime,axe.z *Time.deltaTime);
			
			transform.Translate(newaxe.x,newaxe.y,newaxe.z);


			Vector3 temp;

			temp.x = transform.position.x;
			temp.y = transform.position.y;
			temp.z = transform.position.z;

			if (target.transform.position.y < temp.y) temp.y -= 0.005f;
			else if (target.transform.position.y > temp.y) temp.y += 0.005f;


			this.transform.position = temp;


		}

	}
}
