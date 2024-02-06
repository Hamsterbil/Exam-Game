using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class UpgradeCard : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text price;
    public List<string> upgradeTypes;
    public int priceIncrease;
    public Image icon;
    public Button button;
    public UpgradeManager upgradeManager;

    void Start()
    {
        button.onClick.AddListener(OnClick);
        upgradeManager = GameObject.Find("Upgrades").GetComponent<UpgradeManager>();
    }

    public void setValues(
        string title,
        string description,
        int price,
        int priceIncrease,
        List<string> upgradeTypes
    )
    {
        this.title.text = title;
        // this.description.text = description;
        this.price.text = "Price: " + price.ToString();
        this.priceIncrease = priceIncrease;
        this.upgradeTypes = upgradeTypes;
        this.icon.sprite = Resources.Load<Sprite>("Upgrades/" + title);
    }

    public void OnClick()
    {
        //switch based on upgradeTypes
        foreach (string upgradeType in upgradeTypes)
        {
            switch (upgradeType)
            {
                case "cash":
                Debug.Log("cash upgrade");
                    upgradeManager.ApplyCashUpgrade(title.text + "," + priceIncrease);
                    break;
                case "population":
                    upgradeManager.ApplyPopulationUpgrade(priceIncrease);
                    break;
                case "military":
                    upgradeManager.ApplyMilitaryUpgrade(priceIncrease);
                    break;
                case "militaryMax":
                    // upgradeManager.ApplyMilitaryMaxUpgrade(priceIncrease);
                    break;

                default:
                    Debug.LogWarning("Upgrade type not found: " + upgradeType);
                    break;
            }
        }
    }
}
