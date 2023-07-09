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
    [SerializeField] float damage = 1.0f;
    Animator myAnimator;
    
    
    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        bullet.GetComponent<Bullet>().SetDamage(damage);
        myAnimator= GetComponent<Animator>();
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
        myAnimator.SetBool("isAttacking", true);
        Vector2 bulletDirection = value.Get<Vector2>();
        Vector2 bulletSpawnDelta = (new Vector3(1,1,1) * spriteRenderer.size) * bulletDirection;
        Vector3 bulletSpawnPosition = transform.position + new Vector3(bulletSpawnDelta.x, bulletSpawnDelta.y, 0);
        GameObject ins = Instantiate(bullet, bulletSpawnPosition, transform.rotation);
        ins.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
        
        yield return new WaitForSeconds(fireRate);
        myAnimator.SetBool("isAttacking", false);
        canShoot = true;
    }

    public void MultiplyValues(float multiplier)
    {
        bulletSpeed *= multiplier;
        fireRate *= multiplier;
    }

    public void MultiplyDamage(float multiplier)
    {
        damage *= multiplier;
        bullet.GetComponent<Bullet>().SetDamage(damage);
    }
}
