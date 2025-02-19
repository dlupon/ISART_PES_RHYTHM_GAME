using DG.Tweening;
using Melanchall.DryWetMidi.Interaction;
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
        private Vector3 _rendererDefaultScale;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            InitRendererProperties();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
        private void InitRendererProperties()
        {
            _rendererDefaultPosition = _renderTransform.localPosition;
            _rendererDefaultScale = _renderTransform.localScale;
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Juice
        public void Collision()
        {
            Vector3 lScale = Vector3.zero;
            lScale.x = _rendererDefaultScale.x * .5f;
            lScale.y = _rendererDefaultScale.y * 2f;
            lScale.z = _rendererDefaultScale.z * .5f;

            _renderTransform.DOScale(_rendererDefaultScale, 1f).From(lScale).SetEase(Ease.OutElastic);

            _audioSource.Play();
        }
    }
}