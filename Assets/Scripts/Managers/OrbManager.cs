using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public static OrbManager Instance { get; private set; }

    private int totalOrbs;
    private int orbsCollected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setTotalOrbs(int total)
    {
        totalOrbs = total;
        orbsCollected = 0;
    }

    public void OrbCollected()
    {
        orbsCollected++;
        if (orbsCollected >= totalOrbs)
        {
            GameManager.Instance.AllOrbsCollected();
        }
    }
}
