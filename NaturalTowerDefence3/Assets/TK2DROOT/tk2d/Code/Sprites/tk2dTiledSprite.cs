using UnityEngine;
using System.Collections;

[AddComponentMenu("2D Toolkit/Sprite/tk2dTiledSprite")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
/// <summary>
/// Sprite implementation that tiles a sprite to fill given dimensions.
/// </summary>
public class tk2dTiledSprite : tk2dBaseSprite
{
	Mesh mesh;
	Vector2[] meshUvs;
	Vector3[] meshVertices;
	Color32[] meshColors;
	Vector3[] meshNormals = null;
	Vector4[] meshTangents = null;
	int[] meshIndices;
	
	[SerializeField]
	Vector2 _dimensions = new Vector2(50.0f, 50.0f);
	[SerializeField]
	Anchor _anchor = Anchor.LowerLeft;
	
	/// <summary>
	/// Gets or sets the dimensions.
	/// </summary>
	/// <value>
	/// Use this to change the dimensions of the sliced sprite in pixel units
	/// </value>
	public Vector2 dimensions
	{ 
		get { return _dimensions; } 
		set
		{
			if (value != _dimensions)
			{
				_dimensions = value;
				UpdateVertices();
#if UNITY_EDITOR
				EditMode__CreateCollider();
#endif
				UpdateCollider();
			}
		}
	}
	
	/// <summary>
	/// The anchor position for this tiled sprite
	/// </summary>
	public Anchor anchor
	{
		get { return _anchor; }
		set
		{
			if (value != _anchor)
			{
				_anchor = value;
				UpdateVertices();
#if UNITY_EDITOR
				EditMode__CreateCollider();
#endif
				UpdateCollider();
			}
		}
	}

	[SerializeField]
	protected bool _createBoxCollider = false;

	/// <summary>
	/// Create a trimmed box collider for this sprite
	/// </summary>
	public bool CreateBoxCollider {
		get { return _createBoxCollider; }
		set {
			if (_createBoxCollider != value) {
				_createBoxCollider = value;
				UpdateCollider();
			}
		}
	}
	
	new void Awake()
	{
		base.Awake();
		
		// Create mesh, independently to everything else
		mesh = new Mesh();
		mesh.hideFlags = HideFlags.DontSave;
		GetComponent<MeshFilter>().mesh = mesh;
		
		// This will not be set when instantiating in code
		// In that case, Build will need to be called
		if (Collection)
		{
			// reset spriteId if outside bounds
			// this is when the sprite collection data is corrupt
			if (_spriteId < 0 || _spriteId >= Collection.Count)
				_spriteId = 0;
			
			Build();
			
			if (boxCollider == null)
				boxCollider = GetComponent<BoxCollider>();
#if !(UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2)
			if (boxCollider2D == null) {
				boxCollider2D = GetComponent<BoxCollider2D>();
			}
#endif
		}
	}
	
	protected void OnDestroy()
	{
		if (mesh)
		{
#if UNITY_EDITOR
			DestroyImmediate(mesh);
#else
			Destroy(mesh);
#endif
		}
	}
	
	new protected void SetColors(Color32[] dest)
	{
		int numVertices;
		int numIndices;
		tk2dSpriteGeomGen.GetTiledSpriteGeomDesc(out numVertices, out numIndices, CurrentSprite, dimensions);
		tk2dSpriteGeomGen.SetSpriteColors (dest, 0, numVertices, _color, collectionInst.premultipliedAlpha);
	}
	
	// Calculated center and extents
	Vector3 boundsCenter = Vector3.zero, boundsExtents = Vector3.zero;


	
	public override void Build()
	{
		var spriteDef = CurrentSprite;
		int numVertices;
		int numIndices;
		tk2dSpriteGeomGen.GetTiledSpriteGeomDesc(out numVertices, out numIndices, spriteDef, dimensions);

		if (meshUvs == null || meshUvs.Length != numVertices) {
			meshUvs = new Vector2[numVertices];
			meshVertices = new Vector3[numVertices];
			meshColors = new Color32[numVertices];
		}
		if (meshIndices == null || meshIndices.Length != numIndices) {
			meshIndices = new int[numIndices];
		}

		meshNormals = new Vector3[0];
		meshTangents = new Vector4[0];
		if (spriteDef.normals != null && spriteDef.normals.Length > 0) {
			meshNormals = new Vector3[numVertices];
		}
		if (spriteDef.tangents != null && spriteDef.tangents.Length > 0) {
			meshTangents = new Vector4[numVertices];
		}

		float colliderOffsetZ = ( boxCollider != null ) ? ( boxCollider.center.z ) : 0.0f;
		float colliderExtentZ = ( boxCollider != null ) ? ( boxCollider.size.z * 0.5f ) : 0.5f;
		tk2dSpriteGeomGen.SetTiledSpriteGeom(meshVertices, meshUvs, 0, out boundsCenter, out boundsExtents, spriteDef, _scale, dimensions, anchor, colliderOffsetZ, colliderExtentZ);
		tk2dSpriteGeomGen.SetTiledSpriteIndices(meshIndices, 0, 0, spriteDef, dimensions);

		if (meshNormals.Length > 0 || meshTangents.Length > 0) {
			Vector3 meshVertexMin = new Vector3(spriteDef.positions[0].x * dimensions.x * spriteDef.texelSize.x * scale.x, spriteDef.positions[0].y * dimensions.y * spriteDef.texelSize.y * scale.y);
			Vector3 meshVertexMax = new Vector3(spriteDef.positions[3].x * dimensions.x * spriteDef.texelSize.x * scale.x, spriteDef.positions[3].y * dimensions.y * spriteDef.texelSize.y * scale.y);
			tk2dSpriteGeomGen.SetSpriteVertexNormals(meshVertices, meshVertexMin, meshVertexMax, spriteDef.normals, spriteDef.tangents, meshNormals, meshTangents);
		}
		
		SetColors(meshColors);
		
		if (mesh == null)
		{
			mesh = new Mesh();
			mesh.hideFlags = HideFlags.DontSave;
		}
		else
		{
			mesh.Clear();
		}
		mesh.vertices = meshVertices;
		mesh.colors32 = meshColors;
		mesh.uv = meshUvs;
		mesh.normals = meshNormals;
		mesh.tangents = meshTangents;
		mesh.triangles = meshIndices;
		mesh.RecalculateBounds();
		mesh.bounds = AdjustedMeshBounds( mesh.bounds, renderLayer );
		
		GetComponent<MeshFilter>().mesh = mesh;
		
		UpdateCollider();
		UpdateMaterial();
	}
	
	protected override void UpdateGeometry() { UpdateGeometryImpl(); }
	protected override void UpdateColors() { UpdateColorsImpl(); }
	protected override void UpdateVertices() { UpdateGeometryImpl(); }
	
	
	protected void UpdateColorsImpl()
	{
#if UNITY_EDITOR
		// This can happen with prefabs in the inspector
		if (meshColors == null || meshColors.Length == 0)
			return;
#endif
		if (meshColors == null || meshColors.Length == 0) {
			Build();
		}
		else {
			SetColors(meshColors);
			mesh.colors32 = meshColors;
		}
	}

	protected void UpdateGeometryImpl()
	{
#if UNITY_EDITOR
		// This can happen with prefabs in the inspector
		if (mesh == null)
			return;
#endif
		Build();
	}
	
#region Collider
	protected override void UpdateCollider()
	{
		if (CreateBoxCollider) {
			if (CurrentSprite.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics3D) {
				if (boxCollider != null) {
					boxCollider.size = 2 * boundsExtents;
					boxCollider.center = boundsCenter;
				}
			}
			else if (CurrentSprite.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D) {
#if !(UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2)
				if (boxCollider2D != null) {
					boxCollider2D.size = 2 * boundsExtents;
					boxCollider2D.center = boundsCenter;
				}
#endif
			}
		}
	}

#if UNITY_EDITOR
	void OnDrawGizmos() {
		if (mesh != null) {
			Bounds b = mesh.bounds;
			Gizmos.color = Color.clear;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawCube(b.center, b.extents * 2);
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.color = Color.white;
		}
	}
#endif

	protected override void CreateCollider() {
		UpdateCollider();
	}

#if UNITY_EDITOR
	public override void EditMode__CreateCollider() {
		if (CreateBoxCollider) {
			base.CreateSimpleBoxCollider();
		}

		UpdateCollider();
	}
#endif
#endregion	
	
	protected override void UpdateMaterial()
	{
		if (renderer.sharedMaterial != collectionInst.spriteDefinitions[spriteId].materialInst)
			renderer.material = collectionInst.spriteDefinitions[spriteId].materialInst;
	}
	
	protected override int GetCurrentVertexCount()
	{
#if UNITY_EDITOR
		if (meshVertices == null)
			return 0;
#endif
		return 16;
	}

	public override void ReshapeBounds(Vector3 dMin, Vector3 dMax) { // Identical to tk2dSlicedSprite.ReshapeBounds
		float minSizeClampTexelScale = 0.1f; // Can't shrink sprite smaller than this many texels
		// Irrespective of transform
		var sprite = CurrentSprite;
		Vector2 boundsSize = new Vector2(_dimensions.x * sprite.texelSize.x, _dimensions.y * sprite.texelSize.y);
		Vector3 oldSize = new Vector3(boundsSize.x * _scale.x, boundsSize.y * _scale.y);
		Vector3 oldMin = Vector3.zero;
		switch (_anchor) {
			case Anchor.LowerLeft: oldMin.Set(0,0,0); break;
			case Anchor.LowerCenter: oldMin.Set(0.5f,0,0); break;
			case Anchor.LowerRight: oldMin.Set(1,0,0); break;
			case Anchor.MiddleLeft: oldMin.Set(0,0.5f,0); break;
			case Anchor.MiddleCenter: oldMin.Set(0.5f,0.5f,0); break;
			case Anchor.MiddleRight: oldMin.Set(1,0.5f,0); break;
			case Anchor.UpperLeft: oldMin.Set(0,1,0); break;
			case Anchor.UpperCenter: oldMin.Set(0.5f,1,0); break;
			case Anchor.UpperRight: oldMin.Set(1,1,0); break;
		}
		oldMin = Vector3.Scale(oldMin, oldSize) * -1;
		Vector3 newScale = oldSize + dMax - dMin;
		newScale.x /= boundsSize.x;
		newScale.y /= boundsSize.y;
		// Clamp the minimum size to avoid having the pivot move when we scale from near-zero
		if (Mathf.Abs(boundsSize.x * newScale.x) < sprite.texelSize.x * minSizeClampTexelScale && Mathf.Abs(newScale.x) < Mathf.Abs(_scale.x)) {
			dMin.x = 0;
			newScale.x = _scale.x;
		}
		if (Mathf.Abs(boundsSize.y * newScale.y) < sprite.texelSize.y * minSizeClampTexelScale && Mathf.Abs(newScale.y) < Mathf.Abs(_scale.y)) {
			dMin.y = 0;
			newScale.y = _scale.y;
		}
		// Add our wanted local dMin offset, while negating the positional offset caused by scaling
		Vector2 scaleFactor = new Vector3(Mathf.Approximately(_scale.x, 0) ? 0 : (newScale.x / _scale.x),
			Mathf.Approximately(_scale.y, 0) ? 0 : (newScale.y / _scale.y));
		Vector3 scaledMin = new Vector3(oldMin.x * scaleFactor.x, oldMin.y * scaleFactor.y);
		Vector3 offset = dMin + oldMin - scaledMin;
		offset.z = 0;
		transform.position = transform.TransformPoint(offset);
		dimensions = new Vector2(_dimensions.x * scaleFactor.x, _dimensions.y * scaleFactor.y);
	}
}
