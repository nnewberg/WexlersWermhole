using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DepthKit;


public class DepthKitClipController : MonoBehaviour {

    public GameObject[] _models;
    protected int _activeModel = 0;
    protected Clip _activeClip;

    void OnEnable()
    {
        for (int i = 0; i < _models.Length; i++)
        {
            if (i == 0)
            {
                _models[i].SetActive(true);
            }

            else
            {
                _models[i].SetActive(false);
            }
        }
    }

    public void PlayClip(int index)
    {
        if(_activeModel != index)
        {
            pauseMovie(_activeModel);
            if (index < _models.Length)
            {
                _activeModel = index;
                activateMovie(_activeModel);
            }
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            pauseMovie(_activeModel);
            if ((_activeModel + 1) < _models.Length)
            {
                _activeModel++;
            }
            else
            {
                _activeModel = 0;
            }
            activateMovie(_activeModel);
        }

        if (_activeModel != 0)
        {
            if (!_activeClip.Controller.IsPlaying())
            {
                PlayClip(0);
            }
        }
    }

    protected void pauseMovie(int index)
    {
        Clip[] clips = _models[_activeModel].GetComponentsInChildren<Clip>();
        foreach (Clip clip in clips)
        {
            clip.Controller.Pause();
        }
        _models[_activeModel].SetActive(false);
    }

    protected void activateMovie(int index)
    {
        Clip[] clips = _models[_activeModel].GetComponentsInChildren<Clip>();
        _models[_activeModel].SetActive(true);
        foreach (Clip clip in clips)
        {
            _activeClip = clip;
            clip.Controller.Play();
        }
    }
}
