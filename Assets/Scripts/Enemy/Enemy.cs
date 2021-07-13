using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamage
{
	public static event Action<int> OnEnemyDied;
	Animator _animator;
	
	[SerializeField] private GameObject _character;
	[SerializeField] private float _healthPoint = 100f;
	[SerializeField] private int _pointsByKill = 5;
	public float damage = 5f;
	public float attackSpeed = 5f;
	public float attackRange = 3f;
	private float _attackReload;

	private NavMeshAgent _navMeshAgent;

	private void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
		_navMeshAgent.enabled = false;
		_animator = GetComponent<Animator>();
	}

	void Start()
    {
	    _character = GameObject.FindGameObjectWithTag("Player");
	    _attackReload = attackSpeed;
    }

    void Update()
    {
		DealDamage(damage, _character.GetComponent<CharacterCombat>());
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
		    OnEnemyDied?.Invoke(_pointsByKill); 
		    //Проигрывание анимации смерти.
			Die();
			Destroy(gameObject, 5f);
	    }
	    else
	    {
		    _healthPoint -= damage;
	    }
    }

	private void Die()
	{
		GetComponent<Collider>().enabled = false;
		_navMeshAgent.enabled = false;
		GetComponent<CapsuleCollider>().enabled = false;
		_animator.SetTrigger("Died");
	}
    private void OnDrawGizmos()
    {
	    Gizmos.color = new Color(255, 0, 0 ,0.25f);
		Gizmos.DrawSphere(transform.position, attackRange);
    }

    //TODO: "��������": ���������� ��� OnTriggerEnter(�������������� � �����). ��-�� ����, ��� �� ����� �������� ������ � �����. 
    //(����� �� OnTriggerEnter) �� � ������ ������ � ������ GetDamage �� ����� ��������� �� ������ ����������, ��� ����� ����� �������� ��� ���.
    public void DealDamage(float damage, IDamage target)
    {
	    if (_attackReload <= 0)
	    {
		    if (IsDistanceForAttack(transform, _character.transform))
		    {
			    _animator.SetTrigger("Attack");
			    _navMeshAgent.enabled = false;
			    _attackReload = attackSpeed;
			}
		}
	    else
	    {
		    _attackReload -= Time.deltaTime;
	    }
    }

    public void StartWalking()
    {
	    _navMeshAgent.enabled = true;
	}

    private bool IsDistanceForAttack(Transform enemy, Transform target)
    {
	    if (Vector3.Distance(enemy.position, target.position) < attackRange)
		    return true;
	    else
		    return false;
    }
	
	//Animation Callback - для вызова метода по определенному времени анимации.
    public void AttackHit()
    {
		if(IsDistanceForAttack(transform, _character.transform))
			_character.GetComponent<CharacterCombat>().GetDamage(damage);
		Debug.Log("Hit");
    }

    public void AttackComplete()
    {
		_navMeshAgent.enabled = true;
    }
}
