using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEditor.Experimental.GraphView;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TpsShooter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    //[SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform pfBigBullet;
    [SerializeField] private Transform[] bulletSpawnPoints = new Transform [5];
    [SerializeField] private Text numberOfBulletsText;

    private ThirdPersonController _thirdPersonController;
    private StarterAssetsInputs _starterAssetsInputs;
    private Animator _animator;
    private int _numberOfBullets;
    private int _lastShootBullets;
    private Vector3[] _randomPelletDirections = new Vector3 [5];
    private float _pelletInaccuracy = 0.2f;
    

    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            //debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        
        if (_starterAssetsInputs.aim && !GameManager.Instance._isMenuActive)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(aimSensitivity);
            _thirdPersonController.SetRotateOnMove(false);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(normalSensitivity);
            _thirdPersonController.SetRotateOnMove(true);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (_starterAssetsInputs.shoot && !GameManager.Instance._isMenuActive)
        {
            
            _numberOfBullets++;
            numberOfBulletsText.text =
                "BULLETS \n Total: " + _numberOfBullets * 5 + "\n Last Shoot \n (Pellets): 5";
            Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoints[0].position).normalized;
            //Vector3 testDir = new Vector3(1f, 0f, 2f);
            GenerateRandomPellets(aimDir);
            for (int i = 0; i < _randomPelletDirections.Length; i++)
            {
                Instantiate(GameManager.Instance.bigBullet ? pfBigBullet : pfBulletProjectile, bulletSpawnPoints[i].position,
                    Quaternion.LookRotation(_randomPelletDirections[i], Vector3.up));
            }
            
            
            _starterAssetsInputs.shoot = false;

        }
        
    }

    public void GenerateRandomPellets(Vector3 aimDir)
    {
        for (int i = 0; i < _randomPelletDirections.Length; i++)
        {
            var position = new Vector3( aimDir.x + Random.Range(-_pelletInaccuracy,_pelletInaccuracy),aimDir.y + Random.Range(-_pelletInaccuracy,_pelletInaccuracy),
                aimDir.z + Random.Range(-_pelletInaccuracy,_pelletInaccuracy)).normalized;
            //Random.Range(-_pelletInaccuracy,_pelletInaccuracy)
            _randomPelletDirections[i] = position;
        }
            
    }
    
}
