using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
	void GetDamage(float damage);
	void DealDamage(float damage, IDamage target);
}
