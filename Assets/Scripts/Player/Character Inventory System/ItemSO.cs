using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.AssetImporters;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Items/Basic Item")]
public class ItemSO : ScriptableObject
{
	[SerializeField] private string _name;
	[SerializeField] private string _description;
	[SerializeField] private Sprite _icon;
	[SerializeField] private TypeOfItems _typeOfItem;
	
	public string Name
	{
		get => _name;
	}

	public string Description
	{
		get => _description;
	}

	public Sprite Icon
	{
		get => _icon;
	}

	public TypeOfItems TypeOfItems
	{
		get => _typeOfItem;
	}
}
