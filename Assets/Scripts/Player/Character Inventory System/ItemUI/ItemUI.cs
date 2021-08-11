using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
	public static event Action<int> OnItemClicked;
	public static event Action<int, int> OnItemsSwitched;
	public static event Action<int, int, bool, bool, TypeOfItems> OnUsableSwitched;

	[SerializeField] private bool _isWeaponSlot;
	[SerializeField] private bool _isConsumableSlot;
	
	[SerializeField] public int itemIndex;
	[SerializeField] private GameObject _iconGameObject;
	[SerializeField] private TMP_Text _tmpCountText;
	[SerializeField] private TMP_Text _tmpNameText;
	[SerializeField] private CanvasGroup _canvasGroup;

	private void Start()
	{
		_canvasGroup = transform.GetComponent<CanvasGroup>();
	}

	public void ConfigureItemUI(ItemRecord itemRecord, int index)
	{
		if (itemRecord.Item != null)
		{
			itemIndex = index;
			_tmpCountText.text = itemRecord.currentStackValue.ToString();
			_tmpNameText.text = itemRecord.Item.Name;
		}
		else
		{
			itemIndex = index;
			_tmpCountText.text = "";
			_tmpNameText.text = "";
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(!_isWeaponSlot && !_isConsumableSlot)
			OnItemClicked?.Invoke(itemIndex);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_canvasGroup.alpha = .6f;
		_canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		_iconGameObject.transform.position = eventData.pointerCurrentRaycast.screenPosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_iconGameObject.transform.localPosition = Vector3.zero;
		_canvasGroup.alpha = 1f;
		_canvasGroup.blocksRaycasts = true;
	}

	public void OnDrop(PointerEventData eventData)
	{
		var item = eventData.pointerDrag.transform.GetComponent<ItemUI>();

		if (item._isWeaponSlot || _isWeaponSlot)
		{
			OnUsableSwitched?.Invoke(item.itemIndex, itemIndex, item._isWeaponSlot, _isWeaponSlot, TypeOfItems.Weapon);
		}
		else if (item._isConsumableSlot || _isConsumableSlot)
		{
			OnUsableSwitched?.Invoke(item.itemIndex, itemIndex, item._isConsumableSlot, _isConsumableSlot, TypeOfItems.Buffs);
		}
		else
		{
			OnItemsSwitched?.Invoke(item.itemIndex, itemIndex);
		}

		Debug.Log("Droped " + item.itemIndex + "Current " + itemIndex);
	}
}
