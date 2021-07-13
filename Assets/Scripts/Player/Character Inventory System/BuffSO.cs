using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff Data", menuName = "Items/Buff Item")]
public class BuffSO : ItemSO
{
	[SerializeField] private CharacterStatsType _characterStatsType;
	[SerializeField] private float  _modifyValue;
	public CharacterStatsType CharacterStatsType { get => _characterStatsType; }
	public float ModifyValue { get => _modifyValue; }
}
