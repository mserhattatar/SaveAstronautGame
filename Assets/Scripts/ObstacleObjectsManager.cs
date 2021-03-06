using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleObjectsManager : MonoBehaviour
{
    [SerializeField] private List<SatelliteController> satelliteList = new List<SatelliteController>();
    private float _satellitePosY = 4f;
    public GameObject astronautPlayer;


    private void LateUpdate()
    {
        if (satelliteList.Count(o => !o.isActive) > 0)
            SatelliteSetActive();
        else
            SatelliteSetPassive();
    }


    private void SatelliteSetPassive()
    {
        var pPos = astronautPlayer.transform.position.y;
        var activeObstacleList = satelliteList.Where(o => o.isActive);
        foreach (var o in activeObstacleList)
        {
            if (pPos > o.transform.position.y + 12f)
                o.SetPassiveSatellite();
        }
    }

    private void SatelliteSetActive()
    {
        var passiveSatellite = satelliteList.Where(o => !o.isActive);
        foreach (var satellite in passiveSatellite)
        {
            satellite.SetActiveSatellite(_satellitePosY);
            SatellitePosYUpdate();
        }
    }

    private void SatellitePosYUpdate()
    {
        var extraY = Random.Range(3f, 6f);
        _satellitePosY += extraY;
    }
}