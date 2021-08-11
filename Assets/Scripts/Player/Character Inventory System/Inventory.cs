using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
	[SerializeField] protected int _maxInventorySize;
	[SerializeField] protected int _emptyInventorySlot;

	[SerializeField] private GameObject _inventoryUIObject;
	[SerializeField] protected ItemRecord _itemRecord;

	[SerializeField] protected InventoryUI _inventoryUi;
	[SerializeField] protected List<ItemRecord> _itemList;


	protected virtual void Start()
	{
		ItemUI.OnItemClicked += ItemUse;
		ItemUI.OnItemsSwitched += ItemSwap;

		InitializeInventory();
		_emptyInventorySlot = FindEmptyInventorySlot();
	}
	protected int FindEmptyInventorySlot()
	{
		int emptySlot = _itemList.FindIndex(0, x => x.Item == null);
		return emptySlot;
	}

	private void Update()
	{
		UseInventory();
	}

	private void UseInventory()
	{
		if(Input.GetKeyDown(KeyCode.I))
			_inventoryUIObject.SetActive(!_inventoryUIObject.activeSelf);
	}

	public virtual void ItemUse(int id, bool isUsableSlot)
	{

	}

	protected virtual void ItemDelete(int id)
	{
		_itemList[id].Item = null;
		_inventoryUi.UpdateInventoryUI(_itemList);
	}

	protected virtual void ItemSwap(int firstId, int secondId)
	{
		if (firstId != secondId)
		{
			if (_itemList[firstId].Item == _itemList[secondId].Item)
			{
				if (_itemList[secondId].currentStackValue < _itemList[secondId].Item.MaxStackableValue)
				{
					int value = _itemList[firstId].currentStackValue + _itemList[secondId].currentStackValue -
					            _itemList[firstId].Item.MaxStackableValue;

					if (value <= 0)
					{
						ItemDelete(secondId);
						_itemList[firstId].currentStackValue = value + _itemList[firstId].Item.MaxStackableValue;
					}
					else
					{
						_itemList[secondId].currentStackValue = value;
						_itemList[firstId].currentStackValue = _itemList[firstId].Item.MaxStackableValue;
					}
				}
			}

			var tmpItem = _itemList[firstId];
			_itemList[firstId] = _itemList[secondId];
			_itemList[secondId] = tmpItem;

			_inventoryUi.UpdateInventoryUI(_itemList);
			_emptyInventorySlot = FindEmptyInventorySlot();
		}
	}

	protected virtual void InitializeInventory()
	{
		for (int i = 0; i < _maxInventorySize; i++)
		{
			var item = Instantiate(_itemRecord);
			_itemList.Add(item);
		}
	}
}
