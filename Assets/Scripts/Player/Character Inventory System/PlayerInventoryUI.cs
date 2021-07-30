using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : InventoryUI
{
	[SerializeField] private List<GameObject> _weaponItemUI;
	[SerializeField] private List<GameObject> _consumableItemUI;

	public override void UpdateAllUI(List<ItemRecord> itemList)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < itemList.Count; i++)
		{
			UpdateUIByNewItem(itemList[i], i);
		}
	}
}
