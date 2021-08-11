using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class PlayerInventory : Inventory
{
	public const int UsableItemSlot = 4;
	public event Action<ItemRecord> WeaponItemSwitched;

	[SerializeField] private List<ItemSO> _weaponItemList;
	[SerializeField] private List<ItemSO> _consumableItemList;
	[SerializeField] private CharacterStats _characterStats;
	
	protected override void Start()
	{
		base.Start();
		ItemUI.OnUsableSwitched += SwapUsable;

		_characterStats = GetComponent<CharacterStats>();
		_inventoryUi = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventoryUI>();

		_inventoryUi.UpdateInventoryUI(_itemList);
		((PlayerInventoryUI)_inventoryUi).UpdateUsableUI(_weaponItemList, TypeOfItems.Weapon);
		((PlayerInventoryUI)_inventoryUi).UpdateUsableUI(_consumableItemList, TypeOfItems.Buffs);

		_emptyInventorySlot = FindEmptyInventorySlot();
	}

	//Можно использовать только те предметы, что под типом Consumables(Buffs)
	public override void ItemUse(int id)
	{
		if (_itemList[id].Item.TypeOfItems == TypeOfItems.Buffs)
		{
			_itemList[id].currentStackValue -= 1;
			_characterStats.ModifyValue(_itemList[id].Item);

			if (_itemList[id].currentStackValue == 0)
			{
				DeleteItem(id);
				_emptyInventorySlot = FindEmptyInventorySlot();
			}
			else
			{
				_inventoryUi.UpdateUIByItem(_itemList[id], id);
			}
		}
	}

	private void DeleteItem(int id)
	{
		_itemList[id].Item = null;
		_emptyInventorySlot = FindEmptyInventorySlot();
		_inventoryUi.UpdateInventoryUI(_itemList);
	}

	public bool TryToAddItem(ItemSO newItem)
	{
		var index = -1;
		
			for (int i = 0; i < _itemList.Count; i++)
			{
				if (_itemList[i].Item == newItem && _itemList[i].currentStackValue < newItem.MaxStackableValue)
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

		if (_emptyInventorySlot != -1)
		{
			_itemList[_emptyInventorySlot].Item = newItem;
			_inventoryUi.UpdateUIByItem(_itemList[_emptyInventorySlot], _emptyInventorySlot, true);
			_emptyInventorySlot = FindEmptyInventorySlot();
			return true;
		}

		Debug.Log("False");
		return false;
	}

	private void SwapUsable(int firstId, int secondId, bool firstIsUsableSlot, bool secondIsUsableSlot, TypeOfItems itemType)
	{
		List<ItemSO> itemList = null;

		switch (itemType)
		{
			case TypeOfItems.Weapon:
				itemList = _weaponItemList;
				break;

			case TypeOfItems.Buffs:
				itemList = _consumableItemList;
				break;
		}

		if (firstIsUsableSlot && secondIsUsableSlot)
		{
			var tmpItem = itemList[secondId];
			itemList[secondId] = itemList[firstId];
			itemList[firstId] = tmpItem;
		}
		else if (firstIsUsableSlot && _itemList[secondId].Item.TypeOfItems == itemType)
		{
			SwapUsable(firstId, secondId, itemList);
		}
		else if (secondIsUsableSlot && _itemList[firstId].Item.TypeOfItems == itemType)
		{
			SwapUsable(secondId, firstId, itemList);
		}

		((PlayerInventoryUI)_inventoryUi).UpdateUsableUI(itemList, itemType);
	}

	private void SwapUsable(int firstId, int secondId, List<ItemSO> itemList)
	{
		var tmpItem = itemList[firstId];
		itemList[firstId] = _itemList[secondId].Item;
		var newItem = new ItemRecord(tmpItem);
		_itemList[secondId] = newItem;
		_inventoryUi.UpdateUIByItem(newItem, secondId);

		
	}
}
