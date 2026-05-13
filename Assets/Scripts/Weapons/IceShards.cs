using System.Collections;
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

    [Header("Rotation")]
    public float rotationSpeed = 180f; // Degrees per second

    private Vector3 originalScale;
    private bool isActivated = false;

    private void Start()
    {
        originalScale = transform.localScale;

        visualCollider.SetActive(false);
        iceShardsCollider.enabled = false;

        StartCoroutine(ActivateColliderPeriodically());
    }

    private IEnumerator ActivateColliderPeriodically()
    {
        while (true)
        {
            isActivated = true;
            iceShardsCollider.enabled = true;
            visualCollider.SetActive(true);

            yield return StartCoroutine(GrowCollider());
            yield return StartCoroutine(ShrinkCollider());

            isActivated = false;
            iceShardsCollider.enabled = false;
            visualCollider.SetActive(false);

            yield return new WaitForSeconds(activationInterval);
        }
    }

    private IEnumerator GrowCollider()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / growTime;

            transform.localScale = Vector3.Lerp(originalScale * startSize, originalScale * maxSize, t);

            RotateIceImage();

            yield return null;
        }
    }

    private IEnumerator ShrinkCollider()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / shrinkTime;

            transform.localScale = Vector3.Lerp(originalScale * maxSize, originalScale * startSize, t);

            RotateIceImage();

            yield return null;
        }
    }

    private void RotateIceImage()
    {
        if (visualCollider != null)
        {
            visualCollider.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}