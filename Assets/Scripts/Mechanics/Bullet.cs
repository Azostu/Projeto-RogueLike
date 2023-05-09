using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] float damage = 1.0f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

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
            case "Crate":
                Destroy(collision.gameObject); break;
            default: 
                break;
        }

        Destroy(gameObject);



    }

    public void SetDamage(float damage){
        this.damage = damage;
    }

}
