using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StorageInventory : Inventory
{
	[SerializeField] private List<ItemSO> itemListSpawn;

	protected override void Awake()
	{
		base.Awake();
		GenerateItem();
		_inventoryUi.UpdateInventoryUI(_itemList);
	}

	public void GenerateItem()
	{
		for (int i = 0; i < 8; i++)
		{
			var item = itemListSpawn[UnityEngine.Random.Range(0, itemListSpawn.Count)];
			_itemList[i].Item = item;
		}
	}
}
