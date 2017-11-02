using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    public float maxSpeed = 6;
    public float jumpTakeOffSpeed = 6;

    private List<GameObject> Projectiles = new List<GameObject>();
    public GameObject projectilePrefab;
    private float projectileVelocity;
    
	// Use this for initialization
	void Start () {
        projectileVelocity = 3;
	}

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        if (move.x > 0)
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        if(move.x < 0)
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump")) {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        targetVelocity = move * maxSpeed;
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject bullet = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectiles.Add(bullet);
        }
        //bool trueForLeft = (this.gameObject.GetComponent<SpriteRenderer>().flipX == true);
        for (int i=0; i < Projectiles.Count; i++)
        {
            GameObject goBullet = Projectiles[i];
            if (goBullet != null)
            {
            //    if(trueForLeft)
                    goBullet.transform.Translate(new Vector3(2, 0) * Time.deltaTime * projectileVelocity);
              //  else goBullet.transform.Translate(new Vector3(-2, 0) * Time.deltaTime * projectileVelocity);

                Vector3 bulletScreenPos = Camera.main.WorldToScreenPoint(goBullet.transform.position);
                if(bulletScreenPos.x >= Screen.width || bulletScreenPos.x <= 0)
                {
                    DestroyObject(goBullet);
                    Projectiles.Remove(goBullet);
                }
            }
        }
    }
}
