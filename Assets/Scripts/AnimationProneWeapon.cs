using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationProneWeapon : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start() 
    {
        initialPosition = transform.localPosition;
        StartCoroutine(AnimationsProneWeapon());
    }

    private IEnumerator AnimationsProneWeapon()
    {
        float timeElapsed = 0f;

        while (true)
        {
            float offset = Mathf.Sin((timeElapsed / 1f) * Mathf.PI * 2) * 0.5f;
            transform.localPosition = initialPosition + new Vector3(0, offset, 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
