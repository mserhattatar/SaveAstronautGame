using UnityEngine;

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
        if (transform.position == _allSatellitePoint[_pointIndex])
        {
            _pointIndex += 1;
            if (_pointIndex == _allSatellitePoint.Length)
                _pointIndex = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, _allSatellitePoint[_pointIndex],
            movementSpeed * Time.deltaTime);
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
    }
    public void SetPassiveSatellite()
    {
        gameObject.SetActive(false);
        isActive = false;
    }
}