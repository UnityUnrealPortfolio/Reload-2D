using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 _destination,float _duration)
    {
        Debug.Log($"Duration: {_duration}");
        var startPosition = transform.position;
        var percentComplete = 0.0f;
        while(percentComplete < 1.0f)
        {
            percentComplete += Time.deltaTime/_duration;
            
            var currentHeight = Mathf.Sin(Mathf.PI*percentComplete);
            transform.position = Vector3.Lerp(startPosition, _destination, percentComplete)
                + Vector3.up * currentHeight;

            yield return null;//Pause execution of the Coroutine until next frame
        }

        gameObject.SetActive(false);
    }
}
