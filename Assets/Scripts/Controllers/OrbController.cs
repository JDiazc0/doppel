using UnityEngine;

public class OrbController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OrbManager.Instance.OrbCollected();
            Destroy(gameObject);
        }

    }
}
