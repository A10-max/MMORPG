using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Player player;
    public Image healthbar;

    public void UpdatePlayerHealthBars()
    {
        FindAnyObjectByType<Player>();
        healthbar.fillAmount = player.currentHealth.Value / player.maxHealth;
    }
}
