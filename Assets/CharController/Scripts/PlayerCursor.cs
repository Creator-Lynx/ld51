using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private Transform _cursor;
    [SerializeField] private float _radius;
    //[SerializeField] private float _deadZoneRadius;
    public float _speed = 1f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Player)
        {
            transform.position = Player.position + Vector3.up;

            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            var mouseOffset = new Vector3(mouseX, 0, mouseY);

            _cursor.localPosition += mouseOffset * _speed;

            if (_cursor.localPosition.magnitude > _radius)
            {
                _cursor.localPosition = _cursor.localPosition.normalized * _radius;
            }
        }
    }

    public Vector2 GetDirection()
    {
        return new Vector2
        {
            x = _cursor.localPosition.x / _radius,
            y = _cursor.localPosition.z / _radius
        };
    }

    public float GetRadius()
    {
        return _radius;
    }

    private void OnDrawGizmosSelected()
    {
        if (_cursor)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
            Gizmos.DrawLine(transform.position, _cursor.position);
        }
    }
}
