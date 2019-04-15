using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float objectSpeed = 1.5f;
    [SerializeField] private float resetPosition = 0.0f;
    [SerializeField] private float startPosition = 0.0f;
    [SerializeField] private bool rotate = false;
    [SerializeField] private float rotateSpeed = 1.0f;
    private float angle;

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveLeft();

        if (rotate)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        angle += Time.deltaTime * 50 * rotateSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, angle, 0);
    }

    private void MoveLeft()
    {
        if (GameManager.instance.PlayerActive)
        {
            transform.Translate(Vector3.left * (objectSpeed * Time.deltaTime), Space.World);
            if (transform.localPosition.x <= resetPosition)
            {
                Vector3 newPosition = new Vector3(startPosition, transform.position.y, transform.position.z);
                transform.position = newPosition;

                if (gameObject.tag == "coin")
                    GameManager.instance.ToggleVisibility(gameObject, true);
            }
        }
    }
}
