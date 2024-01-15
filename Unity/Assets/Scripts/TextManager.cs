using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text cashText;
    public Text populationText;
    public Text militaryText;
    public Text happinessText;

    public Player player;

    // Update is called once per frame
    void Update()
    {
        cashText.text = "" + player.cash;
        populationText.text = "" + player.population;
        happinessText.text = "" + player.happiness + "%";
        militaryText.text = "" + player.military;
    }
}
