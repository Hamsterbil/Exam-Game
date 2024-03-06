using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class UpgradeCardGeneration : MonoBehaviour
{
    public GameObject UpgradePrefab;
    public string pathToJson;

    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText(Path.Combine(Application.dataPath, pathToJson));

        JArray upgradeArray = JArray.Parse(json);

        foreach (JObject upgrade in upgradeArray)
        {
            string title = upgrade.GetValue("name").ToString();
            string description = upgrade.GetValue("description").ToString();
            int price = (int)upgrade.GetValue("price");
            int priceIncrease = (int)upgrade.GetValue("priceIncrease");
            // string[] upgradeTypes = new string[()];
            List<string> upgradeTypes = new List<string>();
            foreach (var upgradeType in upgrade.GetValue("affect"))
            {
                upgradeTypes.Add(upgradeType.ToString());
            }
        
            GameObject newUpgrade = Instantiate(UpgradePrefab, transform);
            newUpgrade.name = title;
            newUpgrade.GetComponent<UpgradeCard>().setValues(title, description, price, priceIncrease, upgradeTypes);
        }
    }
}
