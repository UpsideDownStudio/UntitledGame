using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
	public event Action<Weapon> OnWeaponChangeEvent;

	[SerializeField] private Weapon _defaultWeapon;
	[SerializeField] private List<Weapon> _weaponsList;
	[SerializeField] private Weapon currentWeapon;

	private void Start()
	{
		currentWeapon = _defaultWeapon;
	}

	private void Update()
	{
		SwitchWeapon();
	}

	public Weapon GetWeapon()
	{
		return currentWeapon;
	}

	private void SwitchWeapon()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			currentWeapon = _weaponsList[0];
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			currentWeapon = _weaponsList[1];
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			currentWeapon = _weaponsList[2];
		}

		OnWeaponChangeEvent?.Invoke(currentWeapon);
	}
}
