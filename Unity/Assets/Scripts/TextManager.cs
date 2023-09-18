using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text moneyText;
    public Text populationText;
    public Text militaryText;
    public Text happinessText;

    public Player player;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "" + player.money;
        populationText.text = "" + player.population;
        happinessText.text = "" + player.happiness;
        militaryText.text = "" + player.military;
    }
}
