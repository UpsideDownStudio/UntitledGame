using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerInventoryUI : InventoryUI
{
	[SerializeField] private List<GameObject> _weaponItemUI;
	[SerializeField] private List<GameObject> _consumableItemUI;

	public override void UpdateInventoryUI(List<ItemRecord> itemList)
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			UpdateUIByNewItem(itemList[i], i);
		}
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

	private void UpdateUsableUI(List<GameObject> itemUiList, List<ItemRecord> itemList)
	{
		for (int i = 0; i < itemUiList.Count; i++)
		{
			itemUiList[i].GetComponent<ItemUI>().ConfigureItemUI(itemList[i], i);
		}
	}
}
