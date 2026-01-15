using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    [SerializeField]
    private int _movementSpeed;

    private Vector3 _movementDirection;
    private bool _isMoving = false;


    private void Update()
    {
        ChangeDirection();
        Move();
    }

    private void ChangeDirection()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _movementDirection = Vector3.forward;
            _isMoving = true;
            return;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _movementDirection = Vector3.back;
            _isMoving = true;
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _movementDirection = Vector3.left;
            _isMoving = true;
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _movementDirection = Vector3.right;
            _isMoving = true;
            return;
        }
        _isMoving = false;
    }

    private void Move()
    {
        if (!_isMoving)
        {
            return;
        }
        transform.position += _movementDirection * _movementSpeed * Time.deltaTime;
    }
}
