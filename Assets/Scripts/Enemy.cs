using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("General")]
    [SerializeField] float health = 1;
    [SerializeField] float speed = 1;
    [SerializeField] int enemyType = 0;
    Rigidbody2D myRigidBody;
    SpriteRenderer spriteRenderer;

    [Header("Damage")]
    [SerializeField] float damage = 1;
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 0;
    bool canShoot = true;

    [Header("Items")]
    [SerializeField] GameObject coin;
    [SerializeField] GameObject potion;

    [Header("Movement")]
    GameObject player;
    Transform target;
    Vector3 direction;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(health <= 0)
            Destroy(gameObject);

        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Player"))
        {

            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

    }

    private void OnDestroy()
    {
        int rand = Random.Range(0, 6);

        switch (rand)
        {
            case 0: Instantiate(coin, transform.position, transform.rotation); break;
            case 5: Instantiate(potion, transform.position, transform.rotation); break;
            default: break;
        }

    }

    private void Move()
    {
        if(player != null)
        {
            target = player.transform;
            direction = target.position - transform.position;

            if (direction.magnitude < 25)
            {
                myRigidBody.velocity = direction.normalized * speed;
                if (direction.magnitude <= 13 && enemyType == 1 && canShoot)
                {
                    StartCoroutine(Fire(direction.normalized));
                }
                    
            }   
            else
                myRigidBody.velocity = new Vector2(0, 0);
        }
        
    }

    IEnumerator Fire(Vector3 direction)
    {

        canShoot = false;
        Vector2 bulletSpawnDelta = (transform.localScale * spriteRenderer.size) * direction;
        Vector3 bulletSpawnPosition = transform.position + new Vector3(bulletSpawnDelta.x, bulletSpawnDelta.y, 0);
        GameObject ins = Instantiate(bullet, bulletSpawnPosition, transform.rotation);
        ins.GetComponent<Bullet>().SetDamage(damage);
        ins.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;

    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
    }

}
