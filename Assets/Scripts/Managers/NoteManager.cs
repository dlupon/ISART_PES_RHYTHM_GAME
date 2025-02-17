using Com.IsartPesRhythmGame.NoteSystem;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.IsartPesRhythmGame.Managers
{
    public class NoteManager : Manager
    {
        // -------~~~~~~~~~~================# // Events
        public static UnityEvent AllLinesPlaced = new UnityEvent();

        // -------~~~~~~~~~~================# // Note Line
        [SerializeField] private float _lineDistance = 1f;
        [SerializeField] private Transform _lineParent;
        [SerializeField] private GameObject _lineFactory;
        private List<NoteLine> _allLine = new List<NoteLine>();

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            InitLineContainer();
            ConnectEvents();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
        private void InitLineContainer()
        {
            if (!_lineParent) _lineParent = transform;
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Events
        private void ConnectEvents()
        {
            MidiManager.NoteExtractedFromMidi.AddListener(PlaceNoteOnLine);
            MidiManager.MidiExtractionComplited.AddListener(InitLines);
        }

        private void DisconnectEvents()
        {
            MidiManager.NoteExtractedFromMidi.RemoveListener(PlaceNoteOnLine);
            MidiManager.MidiExtractionComplited.RemoveListener(InitLines);
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Note
        private void PlaceNoteOnLine(Note pNote)
        {
            NoteLine lNoteLine = FindOrCreateLine(pNote);
            lNoteLine.Add(pNote);
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Line
        private NoteLine FindOrCreateLine(Note pNote)
        {
            int lWantedID = pNote.NoteNumber;
            int lLength = _allLine.Count;
            NoteLine lCurrentLine;

            for(int lCurrentLineIndex = 0; lCurrentLineIndex < lLength; lCurrentLineIndex++)
            {
                lCurrentLine = _allLine[lCurrentLineIndex];

                // Return Line If Founded
                if (lCurrentLine.ID == lWantedID) return lCurrentLine;

                // Create Line Before The Current One If Index Is Below
                if (lCurrentLine.ID > lWantedID) return CreateLineAt(lCurrentLineIndex, lWantedID);
            }

            // If Line Is Above, Then Create Line At The End Of The List
            return CreateLineAt(lLength, lWantedID);
        }

        private NoteLine CreateLineAt(int pIndex, int pID)
        {
            // Prevent Out Of Range Errors
            pIndex = Mathf.Clamp(pIndex, 0, _allLine.Count);

            // Instantiate Line
            Transform lNewLineTransform = Instantiate(_lineFactory).transform;
            NoteLine lNewLine = lNewLineTransform.GetComponent<NoteLine>();
            _allLine.Insert(pIndex, lNewLine);

            // Setup Line Properties
            lNewLineTransform.parent = _lineParent;
            lNewLine.ID = pID;
            lNewLine.Map = m_map;

            return lNewLine;
        }

        private void InitLines()
        {
            PlaceLines();
            AllLinesPlaced.Invoke();
        }

        private void PlaceLines()
        {
            int lLength = _allLine.Count;
            Transform lNoteLineTransform;
            Vector3 lLineOffset = Vector3.right * _lineDistance;

            for (int lCurrentLineIndex = 0; lCurrentLineIndex < lLength; lCurrentLineIndex++)
            {
                lNoteLineTransform = _allLine[lCurrentLineIndex].transform;
                lNoteLineTransform.position += -lLineOffset * .5f * (lLength - 1);
                lNoteLineTransform.position += lLineOffset * lCurrentLineIndex;
            }
        }
    }
}