using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour, IDamage
{
	private CharacterStats _characterStats;

	[SerializeField]
	private Shot _shotPrefab;
	private float _nextFireTime;

	void Start()
    {
	    _characterStats = GetComponent<CharacterStats>();
    }

    
    void Update()
    {
        if(ReadyToFire())
			Fire();
    }

    private void Fire()
    {
	    _nextFireTime = Time.time + _characterStats.AttackSpeed;
	    Shot shot = Instantiate(_shotPrefab, transform.position + Vector3.up, transform.rotation);
	    shot.Launch(transform.forward);
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
