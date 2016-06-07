using UnityEngine;
using System.Collections;

public class BeatDetectionEngine : MonoBehaviour 
{
	public Octaves currentOctave;
	public FFTWindow transformWindow;
	public bool beatDetected { get; set; }
	[SerializeField] private float beatThreshold = .5f;
	private float[] spectrumData = new float[1024];
	public float frequencyData = 0.0f;
	private int frequency = 0;
	private AudioSource audioSource = null;

	public enum Octaves {C1 = 2, C2 = 11, C3 = 22, C4 = 44}

	void Awake()
	{
		foreach (Octaves octave in Octaves.GetValues(typeof(Octaves)))
		{
			if (octave == currentOctave)
				frequency = (int) octave;
		}
	}

	void Start()
	{
		audioSource = this.GetComponent<AudioSource> ();
	}
	
	void FixedUpdate()
	{
		/*Each index is a specific frequency that accounts for 21 hz (22050 / 1024 = ~21), from [0, 1024]. 
		 *This returns the volume level of frequency[i] as a float normalized between 0 and 1.*/
		audioSource.GetSpectrumData (spectrumData, 0, transformWindow);
	}

	void Update()
	{
		frequencyData = spectrumData [frequency] + spectrumData [frequency + 1] + spectrumData [frequency + 2] / 3;
		if (frequencyData > beatThreshold) 
		{
			beatDetected = true;
		} 
		else 
		{
			beatDetected = false;
		}
	}
}