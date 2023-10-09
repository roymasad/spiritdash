#pragma strict

var axe: Vector3;
private var newaxe: Vector3;

function Start () {

	

}

function Update () {

	//if (gameObject.layer == 14) return;
	
	newaxe = new Vector3(axe.x * Time.deltaTime,axe.y *Time.deltaTime,axe.z *Time.deltaTime);

 	transform.Rotate(newaxe.x,newaxe.y,newaxe.z);
}