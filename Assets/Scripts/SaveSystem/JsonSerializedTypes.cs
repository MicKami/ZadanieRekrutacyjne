using UnityEngine;

public static class JsonSerializedTypes
{
	public struct Vector3Json
	{
		public float x, y, z;

		public Vector3Json(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static implicit operator Vector3(Vector3Json v) => new(v.x, v.y, v.z);
		public static implicit operator Vector3Json(Vector3 v) => new(v.x, v.y, v.z);
	}

	public struct Vector4Json
	{
		public float x, y, z, w;

		public Vector4Json(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static implicit operator Quaternion(Vector4Json v) => new(v.x, v.y, v.z, v.w);
		public static implicit operator Vector4Json(Quaternion v) => new(v.x, v.y, v.z, v.w);
	}
}
