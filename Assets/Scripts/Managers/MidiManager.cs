using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Com.IsartPesRhythmGame.Managers
{
    public class MidiManager : MapRelated
    {
        // -------~~~~~~~~~~================# // Midi
        private MidiFile _midiFile;
        private ICollection<Note> _midiNotes;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            StoreFile();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // File
        private void StoreFile()
        {
            _midiFile = MidiFile.Read(m_map.MidiPath);
            _midiNotes = _midiFile.GetNotes();
            m_map.MidiNotes = _midiNotes;
        }
    }
}