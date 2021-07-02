using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour, ILootable
{
	public string name;
	public CharacterStatsType characterStatsType;
	public float modifyValue;

	//TODO: Подумать над переносом в класс CharacterInventory или что-то в этом духе. Или вообще переделать подбор предмета на Event, что скорее всего логичнее
	private void OnTriggerEnter(Collider other)
    {
	    if (other.CompareTag("Player") && other.gameObject.TryGetComponent(out CharacterStats type))
	    {
		    PickUp(type);
		    Destroy(gameObject);
		}
    }

    public void PickUp(CharacterStats player)
    {
	    player.ModifyValue(characterStatsType, modifyValue);
    }
}
