using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DepthKit;
public class PlayDepthKitClip : MonoBehaviour {

    public DepthKitClipController DepthKitClipController;
    public int ClipIndex = 0;

	void Awake()
    {
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onPickUp.AddListener(Play);
        obj.onGazeTrigger.AddListener(Play);
    }

    private void Play()
    {
        DepthKitClipController.PlayClip(ClipIndex);
        this.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

    }
}
