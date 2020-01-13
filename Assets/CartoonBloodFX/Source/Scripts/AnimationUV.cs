using UnityEngine;
using System.Collections;

namespace HWREfx
{
	public class AnimationUV : MonoBehaviour
	{


	
		public int uvAnimationTileX = 24;
		public int uvAnimationTileY = 1;
		public Vector2[] PackOffset;
		public int IndexPack = 0;
		public float framesPerSecond = 60;
		public bool loop;
		public bool play = true;
		public bool RandomAnimation = true;
		public Vector2 offset;
		private MeshFilter mesh;
		private Vector2[] uvs;
		private Vector2[] originalUVs;
		public int IndexLock = -1;
	
		void Start ()
		{
	
			if (RandomAnimation) {
				offset.x = Random.Range (0, uvAnimationTileX);
				offset.y = -Random.Range (0, uvAnimationTileY);
			}
		
			mesh = GetComponent<MeshFilter> ();
			originalUVs = new Vector2[mesh.mesh.uv.Length];
			originalUVs = mesh.mesh.uv;

			uvs = new Vector2[mesh.mesh.uv.Length];

			for (int i = 0; i<mesh.mesh.uv.Length; i++) {
				if (mesh.mesh.uv [i].x > 0 && mesh.mesh.uv [i].y < 1 && mesh.mesh.uv [i].x < 1 && mesh.mesh.uv [i].y > 0) {
					mesh.mesh.uv [i].x = 1 - mesh.mesh.uv [i].x;
					mesh.mesh.uv [i].y = 1 - mesh.mesh.uv [i].y;
				}
			}
			if (PackOffset.Length > 0) {
				if (IndexPack >= PackOffset.Length)
					IndexPack = PackOffset.Length - 1;
				for (int j = 0; j<mesh.mesh.uv.Length; j++) {
					uvs [j] = originalUVs [j] + (offset + new Vector2 (PackOffset [IndexPack].x, -PackOffset [IndexPack].y));
				}
			}

			mesh.mesh.uv = uvs;
		
		}

		private float timeFPS = 0;
	
		void Update ()
		{
			if (IndexLock != -1) {
				IndexPack = IndexLock;
			}
			if (play) {
				if (timeFPS <= 0) {
					offset.x += 1;
					if (offset.x >= uvAnimationTileX) {
						offset.x = 0;
						offset.y -= 1;	
					}
					if (offset.y <= -(uvAnimationTileY) - 1) {
						if (!loop) {
							offset.x = uvAnimationTileX - 1;
							offset.y += 1;
							play = false;
						} else {
							offset.x = 0;
							offset.y = -1;
						}
					
					}
					timeFPS = 60 * Time.deltaTime;
				} else {
					timeFPS -= framesPerSecond * Time.deltaTime;	
				}
			}
			if (PackOffset.Length > 0) {
				if (IndexPack >= PackOffset.Length)
					IndexPack = PackOffset.Length - 1;
				for (int i = 0; i<mesh.mesh.uv.Length; i++) {
					uvs [i] = originalUVs [i] + (offset + new Vector2 (PackOffset [IndexPack].x, -PackOffset [IndexPack].y));
				}
			}

			mesh.mesh.uv = uvs;
		}
	
	}
}