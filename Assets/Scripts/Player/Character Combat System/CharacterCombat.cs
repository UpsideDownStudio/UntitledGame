using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour, IDamage
{
	private CharacterStats _characterStats;
	private PlayerInventory _playerInventory;
	private CharacterWeapon _characterWeapon;

	[SerializeField] private Shot _shotPrefab;
	[SerializeField] private Transform _firePoint;
	[SerializeField] private Weapon _currentWeapon;
	Animator _animator;
	private float _nextFireTime;

	private void Awake()
	{
		_characterStats = GetComponent<CharacterStats>();
		_characterWeapon = GetComponent<CharacterWeapon>();
		_playerInventory = GetComponent<PlayerInventory>();
		_characterWeapon.OnWeaponChangeEvent += ChangeWeapon;

		_animator = GetComponent<Animator>();
	}

	private void ChangeWeapon(Weapon weapon)
	{
		_currentWeapon = weapon;
	}

	private void Start()
	{
		_currentWeapon = _characterWeapon.GetWeapon();
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Mouse0) && ReadyToDealDamage())
		{
			Fire();
		}
    }
	
	//TODO: Надо пофиксить стрельбу при открытом инвентаре
    private void Fire()
    {
	    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

	    if (Physics.Raycast(mouseRay, out RaycastHit hit))
	    {
			_animator.SetTrigger("Shoot");
		    _currentWeapon.Attack(hit);
		    _nextFireTime = Time.time + _currentWeapon.WeaponItem.AttackSpeed * _characterStats.AttackSpeed;
		}
    }

	private bool ReadyToDealDamage() => Time.time >= _nextFireTime;

    public void GetDamage(float damage)
    {
	    if (_characterStats.HealthPoint <= 0f || _characterStats.HealthPoint - damage <= 0)
	    {
		    Debug.Log("Dead");
		    Destroy(gameObject);
	    }
	    else
	    {
		    _characterStats.HealthPoint -= damage;
	    }
    }

    public void DealDamage(float damage, IDamage target)
    {
	    target.GetDamage(damage);
    }
}
