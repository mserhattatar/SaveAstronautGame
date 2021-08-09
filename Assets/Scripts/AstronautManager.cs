using UnityEngine;

public class AstronautManager : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 _touchPosition, _direction;
    private bool _isGameStart, _checkTouch;
    public new Camera camera;

    private void Start()
    {
        _checkTouch = true;
        _rb = GetComponent<Rigidbody2D>();
        CanvasManager.GameResetDelegate += ResetGame;
        CanvasManager.GameOverDelegate += CheckTouchSetPassive;
    }

    private void Update()
    {
        AstronautMovement();
    }

    private void AstronautMovement()
    {
        if (Input.touchCount > 0 && _checkTouch)
        {
            var touch = Input.GetTouch(0);
            _touchPosition = camera.ScreenToWorldPoint(touch.position);
            _touchPosition.z = 0;
            _direction = (_touchPosition - transform.position);
            _rb.velocity = new Vector2(_direction.x, _direction.y) * 10f;

            if (touch.phase == TouchPhase.Ended)
                _rb.velocity = Vector2.zero;
            if (!_isGameStart)
            {
                _isGameStart = true;
                CanvasManager.GameStartDelegate();
            }
        }

        //TODO: delete getmouse for android
        if (Input.GetMouseButton(1) && _checkTouch)
        {
            var mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);
            var direction1 = (mousePosition - transform.position);
            _rb.velocity = new Vector2(direction1.x, direction1.y) * 10f;
            if (!_isGameStart)
            {
                _isGameStart = true;
                CanvasManager.GameStartDelegate();
            }
        }
    }

    private void CheckTouchSetPassive()
    {
        _checkTouch = false;
    }

    private void ResetGame()
    {
        _checkTouch = true;
        _isGameStart = false;
    }
}