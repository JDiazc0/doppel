using UnityEngine;

public class NextLevelController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entrando akiii");
            GameManager.Instance.LoadNextLevel();
        }

    }
}
