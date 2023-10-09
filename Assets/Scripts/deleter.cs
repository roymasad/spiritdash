using UnityEngine;
using System.Collections;

public class deleter : MonoBehaviour {

	public GameObject spawner;

    public bool deleteFloor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

	void OnTriggerEnter(Collider other) {


        //Debug.Log("recycle");


        if (other.name != "groundPlane")
        {

            if (other.name == "floor(Clone)" && deleteFloor == true)
            {
                    //other.gameObject.GetComponent<mover>().enabled = true;
                    spawner.GetComponent<spawner>().ReturnToPool(other.gameObject);
            }

             
            if (other.name != "floor(Clone)")

            {
                    other.gameObject.transform.parent.gameObject.GetComponent<mover>().enabled = true;
                    spawner.GetComponent<spawner>().ReturnToPool(other.gameObject.transform.parent.gameObject);
            }

		}
		
	}
}
