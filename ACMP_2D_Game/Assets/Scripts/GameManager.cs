using System;
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

    static System.Random random;

	public Camera mainCamera;
	public RoomController currentRoom;
	public int levelSizeModifier;
	public int levelDifficultyModifier;
	public System.Collections.ArrayList rooms;

	private GameObject SPAWN_ROOM_GO;
	private GameObject DEFAULT_ROOM_GO;
	private GameObject EXIT_ROOM_GO;


	// Use this for initialization
	void Start () {

        random = new System.Random();

        rooms = new System.Collections.ArrayList();

		mainCamera = FindObjectOfType (typeof(Camera)) as Camera;
		SPAWN_ROOM_GO = Resources.Load<GameObject> ("Prefabs/Room/SpawnRoom");
		DEFAULT_ROOM_GO = Resources.Load<GameObject> ("Prefabs/Room/DefaultRoom");
		EXIT_ROOM_GO = Resources.Load<GameObject> ("Prefabs/Room/ExitRoom");

        Debug.Log("Room types initialized");

		//FOR TESTING
		InitializeLevelStart();

        Debug.Log("Level start initialized");

        GenerateLevel (5);

        Debug.Log("Level generated");
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
		GameObject nextRoom = GenerateRoomInDirection(currentRoom, RoomController.Direction.right, DEFAULT_ROOM_GO);
		rooms.Add (nextRoom);
		size--;

		//RoomController.Direction[] availableDirections = new RoomController.Direction[0];
        System.Collections.ArrayList availableDirections = new System.Collections.ArrayList();

		for (int i = 0; i < size; i++) {

			currentRoom = nextRoom.GetComponent<RoomController>();

			availableDirections = currentRoom.getClosedWalls ();

            

            rooms.Add(GenerateRoomInDirection(currentRoom, (RoomController.Direction)availableDirections[random.Next(availableDirections.Count)], DEFAULT_ROOM_GO));

			bool validRoomFound = false;

			while (!validRoomFound) {
				nextRoom = (GameObject)rooms [random.Next (rooms.Count)];

				if (nextRoom.GetComponent<RoomController> ().getClosedWalls ().Count >= 1)
					validRoomFound = true;
			}
		}

        availableDirections = nextRoom.GetComponent<RoomController>().getClosedWalls();
        GenerateRoomInDirection(nextRoom.GetComponent<RoomController>(), (RoomController.Direction)availableDirections[random.Next(availableDirections.Count)], EXIT_ROOM_GO);

	}

	// Generates a room in a relative direction to room
	public GameObject GenerateRoomInDirection(RoomController room, RoomController.Direction direction, GameObject roomType){

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

		GameObject nextRoom = Instantiate<GameObject> (roomType, directionToSpawn, room.transform.rotation);
		nextRoom.GetComponent<RoomController> ().connectToRoom (inverseDirection);
		room.connectToRoom (direction);

		return nextRoom;
	}
}
