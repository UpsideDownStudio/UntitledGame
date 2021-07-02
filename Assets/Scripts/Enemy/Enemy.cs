using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamage
{
	public static event Action<int> OnEnemyDied;

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
		    OnEnemyDied?.Invoke(_pointsByKill);
			Destroy(gameObject);
	    }
	    else
	    {
		    _healthPoint -= damage;
	    }
    }

    private void OnDrawGizmos()
    {
	    Gizmos.color = new Color(255, 0, 0 ,0.25f);
		Gizmos.DrawSphere(transform.position, attackRange);
    }

    //TODO: "¬озможно": ѕеределать под OnTriggerEnter(посоветоватьс€ с тимой). »з-за того, что мы можем получать резист к урону. 
    //(ќтвет на OnTriggerEnter) Ќо в классе игрока в методе GetDamage мы можем провер€ть на булеву переменную, мол можем дамаг получить или нет.
    public void DealDamage(float damage, IDamage target)
    {
	    if (_attackReload <= 0)
	    {
		    if (Vector3.Distance(transform.position, _character.transform.position) < attackRange)
		    {

				//ƒл€ того чтобы наносить дамаг как пройдЄт анимаци€ смотреть видос на 1:13:28
			    target.GetDamage(damage);
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
	

	//TODO: Animation callback - настроить их, когда прикрут€т анимации дл€ врага.
    public void AttackPlayer()
    {

    }

    public void AttackComplete()
    {

    }
}
