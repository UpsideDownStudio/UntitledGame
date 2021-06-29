using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
	public class CharacterMovementAnimation : MonoBehaviour
	{
		[SerializeField]
		private LayerMask _aimLayerMask;
		[SerializeField]
		private Animator _animator;

		void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		public void SetMovementAnimationSetting(Vector3 movementVector)
		{
			float velocityZ = Vector3.Dot(movementVector.normalized, transform.forward);
			float velocityX = Vector3.Dot(movementVector.normalized, transform.right);

			_animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
			_animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
		}
	}
}