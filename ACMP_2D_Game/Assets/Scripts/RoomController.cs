using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RoomController : MonoBehaviour {

	public GameObject bottomWall;
	public GameObject topWall;
	public GameObject leftWall;
	public GameObject rightWall;

	private bool leftClosed = true;
	private bool rightClosed = true;
	private bool topClosed = true;
	private bool bottomClosed = true;

	public GameObject CLOSED_WALL_GO;
	public GameObject OPEN_WALL_GO;


	public enum Direction{left, right, up, down};

	// Use this for initialization
	void Start () {

		bottomWall = transform.Find ("bottom").gameObject;
		topWall = transform.Find ("top").gameObject;
		leftWall = transform.Find ("left").gameObject;
		rightWall = transform.Find ("right").gameObject;

		CLOSED_WALL_GO = Resources.Load<GameObject> ("Prefabs/Room/WallClosed");
		OPEN_WALL_GO = Resources.Load<GameObject> ("Prefabs/Room/WallOpen");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetWallFromDirection(Direction dir){

		switch (dir) {
		case Direction.down:
			return bottomWall;
			break;
		case Direction.up:
			return topWall;
			break;
		case Direction.left:
			return leftWall;
			break;
		case Direction.right:
			return rightWall;
			break;
		}

		return null;
	}
		
	public void connectToRoom(Direction dir){

		GameObject wallToChange = null;

		bottomWall = transform.Find ("bottom").gameObject;
		topWall = transform.Find ("top").gameObject;
		leftWall = transform.Find ("left").gameObject;
		rightWall = transform.Find ("right").gameObject;

		switch (dir) {
		case Direction.down:
			wallToChange = bottomWall;
			bottomClosed = false;
			break;
		case Direction.up:
			wallToChange = topWall;
			topClosed = false;
			break;
		case Direction.left:
			wallToChange = leftWall;
			leftClosed = false;
			break;
		case Direction.right:
			wallToChange = rightWall;
			rightClosed = false;
			break;
		}
			
		SwitchWall (wallToChange);
	}

	public ArrayList getClosedWalls(){

		ArrayList closedWalls = new ArrayList();

		if (rightClosed) {
			closedWalls.Add (Direction.right);
		}
		if (topClosed) {
			closedWalls.Add (Direction.up);
		}
		if (leftClosed) {
			closedWalls.Add (Direction.left);
		}
		if (bottomClosed) {
			closedWalls.Add (Direction.down);
		}

		return closedWalls;

	}

	public void SwitchWall(GameObject wallToChange)
    {

		CLOSED_WALL_GO = Resources.Load<GameObject> ("Prefabs/Room/WallClosed");
		OPEN_WALL_GO = Resources.Load<GameObject> ("Prefabs/Room/WallOpen");

		GameObject newWall = OPEN_WALL_GO;

		GameObject spawnedWall = Instantiate (newWall, wallToChange.transform.position, wallToChange.transform.rotation, transform);
		spawnedWall.transform.localScale = wallToChange.transform.localScale;

		Destroy (wallToChange);
    }

	private bool IsDirectionClosed (Direction dir){

		switch (dir) {
		case Direction.down:
			return bottomClosed;
		case Direction.up:
			return topClosed;
		case Direction.left:
			return leftClosed;
		case Direction.right:
			return rightClosed;
		}

		return true;
	}
		
}
