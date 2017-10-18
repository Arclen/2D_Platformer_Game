using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Camera mainCamera;
	public RoomController currentRoom;

	// Use this for initialization
	void Start () {

		mainCamera = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FocusCameraOnThis(GameObject GObject){

		mainCamera.transform.position = new Vector3 (GObject.transform.position.x, GObject.transform.position.y, mainCamera.transform.position.z);
	}
}
