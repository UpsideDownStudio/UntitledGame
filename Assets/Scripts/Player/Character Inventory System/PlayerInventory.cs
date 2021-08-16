using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class PlayerInventory : Inventory
{
	public const int UsableItemSlot = 4;
	public const int WeaponItemSlot = 2;
	public event Action<List<ItemRecord>> OnWeaponItemSwitched;

	[SerializeField] private List<ItemRecord> _weaponItemList;
	[SerializeField] private List<ItemRecord> _consumableItemList;
	[SerializeField] private CharacterStats _characterStats;
	
	protected override void Awake()
	{
		base.Awake();

		_characterStats = GetComponent<CharacterStats>();

		_inventoryUi.UpdateInventoryUI(_itemList);
		((PlayerInventoryUI)_inventoryUi).UpdateUsableUI(_weaponItemList, TypeOfItems.Weapon);
		((PlayerInventoryUI)_inventoryUi).UpdateUsableUI(_consumableItemList, TypeOfItems.Buffs);
		_emptyInventorySlot = FindEmptyInventorySlot();
	}

	//Можно использовать только те предметы, что под типом Consumables(Buffs)
	public override void ItemUse(int id, bool isUsableSlot)
	{
		if (isUsableSlot)
		{
			_consumableItemList[id].CurrentStackValue -= 1;
			_characterStats.ModifyValue(_consumableItemList[id].Item);

			if (_consumableItemList[id].CurrentStackValue == 0)
			{
				DeleteItem(id, _consumableItemList, true);
			}
			else
			{
				((PlayerInventoryUI)_inventoryUi).UpdateUsableUI(_consumableItemList, TypeOfItems.Buffs);
			}
		}
		else
		{
			_itemList[id].CurrentStackValue -= 1;
			_characterStats.ModifyValue(_itemList[id].Item);

			if (_itemList[id].CurrentStackValue == 0)
			{
				DeleteItem(id, _itemList);
				_emptyInventorySlot = FindEmptyInventorySlot();
			}
			else
			{
				_inventoryUi.UpdateUIByItem(_itemList[id], id);
			}
		}
	}

	private void DeleteItem(int id, List<ItemRecord> itemList, bool isUsableSlot = false)
	{
		if (!isUsableSlot)
		{
			base.ItemDelete(id);
			_inventoryUi.UpdateInventoryUI(itemList);
		}
		else
		{
			itemList[id].Item = null;
			itemList[id].CurrentStackValue = 1;
			((PlayerInventoryUI)_inventoryUi).UpdateUsableUI(itemList, TypeOfItems.Buffs);
		}
	}

	public bool TryToAddItem(ItemSO newItem)
	{
		var index = -1;
		
		for (int i = 0; i < _itemList.Count; i++)
		{
			if (_itemList[i].Item != null && (_itemList[i].Item.Name == newItem.Name && _itemList[i].CurrentStackValue < newItem.MaxStackableValue))
			{
				index = i;
				break;
			}
		}

		if (index != -1)
		{
			if (_itemList[index].CurrentStackValue < newItem.MaxStackableValue)
			{
				_itemList[index].CurrentStackValue++;
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

		return false;
	}

	public void SwapUsable(int firstId, int secondId, bool firstIsUsableSlot, bool secondIsUsableSlot, TypeOfItems itemType)
	{
		List<ItemRecord> itemList = null;

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
		else if (firstIsUsableSlot && (_itemList[secondId] == null || _itemList[secondId].Item.TypeOfItems == itemType))
		{
			SwapUsable(firstId, secondId, itemList);
		}
		else if (secondIsUsableSlot && (_itemList[firstId] == null || _itemList[firstId].Item.TypeOfItems == itemType))
		{
			SwapUsable(secondId, firstId, itemList);
		}
		if(itemType == TypeOfItems.Weapon)
			OnWeaponItemSwitched?.Invoke(_weaponItemList);

		((PlayerInventoryUI)_inventoryUi).UpdateUsableUI(itemList, itemType);
	}

	private void SwapUsable(int firstId, int secondId, List<ItemRecord> itemList)
	{
		Debug.Log(itemList[firstId].Item?.Name);
		Debug.Log(_itemList[secondId].Item?.Name);

		SwapItem(firstId, secondId, itemList);

		_emptyInventorySlot = FindEmptyInventorySlot();
		_inventoryUi.UpdateUIByItem(_itemList[secondId], secondId);
	}

	protected override void InitializeInventory()
	{
		base.InitializeInventory();

		for (int i = 1; i < _inventoryItemParent.transform.childCount; i++)
		{
			for (int j = 0; j < _inventoryItemParent.transform.GetChild(i).childCount; j++)
			{
				var itemRecord = GetItemRecordPlayerObject(i, j);

				if (i == 1)
				{
					_weaponItemList.Add(itemRecord);
				}
				else
				{
					_consumableItemList.Add(itemRecord);
				}
			}
		}
	}
}
