using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour {

    public Transform Player;
    public Transform Enemy;
    private Vector3 pos;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        pos = this.gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
            //pos = Enemy.position;
            //this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //var angle = Player.position - Enemy.position;
            pos.x = pos.x - (float).01;
            this.gameObject.transform.position = pos;
            //this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}
}
