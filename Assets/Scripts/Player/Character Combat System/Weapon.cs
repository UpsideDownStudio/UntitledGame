using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public WeaponSO WeaponItem;
	
	[SerializeField] protected Transform _firePoint;

	private float time;

	void Start()
	{
		_firePoint = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2);
	}
	
	public void Attack(RaycastHit hit)
	{
		Debug.Log("Shot in Weapon");
		if(WeaponItem.IsMeleeWeapon)
			MeleeAttack();
		else
			RangeAttack(hit);	
	}

	private void MeleeAttack()
	{

	}

	private void RangeAttack(RaycastHit hit)
	{
		_firePoint = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2);
		Shot shot = Instantiate(WeaponItem.ShotPrefab, _firePoint.position, transform.rotation);
		shot.Launch(hit.point - _firePoint.position);
	}
}
