using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIscroll : MonoBehaviour {

	public float scrollSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float offset = GetComponent<RawImage>().uvRect.x + scrollSpeed;

		Rect temp = new Rect();
		temp.width = GetComponent<RawImage>().uvRect.width;
		temp.height = GetComponent<RawImage>().uvRect.height;
		temp.x = offset;

		GetComponent<RawImage>().uvRect = temp;
	}
}
