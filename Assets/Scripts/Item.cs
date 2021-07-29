using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour, ILootable
{
	[SerializeField] private ItemSO _itemSo;

	//TODO: Подумать над переносом в класс CharacterInventory или что-то в этом духе. Или вообще переделать подбор предмета на Event, что скорее всего логичнее
	private void OnTriggerEnter(Collider other)
    {
	    if (other.CompareTag("Player") && other.gameObject.TryGetComponent(out PlayerInventory player))
	    {
		    PickUp(player);
	    }
    }

	public void PickUp(Inventory playerInventory)
	{
		if(((PlayerInventory)playerInventory).TryToAddItem(_itemSo))
			Destroy(gameObject);
	}

	public void SetItem(ItemSO item)
	{
		_itemSo = item;
	}
}
