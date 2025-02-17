using Com.IsartPesRhythmGame.Managers;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartPesRhythmGame.NoteSystem
{
    public class NoteLine : MonoBehaviour
    {
        // -------~~~~~~~~~~================# // Components
        private Transform _transform;

        // -------~~~~~~~~~~================# // Map
        [HideInInspector] public Map Map;

        // -------~~~~~~~~~~================# // Notes
        // Notes
        [SerializeField] private Transform _noteContainer;
        [SerializeField] private GameObject _noteFactory;
        private List<TapNote> _allNotes = new List<TapNote>();
        
        // Line
        [HideInInspector] public int ID;

        // Motion
        private Vector3 _startPoint;      
        private Vector3 _endPoint;
        private Vector3 _interpolatePoint => (_endPoint - _startPoint) * Map.SongRatio;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            InitComponents();
            ConnectEvents();
        }

        private void Update()
        {
            MoveNotes();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
        private void InitComponents()
        {
            _transform = transform;
        }

        private void InitMovement()
        {
            _startPoint = _noteContainer.position;
            _endPoint = _startPoint - _transform.forward * Map.Length;
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Events
        private void ConnectEvents()
        {
            NoteManager.AllLinesPlaced.AddListener(InitMovement);
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Notes
        public void Add(Note pNote)   
        {
            CreateNote(pNote);
        }

        private void CreateNote(Note pNote)
        {
            // Instanciate The Note
            Transform lNoteTransform = Instantiate(_noteFactory).transform;
            TapNote lTapNote = lNoteTransform.GetComponent<TapNote>();
            _allNotes.Add(lTapNote);

            // Setup Note Properties
            float lNoteDistance = pNote.Time * Map.LengthMultipliyer;

            // Assign Note Properties
            lTapNote.RefNote = pNote;
            lNoteTransform.parent = _noteContainer;
            lNoteTransform.position = _noteContainer.position + _transform.forward * lNoteDistance;
        }

        private void MoveNotes()
        {
            _noteContainer.position = _startPoint + _interpolatePoint;
        }
    }
}