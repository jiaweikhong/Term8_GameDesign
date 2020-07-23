using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpecialPotsSFX")]
public class SpecialPotsSFX : ScriptableObject
{
    [SerializeField]
    private AudioClip swiftnessElixirSFX;
    [SerializeField]
    private AudioClip killerBrewSFX;
    [SerializeField]
    private AudioClip muddlingMistSFX;
    [SerializeField]
    private AudioClip dreamDustSFX;

    public AudioClip SwiftnessElixirSFX
    {
        get
        { return swiftnessElixirSFX; }
    }

    public AudioClip KillerBrewSFX
    {
        get
        { return killerBrewSFX; }
    }
    public AudioClip MuddlingMistSFX
    {
        get
        { return muddlingMistSFX; }
    }
    public AudioClip DreamDustSFX
    {
        get
        { return dreamDustSFX; }
    }
}
