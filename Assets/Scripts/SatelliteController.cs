using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    private int _pointIndex;
    private readonly Vector3[] _allSatellitePoint = new Vector3[7];

    private void Start()
    {
        var pos = transform.position;
        _allSatellitePoint[0] = pos + new Vector3(-2, 1, 0);
        _allSatellitePoint[1] = pos + new Vector3(-1, 0, 0);
        _allSatellitePoint[2] = pos + new Vector3(1, 0, 0);
        _allSatellitePoint[3] = pos + new Vector3(2, 1, 0);
        _allSatellitePoint[4] = pos + new Vector3(1, 2, 0);
        _allSatellitePoint[5] = pos + new Vector3(0, 3, 0);
        _allSatellitePoint[6] = pos + new Vector3(-1, 2, 0);
    }

    private void Update()
    {
        if (transform.position == _allSatellitePoint[_pointIndex])
        {
            _pointIndex += 1;
            if (_pointIndex == _allSatellitePoint.Length)
                _pointIndex = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, _allSatellitePoint[_pointIndex],
            1f * Time.deltaTime);
    }

    public void SetPassiveSatellite()
    {
        gameObject.SetActive(false);
    }
}