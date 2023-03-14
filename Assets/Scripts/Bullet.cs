using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //myRigidbody.velocity = new Vector2(bulletSpeed,0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hello");
    }
    
}
