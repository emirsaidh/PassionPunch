using System.Collections;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material redMaterial;
    private Rigidbody _bulletRigidBody;
    private float _speed = 40f;
    


    private void Awake()
    {
        _bulletRigidBody = GetComponent<Rigidbody>();
        //Change material if red bullet option selected
        GetComponentInChildren<MeshRenderer>().material = GameManager.Instance.redBullet ? redMaterial : yellowMaterial;
    }

    private void Start()
    {
        //Start coroutine if one second bullet option selected
        if (GameManager.Instance.oneSecBullet)
        {
            StartCoroutine(destroyOneSec());
        }
        _bulletRigidBody.velocity = transform.forward * _speed;
    }
    
    //Used both onCollision and OnTrigger for handle not colliding objects in scene
    private void OnCollisionEnter()
    {
        Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter()
    {
        Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator destroyOneSec() {
        yield return new WaitForSecondsRealtime(1); //Wait 1 second
        Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
