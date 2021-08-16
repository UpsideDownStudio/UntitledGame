using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] private string _inventoryName;
	
	[SerializeField] private GameObject _itemUiPrefab;
	[SerializeField] protected ItemRecord _itemRecord;
	[SerializeField] protected List<ItemUI> _itemUiList;

	[SerializeField] protected Inventory _inventory;
	
	public Inventory Inventory
	{
		get
		{
			return _inventory;
		}
		set
		{
			_inventory = value;
		}
	}

	protected virtual void Start()
	{
		Debug.Log("Вызов");
		GetItemsUI();
		SubscribeItemsUI(_itemUiList);
	}

	protected virtual void SubscribeItemsUI(List<ItemUI> listUi)
	{
		for (int i = 0; i < listUi.Count; i++)
		{
			listUi[i].OnItemClicked += _inventory.ItemUse;
			listUi[i].OnItemsSwitched += _inventory.ItemSwap;
		}
	}

	protected virtual void GetItemsUI()
	{
		Debug.Log("Добавление ItemUI");
		for (int i = 0; i < transform.childCount; i++)
		{
			_itemUiList.Add(transform.GetChild(i).GetComponent<ItemUI>());
		}

		if (_itemUiList.Count == 0)
			return;
	}

	public virtual void UpdateInventoryUI(List<ItemRecord> itemList)
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			UpdateUIByNewItem(itemList[i], i);
		}
	}

	public void UpdateUIByItem(ItemRecord item, int id, bool isNewItem = false)
	{
		if (isNewItem)
			UpdateUIByNewItem(item, id);
		else
			UpdateUIByExistItem(item, id);
	}

	protected void UpdateUIByNewItem(ItemRecord itemRecord, int index)
	{
		var itemInfo = transform.GetChild(index).GetComponent<ItemUI>();
		itemInfo.ConfigureItemUI(itemRecord, index);
	}

	protected void UpdateUIByExistItem(ItemRecord itemRecord, int index)
	{
		var item = transform.GetChild(index);
		var itemInfo = item.GetComponent<ItemUI>();
		itemInfo.ConfigureItemUI(itemRecord, index);
	}
}
