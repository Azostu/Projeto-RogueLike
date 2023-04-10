using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [Header("General")]
    SpriteRenderer spriteRenderer;

    [Header("Damage")]
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    [SerializeField] bool canShoot = true;
    
    
    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }
    
    void OnFire(InputValue value){
        
        if(canShoot){
            StartCoroutine(Fire(value));
        }

    }

    IEnumerator Fire(InputValue value){

        canShoot = false;
        Vector2 bulletDirection = value.Get<Vector2>();
        Vector2 bulletSpawnDelta = (transform.localScale * spriteRenderer.size) * bulletDirection;
        Vector3 bulletSpawnPosition = transform.position + new Vector3(bulletSpawnDelta.x, bulletSpawnDelta.y, 0);
        GameObject ins = Instantiate(bullet, bulletSpawnPosition, transform.rotation);
        ins.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
