using UnityEngine;

public class LiftableObstacle : MonoBehaviour
{
    [SerializeField]
    private float _liftScalar;

    private void Update()
    {
        LiftUp();
        LiftDown();
    }

    private void LiftUp()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position += Vector3.up * _liftScalar;
        }
    }

    private void LiftDown()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position += Vector3.down * _liftScalar;
        }
    }
}


