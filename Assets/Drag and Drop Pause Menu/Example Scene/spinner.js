#pragma strict

function Start () {

}

function Update () {

transform.eulerAngles.y += 180*Time.deltaTime;
transform.eulerAngles.z += 90*Time.deltaTime;

}