using Com.IsartPesRhythmGame.NoteSystem;
using DG.Tweening;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // other.transform.DOScale(Vector3.one, .5f).From(new Vector3(2f, .1f, 2f)).SetEase(Ease.OutBack);
        // other.transform.DORotateQuaternion(Quaternion.AngleAxis(180f, Vector2.up), 1f);

        if (!other.TryGetComponent(out TapNote pNote)) return;
        pNote.Collision();
    }
}
