using UnityEngine;

public class HazardRay : MonoBehaviour
{
    [Header("Settings")]
    public float invisibleDuration = 2f;
    public float warningDuration = 2f;
    public float damageDuration = 3f;
    public AudioClip RaySound;

    private Animator animator;
    private Collider2D hazardCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        hazardCollider = GetComponent<Collider2D>();
        hazardCollider.enabled = false;
        StartCoroutine(HazardCycle());
    }

    System.Collections.IEnumerator HazardCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(invisibleDuration);

            animator.SetTrigger("StartWarning");
            yield return new WaitForSeconds(warningDuration);

            animator.SetTrigger("StartDamage");
            hazardCollider.enabled = true;
            AudioManager.Instance.PlaySFX(RaySound);
            yield return new WaitForSeconds(damageDuration);

            AudioManager.Instance.StopSFX();
            animator.SetTrigger("StartInvisible");
            hazardCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}