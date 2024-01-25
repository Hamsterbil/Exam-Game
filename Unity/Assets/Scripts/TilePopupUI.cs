using UnityEngine;
using UnityEngine.UI;

public class TilePopupUI : MonoBehaviour
{
    public Image image;
    public Text nameText;
    public Text costText;

    void Start()
    {
        image.enabled = false;
    }

    // Display the popup with tile information
    public void ShowPopup(HexTile hexTile)
    {
        image.enabled = true;


        nameText.text = hexTile.name;
        costText.text = "Cost: " + hexTile.cost.ToString();
        // incomeText.text = "Income: " + hexTile.Income().ToString();

        // Set the popup position based on the hexTile position
        Vector3 popupPosition = Camera.main.WorldToScreenPoint(hexTile.transform.position);
        transform.position = popupPosition;
    }

    // Hide the popup
    public void HidePopup()
    {
        image.enabled = false;
    }

    public void UpdatePopupPosition(Vector3 mousePosition)
    {
        transform.position = mousePosition;
    }
}
