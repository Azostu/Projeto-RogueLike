using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] GameObject coin;
    [SerializeField] GameObject potion;

    void Start()
    {
        
    }

    
    void Update()
    {
        
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
}
