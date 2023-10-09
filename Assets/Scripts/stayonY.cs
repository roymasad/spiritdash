using UnityEngine;
using System.Collections;

public class stayonY : MonoBehaviour {

	public GameObject target;

	private Vector3 newpos;

	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
	
		newpos = new Vector3(this.transform.position.x, target.transform.position.y, this.transform.position.z  );


		this.transform.position = newpos;


	}
}
