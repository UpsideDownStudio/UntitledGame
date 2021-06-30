using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
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

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, AttackRange);
	}

	public void DealDamage(float damage, IDamage target)
	{
		target.GetDamage(damage);
	}
}
