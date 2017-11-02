//Code created by Jacob Scanlan, 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EnemyController : PhysicsObject {
    private int[] varChoices;
    private int randomDir;
    private Vector2 enemySpeed;
    public bool ranged;
    public int speedMultiplier;
    public int HP;
    private float nextActionTime = 0.0f;
    public float period = 0.5f;
    public bool chasePlayer;
    public Transform Player;
    public GameObject Projectile;
    private double shootbullet = 3f;

    // Use this for initialization
    void Start () {
        varChoices = new int[2];
        varChoices[0] = -1;
        varChoices[1] = 1;
	}

	// Update is called once per frame
	void Update () {
        if(chasePlayer)
        {
            //move towards the player
            if (Mathf.Abs(Player.position.x - transform.position.x) > 3.5f)
            {//move if distance from target is greater than 1
                enemySpeed.x = (Player.position.x - transform.position.x) / Mathf.Abs(Player.position.x - transform.position.x);
                targetVelocity = speedMultiplier * enemySpeed;
            }
            else
                targetVelocity -= targetVelocity;
        }
        else
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                randomDir = Random.Range(0, 2);
                int direction = varChoices[randomDir];
                enemySpeed.x = direction;
                targetVelocity = speedMultiplier * enemySpeed;
            }
        }
        if(Time.time > shootbullet)
        {
            shootbullet += 3;
            GameObject bullet = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
            Vector3 angle = Player.transform.position - transform.position;
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.GetComponent<Rigidbody2D>().velocity = angle * 2;
            Destroy(bullet, 2);
        }
	}
}