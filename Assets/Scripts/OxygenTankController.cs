using UnityEngine;
using Random = UnityEngine.Random;

public class OxygenTankController : MonoBehaviour
{
    [SerializeField] private Transform astronautTransform;

    private void Update()
    {
        if (astronautTransform.position.y > transform.position.y + 10f)
        {
            OxygenTankMove();
        }
    }

    public void OxygenTankMove()
    {
        var extraX = Random.Range(-2f, 2f);
        var astronautY = astronautTransform.position.y;
        transform.position = new Vector3(extraX, astronautY + 20f, 0f);
    }
}