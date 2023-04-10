using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    Rigidbody2D myRigidBody;
    [SerializeField] float speed = 5;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void OnMove(InputValue value)
    {
        myRigidBody.velocity = value.Get<Vector2>() * speed;
    }

}
