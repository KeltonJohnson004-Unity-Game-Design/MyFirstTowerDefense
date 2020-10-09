using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Information : MonoBehaviour
{

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI livesText;

    void Update()
    {
        moneyText.text = "$ " + PlayerStats.Money.ToString();
        roundText.text = "Round: " + (PlayerStats.Rounds + 1).ToString();
        livesText.text = "Lives: " + PlayerStats.Lives.ToString();
    }
}
