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
	[SerializeField] private CharacterStatsType _characterStatsType;
	[SerializeField] private float _characterStatsModifieValue;
	
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

	public CharacterStatsType CharacterStatsType
	{
		get => _characterStatsType;
	}

	public float CharacterStatsModifieValue
	{
		get => _characterStatsModifieValue;
	}
}
