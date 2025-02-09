using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartPesRhythmGame.NotesSystem
{
    public class NoteLine : MonoBehaviour
    {
        // -------~~~~~~~~~~================# // Components
        private Transform _transform;

        // -------~~~~~~~~~~================# // Line
        public int ID;

        // -------~~~~~~~~~~================# // Notes
        [SerializeField] private GameObject _noteFactory;
        private List<TapNote> _allNotes = new List<TapNote>();

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            InitComponents();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
        private void InitComponents()
        {
            _transform = transform;
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
            long lNoteDistance = pNote.Time;

            // Assign Note Properties
            lTapNote.RefNote = pNote;
            lNoteTransform.parent = _transform;
            lNoteTransform.position = _transform.position + _transform.up * lNoteDistance * .05f;
        }
    }
}