using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject gameOverScreen;
    public GameObject orbCounterScreen;

    private void Awake()
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

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AllOrbsCollected()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        GameObject door = GameObject.FindGameObjectWithTag("Door");
        if (door != null)
        {
            door.SetActive(false);
        }
        else
        {
            Debug.Log("Door not found!");
        }
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("no más niveles");
            ShowGameOverScreen();
        }
    }

    private void ShowGameOverScreen()
    {
        if (gameOverScreen != null && orbCounterScreen != null)
        {
            orbCounterScreen.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }

}
