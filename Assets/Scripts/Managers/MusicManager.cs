using System;
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
        [SerializeField] private AudioClip _countDownSound;
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private float _startSpeed = 1;
        private AudioSource _musicPlayer;
        private AudioSource _countDownPlayer;
        private float _songDuration;
        private float _songRatio;

        // State Machine
        private Action UpdateAction;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            InitComponent();
            InitPlayers();
        }

        private void Start()
        {
            StartCountDown();
        }

        private void Update()
        {
            UpdateAction();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
        private void InitComponent()
        {
            _gameObject = gameObject;
        }

        private void InitPlayers()
        {
            InitMusicPlayer();
            InitCountDownPlayer();
        }

        private void InitMusicPlayer()
        {
            // Add Audio Source And Setup Properties
            _musicPlayer = _gameObject.AddComponent<AudioSource>();
            _musicPlayer.clip = m_map.Song;
            _musicPlayer.playOnAwake = false;
            _musicPlayer.outputAudioMixerGroup = _mixer;
            _musicPlayer.pitch = _startSpeed;

            float lMusicLengthMinute = m_map.Song.length / 60f;

            m_map.Length = lMusicLengthMinute * m_map.BPM * MidiManager.BEAT * m_map.LengthMultipliyer;
        }

        private void InitCountDownPlayer()
        {
            _countDownPlayer = _gameObject.AddComponent<AudioSource>();
            _countDownPlayer.clip = _countDownSound;
            _countDownPlayer.playOnAwake = false;
            _countDownPlayer.outputAudioMixerGroup = _mixer;
            _countDownPlayer.pitch = _startSpeed;

            UpdateAction = UpdateCountDown;
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // CountDown
        private void StartCountDown()
        {
            _countDownPlayer.Play();
        }

        private void UpdateCountDown()
        {
            if (_countDownPlayer.isPlaying) return;
            StartSong();
            UpdateAction = UpdateSongProperties;
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Song
        private void ResetSong()
        {
            _songDuration = m_map.Song.length;
            _musicPlayer.time = 0;
        }

        private void StartSong()
        {
            // Reset Song Properties And Play Song
            ResetSong();
            _musicPlayer.Play();

            MusicStarted.Invoke();
        }

        private void UpdateSongProperties()
        {
            if (!_musicPlayer.isPlaying) return;
            UpdateSongRatio();
        }

        private void UpdateSongRatio() => m_map.SongRatio = _songRatio = _musicPlayer.time / _songDuration;
    }
}