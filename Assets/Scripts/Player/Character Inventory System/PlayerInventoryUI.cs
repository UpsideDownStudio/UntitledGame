using System.Collections;
using System.Collections.Generic;
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

	public void UpdateUsableUI(List<ItemSO> itemList, TypeOfItems itemType)
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

	private void UpdateUsableUI(List<GameObject> itemUiList, List<ItemSO> itemList)
	{
		for (int i = 0; i < itemUiList.Count; i++)
		{
			var record = new ItemRecord(itemList[i]);
			_weaponItemUI[i].GetComponent<ItemUI>().ConfigureItemUI(record, i);
		}
	}
}
