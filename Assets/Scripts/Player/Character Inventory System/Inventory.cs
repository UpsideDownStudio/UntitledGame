using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
	[SerializeField] protected int _maxInventorySize;
	[SerializeField] protected int _currentInventorySize;
	
	[SerializeField] protected InventoryUI _inventoryUi;
	[SerializeField] protected List<ItemRecord> _itemList;

	protected virtual void Start()
	{
		ItemUI.OnItemClicked += ItemUse;
		ItemUI.OnItemsSwitched += ItemSwap;

		_currentInventorySize = _itemList.Count;
	}	

	public virtual void ItemUse(int id)
	{
	}

	protected virtual void ItemDelete(int id)
	{
		_itemList.RemoveAt(id);
		_inventoryUi.UpdateAllUI(_itemList);
	}

	protected virtual void ItemSwap(int firstId, int secondId)
	{
		if (firstId != secondId)
		{
			var tmpItem = _itemList[firstId];
			_itemList[firstId] = _itemList[secondId];
			_itemList[secondId] = tmpItem;
			_inventoryUi.UpdateAllUI(_itemList);
		}
	}

	protected virtual void InitializeInventoryUI()
	{

	}
}
