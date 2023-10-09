using UnityEngine;
using System.Collections;

public class landrotate : MonoBehaviour {


	public Vector3 axe;

	public float speed;

	private float originalspeed;

	public int pattern;

	private bool enabled;

	// Use this for initialization
	void Start () {
	
		axe = new Vector3(0.0f,0.0f,1.0f);

		pattern = 1;

		originalspeed = speed;

		enabled = false;

	}

	public void play() {

		enabled = true;

	}

	// Update is called once per frame
	void FixedUpdate () {
	
		if (!enabled) return;

		StartCoroutine("PatternSwitch", 2); 


		if (pattern == 1) { axe = new Vector3(0.0f,0.0f,1.0f); speed = originalspeed * -1;}
		if (pattern == 2) { axe = new Vector3(0.0f,0.0f,1.0f); speed = originalspeed;}
		if (pattern == 3) { axe = new Vector3(0.0f,0.0f,1.0f); }


		//Vector3 newaxe = new Vector3(axe.x * Time.deltaTime * speed,axe.y *Time.deltaTime * speed,axe.z *Time.deltaTime * speed);
		Vector3 newaxe = new Vector3(0,0,axe.z *Time.deltaTime * speed);
		
		transform.Rotate(newaxe.x,newaxe.y,newaxe.z);

		if (pattern == 3) transform.rotation = Quaternion.identity;
	}

	IEnumerator PatternSwitch(float delay)  
	{
		
		yield return new WaitForSeconds(delay);  
		
		pattern++;

		if (pattern > 3) { pattern = 1; enabled = false; }
		
		StopCoroutine("PatternSwitch");  
		
	}
}
