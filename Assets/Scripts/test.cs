using DG.Tweening;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.DOShakeScale(.5f, .5f);
        other.transform.DOShakeRotation(.5f, 15f);
    }
}
