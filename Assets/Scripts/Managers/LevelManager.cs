using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        GameObject[] orbs = GameObject.FindGameObjectsWithTag("Orb");

        OrbManager.Instance.setTotalOrbs(orbs.Length);
    }
}
