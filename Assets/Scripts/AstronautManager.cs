using System;
using UnityEngine;

public class AstronautManager : MonoBehaviour
{
    [SerializeField] private GameObject destroyParticleEffect;
    private int _oxygenPercent;
    private Vector3 _touchPosition;
    private Rigidbody2D _rb;
    private Vector3 _direction;
    private bool _isGameStart;
    private bool _checkTouch;
    
    public new Camera camera;

    private void Start()
    {
        _checkTouch = true;
        _oxygenPercent = 50;
        _rb = GetComponent<Rigidbody2D>();
        CanvasManager.GameResetDelegate += ResetGame;
        CanvasManager.GameOverDelegate += CheckTouchSetPassive;
    }

    private void Update()
    {
        AstronautMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Satellite")) return;
        if (_oxygenPercent == 0)
        {
            CanvasManager.GameOverDelegate();
        }
        _oxygenPercent -= 10;
        CanvasManager.OxygenPercentDelegate();
        Instantiate(destroyParticleEffect, other.transform.position,Quaternion.Euler(0,0,0));
        other.gameObject.GetComponent<SatelliteController>().SetPassiveSatellite();
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
        
        if (Input.GetMouseButton(1)&& _checkTouch)
        {
            var mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);
            var _direction1 = (mousePosition - transform.position);
            _rb.velocity = new Vector2(_direction1.x, _direction1.y) * 10f;
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
        _oxygenPercent = 50;
        _isGameStart = false;
    }
}