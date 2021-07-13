using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterInventoryUI : MonoBehaviour
{
	[SerializeField] private CharacterInventory _characterInventory;
	[SerializeField] private GameObject _itemUiPrefab;
	

	private void Start()
	{
		//Нужно реворкать. Слишком много событий
		_characterInventory.UpdateInventoryUIByNewItem += UpdateUIByNewItem;
		_characterInventory.UpdateInventoryUIByExistItem += UpdateUIByExistItem;
		_characterInventory.UpdateInventoryUIAll += UpdateUIByListOfItems;
		_characterInventory.DeleteItemUI += DeleteItemUI;
		_characterInventory.UpdateInventoryUIAll += sos => { };
	}

	private void DeleteItemUI(int index)
	{
		var itemUI = transform.GetChild(index);
		Destroy(itemUI.gameObject);
	}

	private void UpdateUIByListOfItems(List<ItemRecord> itemList)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < itemList.Count; i++)
		{
			UpdateUIByNewItem(itemList[i],i);
		}
	}

	private void UpdateUIByNewItem(ItemRecord itemRecord, int index)
	{
		var item = Instantiate(_itemUiPrefab, transform);
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
