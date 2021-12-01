using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    
    private Rigidbody bulletRigidBody;
    

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 40f;
        bulletRigidBody.velocity = transform.forward * speed;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
