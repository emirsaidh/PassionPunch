using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private GameObject menuPanel;
    public bool oneSecBullet;
    public bool bigBullet;
    public bool redBullet;
    [FormerlySerializedAs("_isMenuActive")] public bool isMenuActive;
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
            if (!isMenuActive)
            {
                isMenuActive = true;
                OpenMenu();    
            }
            else
            {
                isMenuActive = false;
               CloseMenu(); 
            }
        }
    }

    private void OpenMenu()
    {
        //TimeScale=0 only stops physics engine so we need to change cursor state and keep a boolean for
        //isMenu active to check and stop instantiating bullets when menu is active
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
