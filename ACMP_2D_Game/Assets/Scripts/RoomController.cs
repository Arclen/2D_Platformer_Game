using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RoomController : MonoBehaviour {

	private GameObject GO;
	private wall[] walls = new wall[4];

	public enum RoomType{spawn,normal};
	public RoomType roomType;

	// Structure of a wall in this room
	public struct wall {
		public GameObject wallGO;

		public bool isOpen;

		public enum Orientation { bottom, top, left, right };
		public Orientation orientation;

		public wall(GameObject go, Orientation o, bool open){

			wallGO = go;
			orientation = o;
			isOpen = open;
			setGameObject();
		}

		public wall(Orientation o){

			orientation = o;
			isOpen = false;
			setGameObject();
		}

		private void setGameObject(){
			GameObject openWallGO = Resources.Load<GameObject>("Prefabs/Room/WallOpen");
			GameObject closedWallGO = Resources.Load<GameObject>("Prefabs/Room/WallClosed");

			if (isOpen) {
				wallGO = openWallGO;
			} else {
				wallGO = closedWallGO;
			}
		}
	}

	// Use this for initialization
	void Start () {

		// Bottom Wall
		Instantiate(walls[0].wallGO, new Vector3(0f,-9f,0f) ,Quaternion.identity, gameObject);
		// Top Wall
		Instantiate(walls[1].wallGO, new Vector3(0f,9f,0f) , Quaternion.Euler(new Vector3(0f,0f,180f)), gameObject);
		// Left Wall
		Instantiate(walls[2].wallGO, new Vector3(-5f,0f,0f) , Quaternion.Euler(new Vector3(0f,0f,90f)), gameObject);
		// Right Wall
		Instantiate(walls[3].wallGO, new Vector3(5f,0f,0f) , Quaternion.Euler(new Vector3(0f,0f,270f)), gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Initialize room with passed in walls and room type
	public void Initialize(wall[] walls_, RoomType type){

		walls = walls_;
		roomType = type;

	}

	// Default initialize with all closed walls and normal room type
	public void Initialize(){

		walls [0] = new wall (wall.Orientation.bottom);
		walls [1] = new wall (wall.Orientation.top);
		walls [2] = new wall (wall.Orientation.left);
		walls [3] = new wall (wall.Orientation.right);

		roomType = RoomType.normal;
	}
}
