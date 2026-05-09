using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShards : MonoBehaviour
{
    public float startSize = 1f;
    public float maxSize = 3f;
    public float growTime = 2f;
    public float shrinkTime = 2f;
    public float activationInterval = 5f;
    public GameObject visualCollider;

    public Collider2D iceShardsCollider;
    private Vector3 originalScale;
    private bool isActivated = false;

    private void Start()
    {
        
        originalScale = transform.localScale;
        visualCollider.SetActive(false); // Disable the visual collider initially
        StartCoroutine(ActivateColliderPeriodically());
    }

    private IEnumerator ActivateColliderPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationInterval);

            isActivated = true;
            iceShardsCollider.enabled = true;
            visualCollider.SetActive(true);
            

            yield return StartCoroutine(GrowCollider());
            yield return StartCoroutine(ShrinkCollider());

            isActivated = false;
            iceShardsCollider.enabled = false;
            visualCollider.SetActive(false);
            
        }
    }

    private IEnumerator GrowCollider()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / growTime;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * maxSize, t);
            yield return null;
        }
    }

    private IEnumerator ShrinkCollider()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / shrinkTime;
            transform.localScale = Vector3.Lerp(originalScale * maxSize, originalScale, t);
            yield return null;
        }
    }
}
