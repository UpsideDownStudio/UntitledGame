using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shot : MonoBehaviour
{
	[SerializeField]
	private float _speed = 15f;
	[SerializeField] 
	private float _damage = 10f;

	public void Launch(Vector3 direction)
	{
		direction.Normalize();
		transform.up = direction;
		GetComponent<Rigidbody>().velocity = direction * _speed;
	}

	private void OnCollisionEnter(Collision other)
	{
		var target = other.collider.GetComponent<IDamage>();
		if (target != null)
		{
			target.GetDamage(_damage);
		}

		Destroy(gameObject);
	}

	void Start()
	{
		Destroy(gameObject, 5f);
	}
}
