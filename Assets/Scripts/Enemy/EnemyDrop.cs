using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
	public GameObject itemPrefab;
	public ItemSO item;

	public void DropItem()
	{
		var itemObject = Instantiate(itemPrefab, transform.position, Quaternion.identity);
		itemObject.GetComponent<Item>().SetItem(item);
	}
}
