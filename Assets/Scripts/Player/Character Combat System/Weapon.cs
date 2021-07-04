using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ����������� ����� ������. 
//�������� � ���� ������ ��������, ����������� � �.�.


//TODO: �������� �������������� � ScriptableObject
public class Weapon : MonoBehaviour
{
	public string Name;
	public bool isMeleeWeapon;
	public float damage;
	public float attackRange;
	public float attackSpeed;

	[SerializeField] private Transform _firePoint;
	[SerializeField] private Shot _shotPrefab;

	private float time;

	void Start()
	{
		_firePoint = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2);
	}
	
	public void Attack(RaycastHit hit)
	{
		if(isMeleeWeapon)
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
		Shot shot = Instantiate(_shotPrefab, _firePoint.position, transform.rotation);
		shot.Launch(hit.point - _firePoint.position);
	}
}
