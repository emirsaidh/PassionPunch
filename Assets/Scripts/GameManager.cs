using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;


    public static GameManager Instance => _instance;



    [SerializeField] private GameObject menuPanel;
    public bool oneSecBullet = false;
    public bool bigBullet = false;
    public bool redBullet = false;
    public bool _isMenuActive = false;
    [SerializeField] private Button oneSecButton;
    [SerializeField] private Button bigBulletButton;
    [SerializeField] private Button redBulletButton;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!_isMenuActive)
            {
                _isMenuActive = true;
                OpenMenu();    
            }
            else
            {
                _isMenuActive = false;
               CloseMenu(); 
            }
        }
    }

    private void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void CloseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OneSecButtonClicked()
    {
        if (!oneSecBullet)
        {
            oneSecButton.GetComponent<Image>().color = Color.green;
            oneSecBullet = true;
        }
        else
        {
            oneSecButton.GetComponent<Image>().color = Color.red;
            oneSecBullet = false;
        }
        
    }
    
    public void BigBulletButtonClicked()
    {
        if (!bigBullet)
        {
            bigBulletButton.GetComponent<Image>().color = Color.green;
            bigBullet = true;
        }
        else
        {
            bigBulletButton.GetComponent<Image>().color = Color.red;
            bigBullet = false;
        }
    }
    
    public void RedBulletButtonClicked()
    {
        if (!redBullet)
        {
            redBulletButton.GetComponent<Image>().color = Color.green;
            redBullet = true;
        }
        else
        {
            redBulletButton.GetComponent<Image>().color = Color.red;
            redBullet = false;
        }
    }
    
    
}
