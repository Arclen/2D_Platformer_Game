using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(Attack());
            /*move.x = Input.GetAxis("Horizontal");
            if (this.gameObject.GetComponentInParent(typeof(SpriteRenderer)).GetComponent<SpriteRenderer>().flipX == false)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                this.gameObject.GetComponent<Transform>().position = new Vector3(-this.gameObject.GetComponentInParent(typeof(Transform)).GetComponent<Transform>().position.x, this.gameObject.GetComponentInParent(typeof(Transform)).GetComponent<Transform>().position.y, this.gameObject.GetComponentInParent(typeof(Transform)).GetComponent<Transform>().position.z);
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }*/
        }
    }

    IEnumerator Attack()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

}