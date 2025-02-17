using TMPro;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public static OrbManager Instance { get; private set; }


    [SerializeField] TextMeshProUGUI orbText;
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
        UpdateOrbText();
    }

    public void OrbCollected()
    {
        orbsCollected++;
        UpdateOrbText();
        if (orbsCollected >= totalOrbs)
        {
            GameManager.Instance.AllOrbsCollected();
        }
    }

    public void UpdateOrbText()
    {
        if (orbText != null)
        {
            orbText.text = $"{orbsCollected} / {totalOrbs}";
        }
    }
}
