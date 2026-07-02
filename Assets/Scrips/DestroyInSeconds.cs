using UnityEngine;
using System. Collections;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField]
    private float secondsToDestroy = 2f;
    private void OnEnable()
    {
        StartCoroutine(DestroyAfterSeconds());
    }
    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        gameObject.SetActive(false);
    }
}
