using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] private GameObject _itemUiPrefab;

	public void UpdateAllUI(List<ItemRecord> itemList)
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

	public void UpdateUIByItem(ItemRecord item, int id, bool isNewItem = false)
	{
		if (isNewItem)
			UpdateUIByNewItem(item, id);
		else
			UpdateUIByExistItem(item, id);
	}

	private void UpdateUIByNewItem(ItemRecord itemRecord, int index)
	{
		var item = Instantiate(_itemUiPrefab, transform);
		Debug.Log(itemRecord.GetItem().Name);
		var itemInfo = item.GetComponent<ItemUI>();
		itemInfo.ConfigureItemUI(itemRecord, index);
	}

	private void UpdateUIByExistItem(ItemRecord itemRecord, int index)
	{
		var item = transform.GetChild(index);
		var itemInfo = item.GetComponent<ItemUI>();
		itemInfo.ConfigureItemUI(itemRecord, index);
	}
}
