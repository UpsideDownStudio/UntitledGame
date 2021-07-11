using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour, ILootable
{
	[SerializeField] private ItemSO _itemSo;

	//TODO: Подумать над переносом в класс CharacterInventory или что-то в этом духе. Или вообще переделать подбор предмета на Event, что скорее всего логичнее
	private void OnTriggerEnter(Collider other)
    {
	    if (other.CompareTag("Player") && other.gameObject.TryGetComponent(out CharacterInventory player))
	    {
		    PickUp(player);
	    }
    }

	public void PickUp(CharacterInventory player)
	{
		if(player.TryToPickUpItem(_itemSo))
			Destroy(gameObject);
	}
}
