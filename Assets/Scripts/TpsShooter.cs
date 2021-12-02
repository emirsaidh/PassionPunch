using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TpsShooter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform pfBigBullet;
    [SerializeField] private Transform[] bulletSpawnPoints = new Transform [5];
    [SerializeField] private Text numberOfBulletsText;

    private ThirdPersonController _thirdPersonController;
    private StarterAssetsInputs _starterAssetsInputs;
    private Animator _animator;
    private int _numberOfBullets;
    private readonly Vector3[] _randomPelletDirections = new Vector3 [5];
    [SerializeField] private float pelletInaccuracy = 0.2f;
    

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
        if (Camera.main is { })
        {
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                mouseWorldPosition = raycastHit.point;
            }
        }

        if (_starterAssetsInputs.aim && !GameManager.Instance.isMenuActive)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(aimSensitivity);
            _thirdPersonController.SetRotateOnMove(false);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            

            Vector3 worldAimTarget = mouseWorldPosition;
            var transform1 = transform;
            var position = transform1.position;
            worldAimTarget.y = position.y;
            Vector3 aimDirection = (worldAimTarget - position).normalized;
            transform.forward = Vector3.Lerp(transform1.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(normalSensitivity);
            _thirdPersonController.SetRotateOnMove(true);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
        if (_starterAssetsInputs.shoot && !GameManager.Instance.isMenuActive)
        {
            _numberOfBullets++;
            numberOfBulletsText.text =
                "BULLETS \n Total: " + _numberOfBullets * 5 + "\n Last Shoot \n (Pellets): 5";
            Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoints[0].position).normalized;
            GenerateRandomPellets(aimDir);
            for (int i = 0; i < _randomPelletDirections.Length; i++)
            {
                Instantiate(GameManager.Instance.bigBullet ? pfBigBullet : pfBulletProjectile, bulletSpawnPoints[i].position,
                    Quaternion.LookRotation(_randomPelletDirections[i], Vector3.up));
            }
            _starterAssetsInputs.shoot = false;

        }
    }
    private void GenerateRandomPellets(Vector3 aimDir)
    {
        for (int i = 0; i < _randomPelletDirections.Length; i++)
        {
            var position = new Vector3( aimDir.x + Random.Range(-pelletInaccuracy,pelletInaccuracy),aimDir.y + Random.Range(-pelletInaccuracy,pelletInaccuracy),
                aimDir.z + Random.Range(-pelletInaccuracy,pelletInaccuracy)).normalized;
            _randomPelletDirections[i] = position;
        }
            
    }
    
}
