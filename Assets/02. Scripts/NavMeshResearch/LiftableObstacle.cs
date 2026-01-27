using UnityEngine;

public class LiftableObstacle : MonoBehaviour
{
    [SerializeField]
    private float _liftSpeed;

    private void Update()
    {
        LiftUp();
        LiftDown();
    }

    private void LiftUp()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += Vector3.up * _liftSpeed * Time.deltaTime;
        }
    }

    private void LiftDown()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += Vector3.down * _liftSpeed * Time.deltaTime;
        }
    }
}


