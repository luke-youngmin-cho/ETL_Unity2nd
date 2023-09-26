using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemShopSlot : MonoBehaviour
{
    public int id
    {
        get => _id;
        set
        {
            // todo -> get item data with id.
            // todo -> set price, name text & icon image with item data.
        }
    }
    private int _id;
    public int index;
    
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
}