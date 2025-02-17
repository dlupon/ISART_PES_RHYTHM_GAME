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
    [SerializeField] private float _bpm;
    [SerializeField] private string _midiPath;
    [SerializeField] private AudioClip _song;
    
    public float BPM => _bpm;
    public string MidiPath => _midiPath;
    public AudioClip Song => _song;

    [Header("Notes")]
    [SerializeField] private float _lengthMultipliyer;
    [HideInInspector] public float LengthMultipliyer => _lengthMultipliyer;

    // -------~~~~~~~~~~================# // Map
    // Midi
    [HideInInspector] public int MidiSegment;
    [HideInInspector] public float Length;
    [HideInInspector] public float LengthRatio;

    // Song
    [HideInInspector] public float SongRatio;
}