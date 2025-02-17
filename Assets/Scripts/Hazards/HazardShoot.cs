using UnityEngine;

public class HazardShoot : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 3f;
    public float lifeTime = 5f;
    public LayerMask groundLayer;

    private float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        // Check if the ElectroShoot hits the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit player!");
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }

        // Check if the ElectroShoot hits the ground
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Debug.Log("Hit ground!");
            Destroy(gameObject);
        }
    }
}
