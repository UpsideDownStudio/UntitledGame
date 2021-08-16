using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRecord : MonoBehaviour
{
	[SerializeField] private ItemSO _item;
	public int CurrentStackValue = 1;

	public ItemSO Item
	{
		get
		{
			return _item;
		}
		set
		{
			_item = value;
		}
	}
}
