using DG.Tweening;
using Melanchall.DryWetMidi.Interaction;
using UnityEditor.Search;
using UnityEngine;

namespace Com.IsartPesRhythmGame.NoteSystem
{
    public class TapNote : MonoBehaviour
    {
        // -------~~~~~~~~~~================# // Midi
        public Note RefNote;

        // -------~~~~~~~~~~================# // Renderer
        [SerializeField] private Transform _renderTransform;
        [SerializeField] private AudioSource _audioSource;
        private Vector3 _rendererDefaultPosition;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            InitRendererProperties();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
        private void InitRendererProperties()
        {
            _rendererDefaultPosition = _renderTransform.localPosition;
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Juice
        public void Collision()
        {
            _renderTransform.DOShakeScale(1f, 10f).SetEase(Ease.OutExpo);
            _renderTransform.DOLocalJump(_rendererDefaultPosition, 1f, 1, 1f).SetEase(Ease.InOutExpo);
            _renderTransform.DORotateQuaternion(Quaternion.AngleAxis(180f, Vector2.up), 1f).SetEase(Ease.OutExpo);

            _audioSource.Play();
        }
    }
}