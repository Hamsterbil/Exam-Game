using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public Text cashText;
    public Text populationText;
    public Text militaryText;
    public Text happinessText;
    public TMP_Text cashUpgradeText1;
    public TMP_Text cashUpgradeText2;
    public UpgradeManager upgradeManager;


    public Player player;

    // Update is called once per frame
    void Update()
    {
        cashText.text = "" + player.cash;
        populationText.text = "" + player.population;
        happinessText.text = "" + player.happiness + "%";
        militaryText.text = "" + player.military;
        cashUpgradeText1.text = "Cost: " + upgradeManager.actualUpgradePrice;
        cashUpgradeText2.text = "Cost: " + upgradeManager.actualUpgradePrice;
}




}