using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    [Range(0.1f, 5f)] public float waitBetweenWobbles = 0.5f;
    [Range(1f, 50f)] public float Intensity = 010f;
    Quaternion _targetAngle;

    private void Start()
    {
        InvokeRepeating("ChangeTarget", 0, waitBetweenWobbles);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetAngle, Time.deltaTime);
    }

    public void ChangeTarget()
    {
        var intensity = Random.Range(0.1f, Intensity);
        var curve = Mathf.Sin(Random.Range(0, Mathf.PI * 2));
        _targetAngle = Quaternion.Euler(Vector3.forward * curve * intensity);
    }
}
