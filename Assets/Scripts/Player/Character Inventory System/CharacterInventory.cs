using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
	public event Action<List<ItemSO>> UpdateInventoryUIAll;
	public event Action<ItemSO, int> UpdateInventoryUIByItem;

	[SerializeField] private int _maxInventorySize;
	[SerializeField] private List<ItemSO> _itemSoList;
	[SerializeField] private int _currentInventorySize;
	[SerializeField] private CharacterInventoryUI _characterInventoryUi;

	private CharacterStats _characterStats;
	
	private void Start()
	{
		_currentInventorySize = _itemSoList.Count;
		ItemUI.OnItemClicked += UseItem;
		_characterStats = GetComponent<CharacterStats>();
	}

	public bool TryToPickUpItem(ItemSO newItem)
	{
		if (_currentInventorySize != _maxInventorySize)
		{
			_itemSoList.Add(newItem);
			_currentInventorySize++;
			UpdateInventoryUIByItem?.Invoke(newItem, _currentInventorySize - 1);

			return true;
		}

		return false;
	}

	private void UseItem(int index)
	{
		_characterStats.ModifyValue(_itemSoList[index]);
	}
}
