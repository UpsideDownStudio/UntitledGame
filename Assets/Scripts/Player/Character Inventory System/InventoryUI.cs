using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] private GameObject _itemUiPrefab;
	[SerializeField] protected ItemRecord _itemRecord;
	

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
		ItemRecord newItem = null;
		if (itemRecord == null)
		{
			newItem = Instantiate(_itemRecord);
			newItem.Item = null;
		}
		itemInfo.ConfigureItemUI(itemRecord ?? newItem, index);
	}

	protected void UpdateUIByExistItem(ItemRecord itemRecord, int index)
	{
		var item = transform.GetChild(index);
		var itemInfo = item.GetComponent<ItemUI>();
		itemInfo.ConfigureItemUI(itemRecord, index);
	}
}
