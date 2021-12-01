using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidBody;

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        bulletRigidBody.velocity = transform.forward * speed;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
