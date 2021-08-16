using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerInventoryUI : InventoryUI
{
	[SerializeField] private List<ItemUI> _weaponItemUI;
	[SerializeField] private List<ItemUI> _consumableItemUI;

	protected override void Start()
	{
		Debug.Log("ֲûחמג Player");
		base.Start();

		UpdateUsableUI(_weaponItemUI, null);
		UpdateUsableUI(_consumableItemUI, null);

		SubscribeItemsUI(_weaponItemUI);
		SubscribeItemsUI(_consumableItemUI);
	}

	public void UpdateUsableUI(List<ItemRecord> itemList, TypeOfItems itemType)
	{
		switch (itemType)
		{
			case TypeOfItems.Weapon:
				UpdateUsableUI(_weaponItemUI, itemList);
				break;

			case TypeOfItems.Buffs:
				UpdateUsableUI(_consumableItemUI, itemList);
				break;
		}
	}

	private void UpdateUsableUI(List<ItemUI> itemUiList, List<ItemRecord> itemList)
	{
		for (int i = 0; i < itemUiList.Count; i++)
		{
			itemUiList[i].ConfigureItemUI(itemList != null ? itemList[i] : null, i);
		}
	}

	protected override void GetItemsUI()
	{
		base.GetItemsUI();

		var inventoryParent = transform.parent;
		for (int i = 1; i < inventoryParent.childCount; i++)
		{
			for (int j = 0; j < inventoryParent.GetChild(i).childCount; j++)
			{
				if (i == 1)
				{
					_weaponItemUI.Add(inventoryParent.GetChild(i).GetChild(j).GetComponent<ItemUI>());
				}
				else
				{
					_consumableItemUI.Add(inventoryParent.GetChild(i).GetChild(j).GetComponent<ItemUI>());
				}
			}
		}

		if (_weaponItemUI.Count == 0 || _consumableItemUI.Count == 0)
			return;
	}

	protected override void SubscribeItemsUI(List<ItemUI> listUi)
	{
		base.SubscribeItemsUI(listUi);

		if (listUi != null || listUi.Count > 0)
		{
			for (int i = 0; i < listUi.Count; i++)
			{
				listUi[i].OnUsableSwitched += ((PlayerInventory) _inventory).SwapUsable;
			}
		}
	}
}
