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

        JArray UpgradeArray = JArray.Parse(json);

        foreach (JObject Upgrade in UpgradeArray)
        {
            string title = Upgrade.GetValue("name").ToString();
            string description = Upgrade.GetValue("description").ToString();
            int price = (int)Upgrade.GetValue("price");
            int priceIncrease = (int)Upgrade.GetValue("priceIncrease");
            
            List<string> upgradeTypes = new List<string>();
            foreach (var upgradeType in Upgrade.GetValue("affect"))
            {
                upgradeTypes.Add(upgradeType.ToString());
            }
        
            GameObject newUpgrade = Instantiate(UpgradePrefab, transform);
            newUpgrade.name = title;
            newUpgrade.GetComponent<UpgradeCard>().setValues(title, description, price, priceIncrease, upgradeTypes);
        }
    }
}
