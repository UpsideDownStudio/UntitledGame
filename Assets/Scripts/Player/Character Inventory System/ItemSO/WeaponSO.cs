using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Items/Weapon Item")]
public class WeaponSO : ItemSO
{
	[SerializeField] private float _attackSpeed;
	
	[SerializeField] private bool _isMeleeWeapon;
	[SerializeField] private Shot _shotPrefab;

	public bool IsMeleeWeapon
	{
		get
		{
			return _isMeleeWeapon;
		}
	}
	public Shot ShotPrefab
	{
		get
		{
			return _shotPrefab;
		}

		set
		{
			_shotPrefab = value;
		}
	}
	public float AttackSpeed
	{
		get
		{
			return _attackSpeed;
		}
		set
		{
			_attackSpeed = value;
		}
	}
}
