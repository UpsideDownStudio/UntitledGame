using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler
{
	public static event Action<int> OnItemClicked;
	[SerializeField] public int _itemIndex;

	public void OnPointerClick(PointerEventData eventData)
	{
		OnItemClicked?.Invoke(_itemIndex);
	}
}
