using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Com.IsartPesRhythmGame.Managers
{
    // Faut pas chercher Mehdi à 14 heure.
    public class MidiManager : Manager
    {
        // -------~~~~~~~~~~================# // Events
        public static UnityEvent<Note> NoteExtractedFromMidi = new UnityEvent<Note>();
        public static UnityEvent MidiExtractionComplited = new UnityEvent();

        // -------~~~~~~~~~~================# // Midi
        public const float BAR = 512;
        public const float BEAT = 128;
        private MidiFile _midiFile;
        private ICollection<Note> _midiNotes;

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Unity
        private void Awake()
        {
            StoreFile();
        }

        public void Start()
        {
            ExtractNotes();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // File
        private void StoreFile()
        {
            _midiFile = MidiFile.Read(m_map.MidiPath);
            _midiNotes = _midiFile.GetNotes();
        }

        // ----------------~~~~~~~~~~~~~~~~~~~==========================# // Note
        private void ExtractNotes()
        {
            foreach (Note lCurrentNote in _midiNotes)
            {
                NoteExtractedFromMidi.Invoke(lCurrentNote);
            }

            MidiExtractionComplited.Invoke();
        }
    }
}