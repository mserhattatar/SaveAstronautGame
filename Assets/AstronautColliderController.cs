using System.Collections;
using UnityEngine;

public class AstronautColliderController : MonoBehaviour
{
    [SerializeField] private GameObject destroyParticleEffectRed;
    [SerializeField] private GameObject destroyParticleEffectBlue;
    private bool _isShieldActive;
    private int _oxygenPercent;

    private void Start()
    {
        _oxygenPercent = 50;
        ShieldController.ShieldActiveDelegate += ShieldActive;
        ShieldController.ShieldPassiveDelegate += ShieldPassive;
        CanvasManager.GameResetDelegate += ResetGame;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OxygenTank"))
        {
            ShieldController.ShieldActiveDelegate();
            other.gameObject.GetComponent<OxygenTankController>().OxygenTankMove();
            CanvasManager.OxygenPercentReduceDelegate();
        }

        else if (other.gameObject.CompareTag("Satellite"))
        {
            if (_isShieldActive)
            {
                StartCoroutine(DestroyParticleEffect(Instantiate(destroyParticleEffectBlue, other.transform.position,
                    Quaternion.Euler(0, 0, 0))));
                other.gameObject.GetComponent<SatelliteController>().SetPassiveSatellite();
            }
            else
            {
                if (_oxygenPercent == 0)
                {
                    CanvasManager.GameOverDelegate();
                }

                _oxygenPercent -= 10;
                CanvasManager.OxygenPercentIncreaseDelegate();
                StartCoroutine(DestroyParticleEffect(Instantiate(destroyParticleEffectRed, other.transform.position,
                    Quaternion.Euler(0, 0, 0))));
                other.gameObject.GetComponent<SatelliteController>().SetPassiveSatellite();
            }
        }
    }

    private IEnumerator DestroyParticleEffect(GameObject particleEffect)
    {
        yield return new WaitForSeconds(2f);
        Destroy(particleEffect);
    }

    private void ShieldActive()
    {
        _isShieldActive = true;
    }

    private void ShieldPassive()
    {
        _isShieldActive = false;
    }

    private void ResetGame()
    {
        _oxygenPercent = 50;
    }
}