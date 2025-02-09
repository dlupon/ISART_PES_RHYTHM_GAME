using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartPesRhythmGame.Managers
{
    public class MidiManager : MonoBehaviour
    {
        // -------~~~~~~~~~~================# // File
        [SerializeField] private string _fileLocation = "Assets/Assets/Midi/Into The Zone.mid";

        // -------~~~~~~~~~~================# // Midi
        private MidiFile _midiFile;
        private ICollection<Note> _midiNotes;

        public ICollection<Note> MidiNotes => _midiNotes;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            StoreFile();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // File

        private void StoreFile(string pFileLocation = "")
        {
            if (pFileLocation == "") pFileLocation = _fileLocation;

            _midiFile = MidiFile.Read(pFileLocation);
            _midiNotes = _midiFile.GetNotes();
        }
    }
}