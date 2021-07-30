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
	[SerializeField] private List<ItemRecord> _consumableItemList;
	[SerializeField] private CharacterStats _characterStats;


	protected override void Start()
	{
		base.Start();
		ItemUI.OnWeaponSwitched += SwapWeapon;

		_characterStats = GetComponent<CharacterStats>();
		_inventoryUi = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventoryUI>();
	}

	//Можно использовать только те предметы, что под типом Consumables(Buffs)
	public override void ItemUse(int id)
	{
		if (_itemList[id].GetItem().TypeOfItems == TypeOfItems.Buffs)
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

			return true;
		}

		return false;
	}

	private void SwapWeapon(int firstId, int secondId, bool firstIsWeaponSlot, bool secondIsWeaponSlot)
	{
		if (firstIsWeaponSlot && secondIsWeaponSlot)
		{
			var tmpWeapon = _weaponItemList[secondId];
			_weaponItemList[secondId] = _weaponItemList[firstId];
			_weaponItemList[firstId] = tmpWeapon;
		}
		else if (firstIsWeaponSlot && _itemList[secondId].GetItem().TypeOfItems == TypeOfItems.Weapon)
		{
			SwapWeapon(firstId, secondId);
		}
		else if (secondIsWeaponSlot && _itemList[firstId].GetItem().TypeOfItems == TypeOfItems.Weapon)
		{
			SwapWeapon(secondId, firstId);
		}
	}

	private void SwapWeapon(int firstId, int secondId)
	{
		var tmpWeapon = _weaponItemList[firstId];
		_weaponItemList[firstId] = _itemList[secondId].GetItem();
		_itemList[secondId] = new ItemRecord(tmpWeapon);
	}

	private void SwapConsumables(int firstId, int secondId)
	{
		//смена используемых предметов
		if (_itemList[firstId].GetItem().TypeOfItems == TypeOfItems.Buffs)
			base.ItemSwap(firstId, secondId);
	}
}
