using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class PlayerInventory : Inventory
{
	public event Action<ItemRecord> WeaponItemSwitched;

	[SerializeField] private List<ItemRecord> _weaponItemList;
	[SerializeField] private List<ItemRecord> _consumableItemList;
	[SerializeField] private CharacterStats _characterStats;

	protected override void Start()
	{
		base.Start();

		_characterStats = GetComponent<CharacterStats>();
		_inventoryUi = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<InventoryUI>();
	}

	public override void ItemUse(int id)
	{
		_itemList[id].currentStackValue -= 1;
		_characterStats.ModifyValue(_itemList[id].GetItem());

		if (_itemList[id].currentStackValue == 0)
		{
			_itemList.RemoveAt(id);
			_currentInventorySize--;
			_inventoryUi.UpdateAllUI(_itemList);
		}
		else
		{
			_inventoryUi.UpdateUIByItem(_itemList[id], id);
		}
	}

	public bool TryToAddItem(ItemSO newItem)
	{
		var index = -1;
		for (int i = 0; i < _itemList.Count; i++)
		{
			if (_itemList[i].GetItem() == newItem && _itemList[i].currentStackValue < newItem.MaxStackableValue)
			{
				index = i;
				break;
			}
		}

		if (index != -1)
		{
			if (_itemList[index].currentStackValue < newItem.MaxStackableValue)
			{
				_itemList[index].currentStackValue++;
				Debug.Log("Был добавлен предмет + " + newItem.Name + " " + _itemList[index].currentStackValue);
				_inventoryUi.UpdateUIByItem(_itemList[index], index);
				return true;
			}
		}

		if (_currentInventorySize != _maxInventorySize)
		{
			var itemRecord = new ItemRecord(newItem);
			_itemList.Add(itemRecord);
			_currentInventorySize++;
			_inventoryUi.UpdateUIByItem(itemRecord, _currentInventorySize - 1, true);
			//UpdateInventoryUIByNewItem?.Invoke(itemRecord, _currentInventorySize - 1);

			return true;
		}

		return false;
	}

	protected override void ItemSwap(int firstId, int secondId)
	{
		base.ItemSwap(firstId, secondId);
	}

	private void SwapWeapon(int firstId, int secondId)
	{
		//TODO смена оружия
		base.ItemSwap(firstId, secondId);
	}

	private void SwapConsumables(int firstId, int secondId)
	{
		//TODO смена используемых предметов
		base.ItemSwap(firstId, secondId);
	}
}
