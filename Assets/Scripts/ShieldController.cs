using System.Collections;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public delegate void ShieldDelegate();

    public static ShieldDelegate ShieldActiveDelegate;
    public static ShieldDelegate ShieldPassiveDelegate;


    private void Start()
    {
        ShieldActiveDelegate += SetActiveShield;
    }

    private void SetActiveShield()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(SetPassiveShield());
    }

    private IEnumerator SetPassiveShield()
    {
        yield return new WaitForSeconds(10f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        ShieldPassiveDelegate();
    }
}