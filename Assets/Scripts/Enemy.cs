using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float damage = 1;
    [SerializeField] float health = 1;
    [SerializeField] float speed = 1;
    Rigidbody2D myRigidBody;
    GameObject player;
    Transform target;
    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
            Destroy(gameObject);

        Move();
    }


    private void Move()
    {
        if(player != null)
        {
            target = player.transform;
            direction = target.position - transform.position;
            if (direction.magnitude < 25)
                myRigidBody.velocity = direction.normalized * speed;
            else
                myRigidBody.velocity = new Vector2(0, 0);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.gameObject.tag.Equals("Player")){

            collision.gameObject.GetComponent<PlayerHealth>().LoseHealth(damage);
        }

    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
    }
}
