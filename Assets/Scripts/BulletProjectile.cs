using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material redMaterial;
    private Rigidbody _bulletRigidBody;
    


    private void Awake()
    {
        _bulletRigidBody = GetComponent<Rigidbody>();
        
        if (GameManager.Instance.redBullet)
        {
            GetComponentInChildren<MeshRenderer>().material = redMaterial;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material = yellowMaterial;
        }
    }

    private void Start()
    {
        if (GameManager.Instance.oneSecBullet)
        {
            StartCoroutine(destroyOneSec());
        }

        
        
        float speed = 40f;
        _bulletRigidBody.velocity = transform.forward * speed;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator destroyOneSec() {
        yield return new WaitForSecondsRealtime(0.1f); //Wait 1 second
        Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
