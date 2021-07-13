using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IDropHandler
{
	public static event Action<int> OnItemClicked;
	public static event Action<int, int> OnItemsSwitched;
	[SerializeField] public int itemIndex;
	[SerializeField] private GameObject _iconGameObject;
	[SerializeField] private CanvasGroup _canvasGroup;
		
	private int _dropIndexItem;

	private void Start()
	{
		_canvasGroup = transform.GetComponent<CanvasGroup>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnItemClicked?.Invoke(itemIndex);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		//Debug.Log($"Dragging with {eventData.pointerEnter.name}");
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

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log(itemIndex);
	}

	public void OnDrop(PointerEventData eventData)
	{
		int indexOfItem = eventData.pointerDrag.transform.GetComponent<ItemUI>().itemIndex;
		OnItemsSwitched?.Invoke(indexOfItem, itemIndex);
		Debug.Log("Droped " + indexOfItem + "Current " + itemIndex);
	}
}
