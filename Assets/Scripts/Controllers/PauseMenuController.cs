using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance { get; private set; }

    public GameObject menu; // Referencia al panel del menú de pausa
    private bool inMenu = false;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        inMenu = !inMenu;
        menu.SetActive(inMenu);
        Time.timeScale = inMenu ? 0 : 1;
    }

    public void ResumeGame()
    {
        TogglePauseMenu();
    }
}