using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
	public event Action<Weapon> OnWeaponChangeEvent;

	[SerializeField] private Weapon _weaponPrefab;
	
	[SerializeField] private Weapon _defaultWeapon;
	[SerializeField] private List<Weapon> _weaponsList;
	[SerializeField] private Weapon _currentWeapon;

	private PlayerInventory _playerInventory;

	private void Start()
	{
		_playerInventory = GetComponent<PlayerInventory>();

		InitializeWeapon();

		_playerInventory.OnWeaponItemSwitched += SwitchWeaponItem;
		_currentWeapon = _defaultWeapon;
	}

	//¬озможность загрузки из сохранений.
	private void InitializeWeapon()
	{
		for (int i = 0; i < PlayerInventory.WeaponItemSlot; i++)
		{
			_weaponsList.Add(Instantiate(_weaponPrefab));
		}
	}

	private void Update()
	{
		SwitchWeapon();
	}

	public Weapon GetWeapon()
	{
		return _currentWeapon;
	}

	private void SwitchWeaponItem(List<ItemRecord> weaponList)
	{
		for (int i = 0; i < PlayerInventory.WeaponItemSlot; i++)
		{
			if(weaponList[i].Item is WeaponSO)
				_weaponsList[i].WeaponItem = (WeaponSO) weaponList[i].Item;
			Debug.Log(_weaponsList[i].WeaponItem?.Name);
		}
	}

	private void SwitchWeapon()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwapWeapon(0);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwapWeapon(1);
		}
	}

	private void SwapWeapon(int id)
	{
		_currentWeapon = _weaponsList[id];
		OnWeaponChangeEvent?.Invoke(_currentWeapon);
		Debug.Log(_currentWeapon.WeaponItem.Name);
	}
}
