using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SatelliteController : MonoBehaviour
{
    private int _pointIndex;
    public bool isActive;
    private readonly Vector3[] _allSatellitePoint = new Vector3[7];
    [SerializeField] private float movementSpeed;

    private void Start()
    {
        RandomPointPos();
        isActive = true;
    }

    private void Update()
    {
        var t = transform.position;
        var st = _allSatellitePoint[_pointIndex];
        if(Math.Abs(t.x - st.x) < 0.5f && Math.Abs(t.y - st.y) <0.5f)
        {
            _pointIndex += 1;
            if (_pointIndex == _allSatellitePoint.Length)
                _pointIndex = 0;
        }

        transform.position = Vector3.MoveTowards(t, st,movementSpeed * Time.deltaTime);
    }

    private void RandomPointPos()
    {
        var pos = transform.position;
        for (int i = 0; i < 7; i++)
        {
            var x = Random.Range(-2, 3);
            var y = Random.Range(-1, 4);
            _allSatellitePoint[i] = pos + new Vector3(x, y, 0);
        }
    }

    public void SetActiveSatellite(float y)
    {
        gameObject.SetActive(true);
        isActive = true;
        transform.position = new Vector3(0, y, 0);
        RandomPointPos();
    }
    public void SetPassiveSatellite()
    {
        gameObject.SetActive(false);
        isActive = false;
    }
}