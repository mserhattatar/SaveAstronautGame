using UnityEngine;

public class AstronautManager : MonoBehaviour
{
    private int _lifeCount;
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float moveSpeed = 10f;

    private void Start()
    {
        _lifeCount = 3;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

            if (touch.phase == TouchPhase.Ended)
                rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Satellite"))
        {
            if (_lifeCount == 0)
            {
                Debug.Log("astronotun canı bitti");
            }

            Debug.Log("çarpıştı");
            _lifeCount -= 1;
            other.gameObject.GetComponent<SatelliteController>().SetPassiveSatellite();
        }
    }
}