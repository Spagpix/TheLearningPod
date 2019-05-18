using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MagicWand : MonoBehaviour
{

	public GlyphCanvas CurrentSpellCanvas = null;
	public SpellGlyph CurrentSpellGlyph = null;
	public SpellTarget CurrentSpellTarget = null;
	
	public bool IsActivated = false;

	public float ProjectionDistance = 5f;
	public float LiftThreshold = 0f;
    public float DistanceMoveSpeed = 0.4f;
    public float ObjectTargetDistance;
	
	public UnityEvent OnHover;
	public UnityEvent OnUnHover;

	private RaycastHit[] _hitCache;
	private LineRenderer _spellGlyph;
	private List<Vector3> _spellGlyphPoints;

	private int _currentAngle = -1;
	private int _requiredAngle = -1;
	private int _quadrantCount = 0;
	private bool _movingDown = false;

    private bool _isMovingTrackpad = false;
    private float _trackpadPosition;
	
	public void Awake()
	{
		_hitCache = new RaycastHit[1];
		OnHover.AddListener(HandleOnHover);
		OnUnHover.AddListener(HandleOnUnHover);
	}

	public void Update()
	{
		TryGetNewHoverTarget();
        MoveTarget();

		if (CurrentSpellGlyph != null) DoGlyph();
		else if (CurrentSpellTarget != null) MoveTarget();
	}

	private void DoGlyph()
	{
		var localPoint = CurrentSpellGlyph.AddPoint();
			
		if(!_movingDown) {
			var angle = Mathf.RoundToInt(Mathf.Atan2(localPoint.y, localPoint.x) * Mathf.Rad2Deg);
			angle -= 90;
			angle %= 360;
			while (angle < 360) angle += 360;
			while (angle > 360) angle -= 360;

			var first = _currentAngle == -1;
			_currentAngle = angle - (angle % 90);
			if (first) {
				_currentAngle = (angle - (angle % 90));
				_requiredAngle = _currentAngle;// - 90;
				while (_requiredAngle < 0) _requiredAngle += 360;
				while (_requiredAngle > 360) _requiredAngle -= 360;
			} else {
				if ((angle < _requiredAngle) && _requiredAngle != 0) {
					_requiredAngle -= 90;
					_quadrantCount++;
	
					if (_quadrantCount > 3 && (angle > 270 || angle < 90)) {
						_movingDown = true;
					}
						
				} else if (angle > 270 && _requiredAngle == 0) {
					_requiredAngle = 270;
				}
			}
			
			
			//FindObjectOfType<Text>().text = $"Angle: {angle.ToString("0°")}\n_currentAngle: {_currentAngle}\n_requiredAngle: {_requiredAngle}\n_quadrantCount: {_quadrantCount}\n_movingDown: {_movingDown}";
		} else {
			if (localPoint.y < LiftThreshold) {
				CurrentSpellTarget = CurrentSpellGlyph.GlyphCanvas.SpellTarget;
				CurrentSpellTarget.Activate(this);
				Destroy(CurrentSpellGlyph.gameObject);
				CurrentSpellGlyph = null;
			}
		}
	}

    public void ReceiveTrackpad(float value)
    {
        _trackpadPosition = Mathf.Lerp(1f, -1f, value);
    }

    public void StartTrackpad()
    {
        _isMovingTrackpad = true;
    }

    public void StopTrackpad()
    {
        _isMovingTrackpad = false;
    }

	private void MoveTarget()
	{
        if (!_isMovingTrackpad) return;

        ObjectTargetDistance += DistanceMoveSpeed * Time.deltaTime * _trackpadPosition;
        ObjectTargetDistance = Mathf.Clamp(ObjectTargetDistance, 0.5f, 10f);
	}

	public void Activate()
	{
		if (CurrentSpellCanvas == null) return;

		StartDrawingGlyph();

		IsActivated = true;
	}

	public void Deactivate()
	{
		if (CurrentSpellGlyph != null) {
			Destroy(CurrentSpellGlyph.gameObject);
			CurrentSpellGlyph = null;
		}

		if (CurrentSpellTarget != null) {
			CurrentSpellTarget.Deactivate();
			CurrentSpellTarget = null;
		}

		_currentAngle = _requiredAngle = -1;
		_quadrantCount = 0;
		_movingDown = false;

		IsActivated = false;
	}
	
	private void TryGetNewHoverTarget()
	{
		if (Physics.RaycastNonAlloc(transform.position, transform.forward, _hitCache, float.MaxValue,
			    LayerMask.GetMask(new[] {"GlyphCanvas"})) <= 0) {
			if (CurrentSpellCanvas == null) return;
			
			OnUnHover.Invoke();
			CurrentSpellCanvas = null;
			return;
		}
		
		if (CurrentSpellCanvas == null) {
			CurrentSpellCanvas = _hitCache[0].collider.gameObject.GetComponent<GlyphCanvas>();
			OnHover.Invoke();
		} else if (CurrentSpellCanvas != _hitCache[0].collider.gameObject) {
			OnUnHover.Invoke();
			CurrentSpellCanvas = _hitCache[0].collider.gameObject.GetComponent<GlyphCanvas>();
			OnHover.Invoke();
		}
	}

	private void HandleOnHover()
	{
		
	}

	private void HandleOnUnHover()
	{
		
	}

	public void StartDrawingGlyph()
	{
		CurrentSpellGlyph = ((GameObject)Instantiate(Resources.Load("SpellGlyph"))).GetComponent<SpellGlyph>();
		CurrentSpellGlyph.Initialize(this, CurrentSpellCanvas);
	}
}
