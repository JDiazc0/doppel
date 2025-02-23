using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    public static NextLevelController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public UnityEvent OnLoadNextLevel = new UnityEvent();
    public UnityEvent OnNoMoreLevels = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Check if there are more levels
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Invoke the event to load the next level
                OnLoadNextLevel.Invoke();
            }
            else
            {
                // Invoke the event to handle no more levels
                OnNoMoreLevels.Invoke();
            }
        }
    }
}