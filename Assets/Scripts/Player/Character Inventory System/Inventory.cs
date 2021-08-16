using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
	[SerializeField] protected int _maxInventorySize;
	[SerializeField] protected int _emptyInventorySlot;

	[SerializeField] protected List<ItemRecord> _itemList;
	[SerializeField] protected ItemRecord _itemRecord;

	[SerializeField] protected GameObject _inventoryUIObject;
	[SerializeField] protected InventoryUI _inventoryUi;

	protected GameObject _inventoryItemParent;

	protected virtual void Awake()
	{
		_inventoryUi = _inventoryUIObject.transform.GetChild(0).GetComponent<InventoryUI>();
		_inventoryUi.Inventory = this;

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
		Debug.Log("ItemUSE");
	}

	protected virtual void ItemDelete(int id)
	{
		_itemList[id].Item = null;
		_itemList[id].CurrentStackValue = 1;
		_inventoryUi.UpdateInventoryUI(_itemList);
	}

	public virtual void ItemSwap(int firstId, int secondId)
	{
		Debug.Log(firstId);
		Debug.Log(secondId);

		if (firstId != secondId)
		{
			if ((_itemList[firstId].Item == _itemList[secondId].Item) && (_itemList[firstId].Item != null && _itemList[secondId].Item != null))
			{
				Debug.Log("Зашёл 1");
				if (_itemList[secondId].CurrentStackValue < _itemList[secondId].Item.MaxStackableValue)
				{
					Debug.Log("Зашёл 2");
					int value = _itemList[firstId].CurrentStackValue + _itemList[secondId].CurrentStackValue -
					            _itemList[firstId].Item.MaxStackableValue;

					if (value <= 0)
					{
						ItemDelete(secondId);
						_itemList[firstId].CurrentStackValue = value + _itemList[firstId].Item.MaxStackableValue;
					}
					else
					{
						_itemList[secondId].CurrentStackValue = value;
						_itemList[firstId].CurrentStackValue = _itemList[firstId].Item.MaxStackableValue;
					}
				}
			}
			
			SwapItem(firstId, secondId, _itemList);

			_inventoryUi.UpdateInventoryUI(_itemList);
			_emptyInventorySlot = FindEmptyInventorySlot();
		}
	}
	protected void SwapItem(int firstId, int secondId, List<ItemRecord> itemList)
	{
		var tmpItem = itemList[firstId].Item;
		var tmpItemCount = itemList[firstId].CurrentStackValue;

		itemList[firstId].Item = _itemList[secondId].Item;
		itemList[firstId].CurrentStackValue = _itemList[secondId].CurrentStackValue;

		_itemList[secondId].Item = tmpItem;
		_itemList[secondId].CurrentStackValue = tmpItemCount;
	}

	protected virtual void InitializeInventory()
	{
		_inventoryItemParent = transform.GetChild(0).gameObject;

		for (int i = 0; i < _maxInventorySize; i++)
		{
			var itemRecord = GetItemRecordPlayerObject(0, i);
			_itemList.Add(itemRecord);
		}
	}

	protected ItemRecord GetItemRecordPlayerObject(int firstChild, int secondChild)
	{
		return _inventoryItemParent.transform.GetChild(firstChild).GetChild(secondChild).GetComponent<ItemRecord>();
	}
}
