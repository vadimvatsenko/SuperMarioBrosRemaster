using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    [SerializeField] float fireBallSpeed = 20f;
    Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = transform.right * fireBallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);


        Destroy(gameObject);

    }
}
