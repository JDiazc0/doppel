using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OrbManager : MonoBehaviour
{
    public static OrbManager Instance { get; private set; }


    [SerializeField] TextMeshProUGUI orbText;
    private int totalOrbs;
    private int orbsCollected;

    public UnityEvent OnAllOrbsCollected = new UnityEvent();

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
            OnAllOrbsCollected.Invoke();
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
