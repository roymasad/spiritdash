#pragma strict

static var movingspeed = 0.85;

var staticspeed: boolean = false;

static var ended = false;

function Start () {

	ended = false;
	
}

// Scroll main texture based on time

var scrollSpeed : float = 0.5;

function Update () {

	if (ended == true) return;
	//if (gameObject.layer == 14) return;
	
	var offset : float;
	
 	if (staticspeed) offset = Time.time * scrollSpeed;
 	else offset = Time.time * scrollSpeed * movingspeed;
	
	GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", Vector2(offset,0));
	
}