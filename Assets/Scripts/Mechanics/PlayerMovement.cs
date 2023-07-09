using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    Rigidbody2D myRigidBody;
    [SerializeField] float speed = 5;

    [Header("Pause Menu")]
    [SerializeField]
    GameObject panel;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        panel.SetActive(false);
    }

    void Update()
    {

    }

    void OnMove(InputValue value)
    {
        myRigidBody.velocity = value.Get<Vector2>() * speed;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            gameObject.transform.localScale = new Vector3(Mathf.Sign(myRigidBody.velocity.x), 1, 1);
        }
            
    }

    void OnMenu(InputValue value)
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

}
