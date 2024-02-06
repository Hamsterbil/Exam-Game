using UnityEngine;
using UnityEngine.UI;

public class TilePopupUI : MonoBehaviour
{
    public Image image;
    public Text nameText;
    public Text costText;
    public bool updatePosition;

    void Start()
    {
        
    }

    void Update()
    {
        if (updatePosition)
        {
            transform.position = Input.mousePosition;
        }
    }

    // Display the popup with tile information
    public void ShowPopup(HexTile hexTile)
    {
        image.enabled = true;
        nameText.enabled = true;
        costText.enabled = true;
        updatePosition = true;

        nameText.text = hexTile.name;
        costText.text = "Cost: " + hexTile.cost.ToString();
    }

    // Hide the popup
    public void HidePopup()
    {
        image.enabled = false;
        nameText.enabled = false;
        costText.enabled = false;
        updatePosition = false;
    }
}
