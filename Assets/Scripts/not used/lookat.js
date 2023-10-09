#pragma strict
 
// #################################################################################################
// This complete script can be attached to a camera to make it continuously point at another object.
// #################################################################################################
 
// Camera target public variable START #############################################################
// The target variable shows up as a property in the inspector. Drag another object onto it to make the camera look at it.
var cameraTarget : Transform;
// Camera target public variable END ###############################################################
 
function Start () {
 
}
     
function Update() {
     
}
     
function LateUpdate () {
//LateUpdate is called after all Update functions have been called.
// This is useful to order script execution. For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside Update.
transform.LookAt(cameraTarget);
}