using System.Collections.Generic;
using UnityEngine;

public class ItemShopUI : MonoBehaviour
{
    [SerializeField] private ItemShopSlot _slotPrefab;
    private ItemShopPresenter _presenter;
    private List<ItemShopSlot> _slots = new List<ItemShopSlot>();

    private void Awake()
    {
        _presenter = new ItemShopPresenter();

        foreach (var id in _presenter.itemShopSource)
        {
            ItemShopSlot slot = Instantiate<ItemShopSlot>(_slotPrefab);
            slot.id = id;
            _slots.Add(slot);
        }

        _presenter.itemShopSource.onItemChanged += (slotIndex, id) => _slots[slotIndex].id = id;

    }
}