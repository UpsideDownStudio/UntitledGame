using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamage
{
	[SerializeField]
	private GameObject _character;
	[SerializeField]
	private float _healthPoint = 100f;
	public float damage = 5f;
	public float attackSpeed = 5f;
	public float attackRange = 3f;
	private float _attackReload;

	private NavMeshAgent _navMeshAgent;

	private void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	void Start()
    {
	    _character = GameObject.FindGameObjectWithTag("Player");
	    _attackReload = attackSpeed;
    }

    void Update()
    {
		DealDamage(damage, _character.GetComponent<CharacterWeapon>());
		FollowTarget(_character);
    }

    private void FollowTarget(GameObject target)
    { 
		if(_navMeshAgent.enabled)
			_navMeshAgent.SetDestination(_character.transform.position);
	}

    public void GetDamage(float damage)
    {
	    if (_healthPoint <= 0 || _healthPoint - damage <= 0)
	    {
			Destroy(gameObject);
	    }
	    else
	    {
		    _healthPoint -= damage;
	    }
    }

    private void OnDrawGizmos()
    {
	    Gizmos.color = Color.blue;
		Gizmos.DrawSphere(transform.position, attackRange);
    }

    public void DealDamage(float damage, IDamage target)
    {
	    if (_attackReload <= 0)
	    {
		    if (Vector3.Distance(transform.position, _character.transform.position) < attackRange)
		    {

				//Для того чтобы наносить дамаг как пройдёт анимация смотреть видос на 1:13:28
			    target.GetDamage(damage);
			    _attackReload = attackSpeed;
		    }
		}
	    else
	    {
		    _attackReload -= Time.deltaTime;
	    }
    }
	

	//TODO: Animation callback - настроить их, когда прикрутят анимации для врага.
    public void AttackPlayer()
    {

    }

    public void AttackComplete()
    {

    }
}
