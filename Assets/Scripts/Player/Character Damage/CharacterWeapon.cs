using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour, IDamage
{
	private CharacterStats _characterStats;
	[SerializeField] private Shot _shotPrefab;
	[SerializeField] private Transform _firePoint;
	private float _nextFireTime;

	void Start()
    {
	    _characterStats = GetComponent<CharacterStats>();
    }

    
    void Update()
    {
		//В зависимости от реализации оружия
		//Полуавтоматическое GetKeyDown
		//Автоматическое GetKey
        if(Input.GetKeyDown(KeyCode.Mouse0) && ReadyToFire())
			Fire();
    }

    private void Fire()
    {
	    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

	    if (Physics.Raycast(mouseRay, out RaycastHit hit))
		{
			Shot shot = Instantiate(_shotPrefab, _firePoint.position, transform.rotation);
		    shot.Launch(hit.point - _firePoint.position);
		    _nextFireTime = Time.time + _characterStats.AttackSpeed;
		}
    }

    private bool ReadyToFire() => Time.time >= _nextFireTime;

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
