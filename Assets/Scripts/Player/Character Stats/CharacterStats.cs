using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Перечисление для характеристик персонажа
public enum CharacterStatsType
{
	HelthPoint,
	AttackRange,
	AttackSpeed,
	Damage,
	Speed
}

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

	public void ModifyValue(CharacterStatsType statType, float value)
	{
		switch (statType)
		{
			case CharacterStatsType.HelthPoint:
				HealthPoint += value;
				Debug.Log("Поднял здоровье = " + HealthPoint);
				break;
			case CharacterStatsType.Damage:
				Damage += value;
				Debug.Log("Поднял урон = " + Damage);
				break;
			case CharacterStatsType.AttackSpeed:
				AttackSpeed += value;
				Debug.Log("Поднял скорость атаки = " + AttackSpeed);
				break;
			case CharacterStatsType.AttackRange:
				AttackRange += value;
				Debug.Log("Поднял дальность атаки = " + AttackRange);
				break;
			case CharacterStatsType.Speed:
				Speed += value;
				Debug.Log("Поднял скорость передвижения = " + Speed);
				break;
		}
	}

	public void GetDamage(float damage)
	{
		if (HealthPoint <= 0f)
		{
			Debug.Log("Dead");
			Destroy(gameObject);
		}
		else
		{
			HealthPoint -= damage;
		}
	}

	public void DealDamage(float damage, IDamage target)
	{
		target.GetDamage(damage);
	}
}

