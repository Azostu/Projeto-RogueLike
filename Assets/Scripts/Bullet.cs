using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D myRigidbody;
    [SerializeField] float damage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                break;
            case "Player":
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                break;
            default: 
                break;
        }

        Destroy(gameObject);



    }

    public void SetDamage(float damage){
        this.damage = damage;
    }

}
