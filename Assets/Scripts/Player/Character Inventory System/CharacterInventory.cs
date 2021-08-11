using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
	public event Action<List<ItemRecord>> UpdateInventoryUIAll;
	public event Action<ItemRecord, int> UpdateInventoryUIByNewItem;
	public event Action<ItemRecord, int> UpdateInventoryUIByExistItem;
	public event Action<int> DeleteItemUI;

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
	}

	private void SwitchItems(int firstItemIndex, int secondItemIndex)
	{
		if (firstItemIndex != secondItemIndex)
		{
			var tmpItem = _itemSoList[firstItemIndex];
			_itemSoList[firstItemIndex] = _itemSoList[secondItemIndex];
			_itemSoList[secondItemIndex] = tmpItem;
			UpdateInventoryUIAll?.Invoke(_itemSoList);
		}
	}

	public bool TryToPickUpItem(ItemSO newItem)
	{
		var index = -1;
		for (int i = 0; i < _itemSoList.Count; i++)
		{
			if (_itemSoList[i].Item == newItem && _itemSoList[i].currentStackValue < newItem.MaxStackableValue)
			{
				index = i;
				break;
			}
		}

		if (index != -1)
		{
			if (_itemSoList[index].currentStackValue < newItem.MaxStackableValue)
			{
				_itemSoList[index].currentStackValue++;
				Debug.Log("Был добавлен предмет + " + newItem.Name + " " + _itemSoList[index].currentStackValue);
				UpdateInventoryUIByExistItem?.Invoke(_itemSoList[index], index);
				return true;
			}
		}
		
		if (_currentInventorySize != _maxInventorySize)
		{
			var itemRecord = new ItemRecord(newItem);
			_itemSoList.Add(itemRecord);
			_currentInventorySize++;
			UpdateInventoryUIByNewItem?.Invoke(itemRecord, _currentInventorySize - 1);

			return true;
		}

		return false;
	}

	private void UseItem(int index, bool isUsable)
	{
		_itemSoList[index].currentStackValue -= 1;
		_characterStats.ModifyValue(_itemSoList[index].Item);

		if (_itemSoList[index].currentStackValue == 0)
		{
			_itemSoList.RemoveAt(index);
			_currentInventorySize--;
			UpdateInventoryUIAll?.Invoke(_itemSoList);
		}
		else
		{
			UpdateInventoryUIByExistItem?.Invoke(_itemSoList[index], index);
		}
	}
}
