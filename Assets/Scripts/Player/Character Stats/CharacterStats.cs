using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public float Speed = 5f;
	public float HealthPoint = 100f;
	public float ArmorPoint = 2f;
	public float Damage = 10f;
	public float AttackSpeed = 1.2f;
	private float _attackReload;
	public float AttackRange = 2f;

	private void Start()
	{
		_attackReload = AttackSpeed;
	}

	private void Update()
	{
		if (_attackReload > 0)
			_attackReload -= Time.deltaTime;
	}

	public void ModifyValue(ItemSO item)
	{
		var statType = item.TypeOfItems;

		switch (statType)
		{
			case TypeOfItems.Buffs:
				UseBuffItem(item as BuffSO);
				break;
			case TypeOfItems.Weapon:
				UseWeaponItem();
				break;
		}
	}

	private void UseWeaponItem()
	{
		throw new NotImplementedException();
	}

	private void UseBuffItem(BuffSO item)
	{
		if (item != null)
		{
			var statType = item.CharacterStatsType;
			switch (statType)
			{
				case CharacterStatsType.HealthPoint:
					HealthPoint += item.ModifyValue;
					Debug.Log("Поднял здоровье = " + HealthPoint + "\nОписание: " + item.Description);
					break;
				case CharacterStatsType.Damage:
					Damage += item.ModifyValue;
					Debug.Log("Поднял урон = " + Damage);
					break;
				case CharacterStatsType.AttackSpeed:
					AttackSpeed += item.ModifyValue;
					Debug.Log("Поднял скорость атаки = " + AttackSpeed);
					break;
				case CharacterStatsType.AttackRange:
					AttackRange += item.ModifyValue;
					Debug.Log("Поднял дальность атаки = " + AttackRange);
					break;
				case CharacterStatsType.Speed:
					Speed += item.ModifyValue;
					Debug.Log("Поднял скорость передвижения = " + Speed);
					break;
			}
		}
	}
}

