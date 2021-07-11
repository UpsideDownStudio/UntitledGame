using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������������ ��� ������������� ���������
public enum CharacterStatsType
{
	HealthPoint,
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

	public void ModifyValue(ItemSO item)
	{
		var statType = item.CharacterStatsType;

		switch (statType)
		{
			case CharacterStatsType.HealthPoint:
				HealthPoint += item.CharacterStatsModifieValue;
				Debug.Log("������ �������� = " + HealthPoint + "\n��������: " + item.Description);
				break;
			case CharacterStatsType.Damage:
				Damage += item.CharacterStatsModifieValue;
				Debug.Log("������ ���� = " + Damage);
				break;
			case CharacterStatsType.AttackSpeed:
				AttackSpeed += item.CharacterStatsModifieValue;
				Debug.Log("������ �������� ����� = " + AttackSpeed);
				break;
			case CharacterStatsType.AttackRange:
				AttackRange += item.CharacterStatsModifieValue;
				Debug.Log("������ ��������� ����� = " + AttackRange);
				break;
			case CharacterStatsType.Speed:
				Speed += item.CharacterStatsModifieValue;
				Debug.Log("������ �������� ������������ = " + Speed);
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

