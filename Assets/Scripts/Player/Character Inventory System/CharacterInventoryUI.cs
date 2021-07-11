using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterInventoryUI : MonoBehaviour
{
	[SerializeField] private CharacterInventory _characterInventory;
	[SerializeField] private GameObject _itemUiPrefab;
	

	private void Start()
	{
		_characterInventory.UpdateInventoryUIByItem += UpdateUIByItem;
		_characterInventory.UpdateInventoryUIAll += sos => { };
	}

	private void UpdateUIByItem(ItemSO newItem, int index)
	{
		var item = Instantiate(_itemUiPrefab, transform);
		var itemInfo = item.GetComponent<ItemUI>();
		itemInfo._itemIndex = index;

	}
}
