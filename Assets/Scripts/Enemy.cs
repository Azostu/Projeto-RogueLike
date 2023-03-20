using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float damage = 1;
    [SerializeField] float health = 1;
    [SerializeField] float speed = 1;
    [SerializeField] int enemyType = 0;
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 0;
    Rigidbody2D myRigidBody;
    GameObject player;
    Transform target;
    Vector3 direction;
    bool canShoot = true;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer= GetComponent<SpriteRenderer>();
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
            Debug.Log("Target position " + target.position + " Enemy Position " + transform.position);
            if (direction.magnitude < 25)
            {
                myRigidBody.velocity = direction.normalized * speed;
                if (direction.magnitude <= 20 && enemyType == 1 && canShoot)
                {
                    myRigidBody.velocity = new Vector2(0, 0);
                    StartCoroutine(Fire(direction));
                }
                    
            }   
            else
                myRigidBody.velocity = new Vector2(0, 0);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.gameObject.tag.Equals("Player")){

            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
    }

    IEnumerator Fire(Vector3 direction){

        Debug.Log("Enemy Positon: " + transform.position);
        canShoot = false;
        Vector2 bulletSpawnDelta = (transform.localScale * spriteRenderer.size) * direction;
        Vector3 bulletSpawnPosition = transform.position + new Vector3(bulletSpawnDelta.x, bulletSpawnDelta.y, 0);
        GameObject ins = Instantiate(bullet, bulletSpawnPosition, transform.rotation);
        ins.GetComponent<Bullet>().SetDamage(damage);
        ins.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;

    }
}
