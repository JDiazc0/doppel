using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // Stop time
        Debug.Log("Game Paused");
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume time
        Debug.Log("Game Resumed");
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}