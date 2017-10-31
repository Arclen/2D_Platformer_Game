using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Cache for the instance of the game manager
	private static GameManager INSTANCE = null;

	// Will determine if their already exists a gamemanager in the scene, if so that will become the instance, if not create an instance
	public static GameManager instance {
		get {
			if (INSTANCE == null) {
				INSTANCE = FindObjectOfType (typeof(GameManager)) as GameManager;
			}

			if (INSTANCE == null) {
				GameObject obj = new GameObject ("GameManager");
				INSTANCE = obj.AddComponent (typeof(GameManager)) as GameManager;
			}

			return INSTANCE;
		}
	}

	// Destroy the intance of the gamemanager on exiting the application
	void OnApplicationQuit(){
		INSTANCE = null;
	}

	public Camera mainCamera;
	public RoomController currentRoom;
	public int levelSizeModifier;
	public int levelDifficultyModifier;
	public LinkedList<GameObject> rooms;

	private GameObject SPAWN_ROOM_GO;
	private GameObject DEFAULT_ROOM_GO;
	private GameObject EXIT_ROOM_GO;


	// Use this for initialization
	void Start () {

		mainCamera = FindObjectOfType (typeof(Camera)) as Camera;
		SPAWN_ROOM_GO = Resources.Load<GameObject> ("Prefabs/Room/SpawnRoom");
		DEFAULT_ROOM_GO = Resources.Load<GameObject> ("Prefabs/Room/DefaultRoom");
		EXIT_ROOM_GO = Resources.Load<GameObject> ("Prefabs/Room/ExitRoom");

		//FOR TESTING
		InitializeLevelStart();

		GenerateLevel (5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FocusCameraOnThis(GameObject GObject){

		mainCamera.transform.position = new Vector3 (GObject.transform.position.x, GObject.transform.position.y, mainCamera.transform.position.z);
	}

	public void InitializeLevelStart()
	{
		GameObject spawnRoom = Instantiate<GameObject> (SPAWN_ROOM_GO,new Vector3(0f,0f,0f),Quaternion.identity);
		FocusCameraOnThis (spawnRoom);
		currentRoom = spawnRoom.GetComponentInChildren<RoomController> ();

	}

	//Get a random number based on the level size modifier and send it to generateLevel()
	public void GenerateLevelRandomSize(){


	}

	public void GenerateLevel(int size){

		//Generate first room to the right of the originial room
		GameObject nextRoom = GenerateRoomInDirection(currentRoom, RoomController.Direction.right);
		rooms.AddLast (nextRoom);
		size--;

		for (int i = 0; i < size; i++) {

			currentRoom = nextRoom.GetComponent<RoomController>();

			LinkedList<RoomController.Direction> availableDirectionsList = currentRoom.getClosedWalls();
			RoomController.Direction[] availableDirections = new RoomController.Direction[availableDirectionsList.Count];
			for(int j = 0; j < availableDirectionsList.Count; j++) {
				availableDirectionsList.CopyTo (availableDirections, j);
			}

			nextRoom = GenerateRoomInDirection (currentRoom, availableDirections[Random.Range(0,availableDirectionsList.Count -1 )]);

			rooms.AddLast(currentRoom.gameObject);


		} 


	}

	// Generates a room in a relative direction to room
	public GameObject GenerateRoomInDirection(RoomController room, RoomController.Direction direction){

		Vector3 rightRoomRelative = new Vector3 ((room.transform.position.x + room.GetComponent<BoxCollider2D> ().size.x * room.transform.localScale.x), room.transform.position.y, room.transform.position.z);
		Vector3 leftRoomRelative = new Vector3 ((room.transform.position.x + room.GetComponent<BoxCollider2D> ().size.x * -room.transform.localScale.x), room.transform.position.y, room.transform.position.z);
		Vector3 topRoomRelative = new Vector3 (room.transform.position.x, (room.transform.position.y + room.GetComponent<BoxCollider2D> ().size.y * room.transform.localScale.y), room.transform.position.z);
		Vector3 bottomRoomRelative = new Vector3 (room.transform.position.x, (room.transform.position.y + room.GetComponent<BoxCollider2D> ().size.y * -room.transform.localScale.y), room.transform.position.z);

		Vector3 directionToSpawn = new Vector3(0f,0f,0f);
		RoomController.Direction inverseDirection = RoomController.Direction.down;

		switch (direction) {
		case RoomController.Direction.down:
			directionToSpawn = bottomRoomRelative;
			inverseDirection = RoomController.Direction.up;
			break;
		case RoomController.Direction.up:
			directionToSpawn = topRoomRelative;
			inverseDirection = RoomController.Direction.down;
			break;
		case RoomController.Direction.left:
			directionToSpawn = leftRoomRelative;
			inverseDirection = RoomController.Direction.right;
			break;
		case RoomController.Direction.right:
			directionToSpawn = rightRoomRelative;
			inverseDirection = RoomController.Direction.left;
			break;
		}

		GameObject nextRoom = Instantiate<GameObject> (DEFAULT_ROOM_GO, directionToSpawn, room.transform.rotation);
		nextRoom.GetComponent<RoomController> ().connectToRoom (inverseDirection);

		return nextRoom;
	}
}
