using Com.IsartPesRhythmGame.Managers;
using Com.IsartPesRhythmGame.NotesSystem;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    // -------~~~~~~~~~~================# // Components
    private MidiManager _midiManager;

    // -------~~~~~~~~~~================# // Note Line
    [SerializeField] private float _lineDistance = 1f;
    [SerializeField] private Transform _lineParent;
    [SerializeField] private GameObject _lineFactory;
    private List<NoteLine> _allLine = new List<NoteLine>();

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
    private void Awake()
    {
        InitComponents();
    }

    private void Start()
    {
        PlaceNotes();
        PlaceNoteLine();
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
    private void InitComponents()
    {
        _midiManager = GetComponent<MidiManager>();
        if (!_lineParent) _lineParent = transform;
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Note
    private void PlaceNotes()
    {
        foreach (Note lCurrentNote in _midiManager.MidiNotes) PlaceNoteOnLine(lCurrentNote);
    }

    private void PlaceNoteOnLine(Note pNote)
    {
        NoteLine lNoteLine = FindOrCreateLine(pNote);
        lNoteLine.Add(pNote);
    }

    // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Note Line
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
            if (lCurrentLine.ID < lWantedID) return CreateNoteLineAt(lCurrentLineIndex, lWantedID);
        }

        // If Line Is Above, Then Create Line At The End Of The List
        return CreateNoteLineAt(lLength, lWantedID);
    }

    private NoteLine CreateNoteLineAt(int pIndex, int pID)
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

        return lNewLine;
    }

    private void PlaceNoteLine()
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
