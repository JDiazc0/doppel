using System.Collections;
using UnityEngine;

public class CatapultMechanic : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public GameObject rayPrefab; // Single prefab for the ray

    [Header("Settings")]
    public float raySpeed = 1f;
    public float maxRayDistance = 5f;
    public float rotationSpeed = 45f;
    public float impulseMultiplier = 10f;
    public float unfreezeDelay = 1.5f;

    private GameObject rayInstance;
    private float rayDistance = 1f;
    private float rayAngle = 0f;
    private bool isActive = false;

    void Start()
    {
        rayInstance = Instantiate(rayPrefab, playerController.transform.position, Quaternion.identity);
        rayInstance.SetActive(false);
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rayAngle += rotationSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rayAngle -= rotationSpeed * Time.deltaTime;
            }

            rayAngle = Mathf.Clamp(rayAngle, -55f, 55f);

            rayDistance += raySpeed * Time.deltaTime;
            rayDistance = Mathf.Clamp(rayDistance, 1f, maxRayDistance);

            UpdateRay();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Activate();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Deactivate();
        }
    }

    void Activate()
    {
        isActive = true;
        rayInstance.SetActive(true);
        playerController.FreezePlayer(true);
    }

    void Deactivate()
    {
        isActive = false;
        rayInstance.SetActive(false);
        ApplyImpulse();

        rayDistance = 1f;
        rayAngle = 0f;

        StartCoroutine(UnfreezePlayer());
    }

    IEnumerator UnfreezePlayer()
    {
        yield return new WaitForSeconds(unfreezeDelay);
        playerController.FreezePlayer(false);
    }

    void UpdateRay()
    {
        Vector2 rayDirection = Quaternion.Euler(0, 0, rayAngle) * Vector2.up;
        Vector2 rayEndPosition = (Vector2)playerController.transform.position + rayDirection * rayDistance;

        rayInstance.transform.position = (Vector2)playerController.transform.position + rayDirection * (rayDistance / 2f);
        rayInstance.transform.localScale = new Vector3(1, rayDistance, 1);
        rayInstance.transform.rotation = Quaternion.Euler(0, 0, rayAngle);
    }

    void ApplyImpulse()
    {
        Vector2 impulseDirection = Quaternion.Euler(0, 0, rayAngle) * Vector2.up;
        impulseDirection.Normalize();

        float impulseForce = rayDistance * impulseMultiplier;

        Rigidbody2D playerRb = playerController.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.linearVelocity = impulseDirection * impulseForce;
        }
    }
}