using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
	[SerializeField] protected int _maxInventorySize;
	[SerializeField] protected int _emptyInventorySlot;

	[SerializeField] private GameObject _inventoryUIObject;
	[SerializeField] private ItemRecord _itemRecord;

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

	public virtual void ItemUse(int id)
	{
	}

	protected virtual void ItemDelete(int id)
	{
		_itemList.RemoveAt(id);
		_inventoryUi.UpdateInventoryUI(_itemList);
	}

	protected virtual void ItemSwap(int firstId, int secondId)
	{
		if (firstId != secondId)
		{
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
