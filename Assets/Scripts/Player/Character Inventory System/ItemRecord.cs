using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRecord : MonoBehaviour
{
	[SerializeField] private ItemSO _item;
	[SerializeField] public int currentStackValue;

	public ItemRecord(ItemSO item)
	{
		_item = item;
		currentStackValue = 1;
	}
	public ItemSO GetItem() => _item;
}
