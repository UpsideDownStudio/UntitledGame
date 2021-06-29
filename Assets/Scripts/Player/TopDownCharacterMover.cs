using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

public class TopDownCharacterMover : MonoBehaviour
{
    private InputHandler _inputHandler;
    private CharacterMovementAnimation _characterMovementAnimation;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private bool rotateTowardsMouse;


    [SerializeField]
    private Camera camera;

    private void Awake()
	{
        _inputHandler = GetComponent<InputHandler>();
        _characterMovementAnimation = GetComponent<CharacterMovementAnimation>();
	}

    void Update()
    {
        var targetVector = new Vector3(_inputHandler.InputVector.x, 0, _inputHandler.InputVector.y);

        var movementVector = MoveTowardTarget(targetVector);
        _characterMovementAnimation.SetMovementAnimationSetting(movementVector);

        if (!rotateTowardsMouse)
            RotateTowardMovementVector(movementVector);
        else
            RotateTowardMouseVector();
    }

	private void RotateTowardMouseVector()
	{
        Ray ray = camera.ScreenPointToRay(_inputHandler.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
		{
            Quaternion targetRotation = Quaternion.LookRotation(hitInfo.point - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            //Второй способ поворота персонажа к точке.
            //var target = hitInfo.point;
            //target.y = transform.position.y;
            //transform.LookAt(target);
        }
	}

	private void RotateTowardMovementVector(Vector3 movementVector)
	{
        if(movementVector.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
	}

    private Vector3 MoveTowardTarget(Vector3 targetVector)
	{
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;

        return targetVector;
	}
}
