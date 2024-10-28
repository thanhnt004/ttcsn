using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Image totalHealBar;
    [SerializeField] private Image currentHealBar;
    [SerializeField] private Health playerHeal;
    void Awake()
    {
        totalHealBar.fillAmount = playerHeal.heal/10;
        Debug.Log(playerHeal.heal);
        Debug.Log(playerHeal.heal/10);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealBar.fillAmount = playerHeal.heal/10;
    }
}
