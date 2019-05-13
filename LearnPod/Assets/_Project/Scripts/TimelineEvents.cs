using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineEvents : MonoBehaviour
{

	[System.Serializable]
	public class UnityFloatEvent : UnityEvent<float> { }

	public UnityEvent OnTimelineStart;
    public UnityFloatEvent OnTimelineTimeUpdate;
    public UnityEvent OnTimelinePause;
    public UnityEvent OnTimelineEnd;

    private double _lastTime;
    private bool _lastIsPlaying;
    private PlayableDirector _playableDirector;

 	protected virtual void Awake()
    {
	    _playableDirector = GetComponent<PlayableDirector>();
	    _playableDirector.played += HandlePlayed;
	    _playableDirector.paused += HandlePaused;
	    _playableDirector.stopped += HandleStopped;
    }

 	protected virtual void Update()
 	{
	    if (!(Math.Abs(_playableDirector.time - _lastTime) > 0.01f)) return;
	    
	    OnTimelineTimeUpdate.Invoke((float)(_playableDirector.time / _playableDirector.duration));
	    _lastTime = _playableDirector.time;
    }

    private void HandlePlayed(PlayableDirector director)
    {
	    OnTimelineStart.Invoke();
    }

    private void HandlePaused(PlayableDirector director)
    {
	    OnTimelineEnd.Invoke();
    }
    
    private void HandleStopped(PlayableDirector director)
    {
	    OnTimelineEnd.Invoke();
    }
}
