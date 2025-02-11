using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Map : ScriptableObject
{
    // -------~~~~~~~~~~================# // Export Properties
    [Header("General Infos")]
    [SerializeField] private string _name;
    [SerializeField] private string _autor;
    [SerializeField] private string _designer;
    
    public string Name => _name;
    public string Autor => _autor;
    public string Deseigner => _designer;

    [Header("Song Properties")]
    [SerializeField] private int _bpm;
    [SerializeField] private string _midiPath;
    
    public int BPM => _bpm;
    public string MidiPath => _midiPath;

    // -------~~~~~~~~~~================# // Map
    // Midi
    public ICollection<Note> MidiNotes;
}
