using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    public float maxSpeed = 6;
    public float jumpTakeOffSpeed = 6;
    
	// Use this for initialization
	void Start () {
		
	}

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump")) {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        targetVelocity = move * maxSpeed;
    }
}
