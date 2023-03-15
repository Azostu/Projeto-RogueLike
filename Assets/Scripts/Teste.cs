using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    [SerializeField] float damage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag.Equals("Player")){

            collision.gameObject.GetComponent<PlayerHealth>().LoseHealth(damage);
        }
        
    }

}
