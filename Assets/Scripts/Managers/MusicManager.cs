using System.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Com.IsartPesRhythmGame.Managers
{
    public class MusicManager : Manager
    {
        // -------~~~~~~~~~~================# // Evemts
        static UnityEvent MusicStarted = new UnityEvent();

        // -------~~~~~~~~~~================# // Component
        private GameObject _gameObject;

        // Song
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private float _startSpeed = 1;
        private AudioSource _player;
        private float _songDuration;
        private float _songRatio;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            InitComponent();
            InitPlayer();
        }

        private void Start()
        {
            StartSong();
        }

        private void Update()
        {
            UpdateSongProperties();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
        private void InitComponent()
        {
            _gameObject = gameObject;
        }

        private void InitPlayer()
        {
            // Add Audio Source And Setup Properties
            _player = _gameObject.AddComponent<AudioSource>();
            _player.clip = m_map.Song;
            _player.playOnAwake = false;
            _player.outputAudioMixerGroup = _mixer;
            _player.pitch = _startSpeed;

            float lMusicLengthMinute = m_map.Song.length / 60f;

            m_map.Length = lMusicLengthMinute * m_map.BPM * MidiManager.BEAT * m_map.LengthMultipliyer;
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Song
        private void ResetSong()
        {
            _songDuration = m_map.Song.length;
            _player.time = 0;
        }

        private void StartSong()
        {
            // Reset Song Properties And Play Song
            ResetSong();
            _player.Play();

            MusicStarted.Invoke();
        }

        private void UpdateSongProperties()
        {
            if (!_player.isPlaying) return;
            UpdateSongRatio();
        }

        private void UpdateSongRatio() => m_map.SongRatio = _songRatio = _player.time / _songDuration;
    }
}