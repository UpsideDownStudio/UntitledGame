using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
	public event Action<List<ItemSO>> UpdateInventoryUIAll;
	public event Action<ItemSO, int> UpdateInventoryUIByItem;

	[SerializeField] private int _maxInventorySize;
	[SerializeField] private List<ItemRecord> _itemSoList;
	[SerializeField] private int _currentInventorySize;
	[SerializeField] private CharacterInventoryUI _characterInventoryUi;

	private CharacterStats _characterStats;
	
	private void Start()
	{
		_currentInventorySize = _itemSoList.Count;
		_characterStats = GetComponent<CharacterStats>();

		ItemUI.OnItemClicked += UseItem;
		ItemUI.OnItemsSwitched += SwitchItems;
	}

	private void SwitchItems(int firstItemIndex, int secondItemIndex)
	{
		if (firstItemIndex != secondItemIndex)
		{
			var tmpItem = _itemSoList[firstItemIndex];
			_itemSoList[firstItemIndex] = _itemSoList[secondItemIndex];
			_itemSoList[secondItemIndex] = tmpItem;
		}
	}

	public bool TryToPickUpItem(ItemSO newItem)
	{
		var index = _itemSoList.FindIndex(x => x.GetItem() == newItem);

		if (index != -1)
		{
			if (_itemSoList[index].currentStackValue < newItem.MaxStackableValue)
			{
				_itemSoList[index].currentStackValue++;
				Debug.Log("Был добавлен предмет + " + newItem.Name + " " + _itemSoList[index].currentStackValue);
				return true;
			}
		}
		
		if (_currentInventorySize != _maxInventorySize)
		{
			_itemSoList.Add(new ItemRecord(newItem));
			_currentInventorySize++;
			UpdateInventoryUIByItem?.Invoke(newItem, _currentInventorySize - 1);

			return true;
		}

		return false;
	}

	private void UseItem(int index)
	{
		_characterStats.ModifyValue(_itemSoList[index].GetItem());
	}
}
