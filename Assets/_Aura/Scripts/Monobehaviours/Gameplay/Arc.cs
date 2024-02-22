using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 _destination,float _duration)
    {
        var startPosition = transform.position;
        var percentComplete = 0.0f;
        while(percentComplete < 1.0f)
        {
            percentComplete += Time.deltaTime/_duration;
            transform.position = Vector3.Lerp(startPosition, _destination, percentComplete);

            yield return null;//Pause execution of the Coroutine until next frame
        }

        gameObject.SetActive(false);
    }
}
