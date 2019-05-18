using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGlyph : MonoBehaviour
{

	public float LerpSpeed = 4f;

	private LineRenderer _lineRenderer;
	private List<Vector3> _linePoints;

	private MagicWand _magicWand;
	public GlyphCanvas GlyphCanvas;

	private bool _initialized;

	private Vector3 _wandPos;
	private Vector3 _wandForward;

	public void Initialize(MagicWand magicWand, GlyphCanvas glyphCanvas)
	{
		_lineRenderer = GetComponent<LineRenderer>();
		_linePoints = new List<Vector3>();

		_magicWand = magicWand;
		GlyphCanvas = glyphCanvas;

		_wandPos = _magicWand.transform.position;
		_wandForward = _magicWand.transform.forward;
		
		_initialized = true;
	}

	public Vector3 AddPoint()
	{
		if (!_initialized) return Vector3.zero;

		_wandPos = Vector3.Lerp(_wandPos, _magicWand.transform.position, Time.deltaTime * LerpSpeed);
		_wandForward = Vector3.Lerp(_wandForward, _magicWand.transform.forward, Time.deltaTime * LerpSpeed);
		
		var planePos = Vector3.ProjectOnPlane(
			_wandPos + _wandForward * _magicWand.ProjectionDistance,
			GlyphCanvas.transform.forward
		);
		var dist = planePos.magnitude;
		var clampedPlanePos = planePos.normalized * 
		                      GlyphCanvas.ProjectionScale * 
		                      Mathf.Clamp(dist, 0f, GlyphCanvas.MaxProjectedDistance);

		clampedPlanePos += GlyphCanvas.transform.position;
		_linePoints.Add(clampedPlanePos);

		_lineRenderer.positionCount = _linePoints.Count;
		_lineRenderer.SetPositions(_linePoints.ToArray());

		return GlyphCanvas.transform.InverseTransformPoint(clampedPlanePos);
	}
}
