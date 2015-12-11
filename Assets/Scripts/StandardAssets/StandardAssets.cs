using UnityEngine;
using System;
using System.Collections.Generic;

namespace StandardAssets{
	public static class Meshwork {
		public class Face {
			public Vector3[] tris;
			public Vector3[] verts;

			public int vert {
				get { return verts.Length; }
			}

			public Face(Vector3[] tris, Vector3[] verts) {
				this.tris = tris;
				this.verts = verts;
			}
			public Face() {

			}

		}

		[Serializable]
		public class Triangle {
			[SerializeField]
			Face face;

			public int vertex;

			public int one {
				get {
					return (int)face.tris[0].x;
				}
			}
			public int two {
				get {
					return (int)face.tris[0].y;
				}
			}
			public int three {
				get {
					return (int)face.tris[0].z;
				}
			}

			public Vector3 width {

				get { return face.verts[0]; }
			}
			public Vector3 height {

				get { return face.verts[1]; }
			}
			public Vector3 length {

				get { return face.verts[2]; }
			}

			public int[] tris {
				get {
					return new int[] { one, two, three };
				}
			}
			public Vector3[] verts {
				get { return new Vector3[] { width, height, length }; }
			}


			[Serializable]
			public class TooManyVerticies : Exception {
				public TooManyVerticies() { }
				public TooManyVerticies(string message) : base(message) { }
				public TooManyVerticies(string message, Exception inner) : base(message, inner) { }
				protected TooManyVerticies(
				  System.Runtime.Serialization.SerializationInfo info,
				  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
			}


			/* Deprecated
			/// <summary>
			/// Creates a triangle with the face points one,two three and the verts w,h.
			/// The Third Vertex is being calculated
			/// </summary>
			/// <param name="one">First Vertex</param>
			/// <param name="two">Second Vertex</param>
			/// <param name="three">Third Vertex</param>
			/// <param name="width">Vertex one</param>
			/// <param name="height">Vertex two</param>
			/// 
			public Triangle(int one, int two, int three, Vector3 width, Vector3 height) {
				tri = new Vector3(one,two,three);
				vertOne = width;
				vertTwo = height;
				vertThree = length;
				vertex = 3;
			}*/
			/// <summary>
			/// Creates a triangle with a Face off face
			/// </summary>
			/// <param name="face">The Face in form of 3 Verts</param>
			public Triangle(Face face) {
				if (face.vert != 3)
					throw (new TooManyVerticies("There are to many Verticies in the given Face: " + face.vert));
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="pos">The local Position in the mesh</param>
			/// <param name="wh">The width and height in a Vector2(w,h)</param>
			public Triangle(Vector3 pos, Vector2 wh) {
				Face tFace = new Face();
				tFace.verts = new Vector3[3] { pos - Vector3.one / 2, new Vector3(pos.x + wh.x, pos.y, pos.z) - Vector3.one / 2, new Vector3(pos.x, pos.y + wh.y, pos.z) - Vector3.one / 2 };
				tFace.tris = new Vector3[] { new Vector3(0, 2, 1) };
				face = tFace;
			}
			/* A Face
			/// <summary>
			/// Creates a triangle/face with the triangle points one,two three and the verts w,h,l
			/// </summary>
			/// <param name="one">First Vertex</param>
			/// <param name="two">Second Vertex</param>
			/// <param name="three">Third Vertex</param>
			public Triangle(int one, int two, int three) {
				tri = new Vector3(one, two, three);
				vertex = 0;
			}
			/// <summary>
			/// Creates a triangle/face with the triangle points one,two three inside a Vector3 and the verts in an array
			/// </summary>
			/// <param name="tri">The triangle in form of(first Vertex, second Vertex, third Vertex)</param>
			public Triangle(Vector3 tri) {
				this.tri = tri;
				vertex = 0;
			}
			*/

		}

		[Serializable]
		public class Rectangle {
			[SerializeField]
			public Face face;

			public int vertex;

			public int one {
				get {
					return (int)face.tris[0].x;
				}
			}
			public int two {
				get {
					return (int)face.tris[0].y;
				}
			}
			public int three {
				get {
					return (int)face.tris[0].z;
				}
			}
			public int four {
				get {
					return (int)face.tris[1].x;
				}
			}
			public int five {
				get {
					return (int)face.tris[1].y;
				}
			}
			public int six {
				get {
					return (int)face.tris[1].z;
				}
			}

			public Vector3 width {

				get { return face.verts[0]; }
			}
			public Vector3 height {

				get { return face.verts[1]; }
			}
			public Vector3 length {

				get { return face.verts[2]; }
			}

			public int[] tris {
				get {
					return new int[] { one, two, three };
				}
			}
			public Vector3[] verts {
				get { return new Vector3[] { width, height, length }; }
			}


			[Serializable]
			public class TooManyVerticies : Exception {
				public TooManyVerticies() { }
				public TooManyVerticies(string message) : base(message) { }
				public TooManyVerticies(string message, Exception inner) : base(message, inner) { }
				protected TooManyVerticies(
				  System.Runtime.Serialization.SerializationInfo info,
				  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
			}


			/* Deprecated
			/// <summary>
			/// Creates a triangle with the face points one,two three and the verts w,h.
			/// The Third Vertex is being calculated
			/// </summary>
			/// <param name="one">First Vertex</param>
			/// <param name="two">Second Vertex</param>
			/// <param name="three">Third Vertex</param>
			/// <param name="width">Vertex one</param>
			/// <param name="height">Vertex two</param>
			/// 
			public Triangle(int one, int two, int three, Vector3 width, Vector3 height) {
				tri = new Vector3(one,two,three);
				vertOne = width;
				vertTwo = height;
				vertThree = length;
				vertex = 3;
			}*/
			/// <summary>
			/// Creates a triangle with a Face off face
			/// </summary>
			/// <param name="face">The Face in form of 3 Verts</param>
			public Rectangle(Face face) {
				if (face.vert != 4)
					throw (new TooManyVerticies("There are to many Verticies in the given Face: " + face.vert));
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="pos">The local Position in the mesh</param>
			/// <param name="wh">The width and height in a Vector2(w,h)</param>
			public Rectangle(Vector3 pos, Vector2 wh) {
				Face tFace = new Face();
				tFace.verts = new Vector3[4] { pos - Vector3.one / 2, new Vector3(pos.x + wh.x, pos.y, pos.z) - Vector3.one / 2, new Vector3(pos.x, pos.y + wh.y, pos.z) - Vector3.one / 2, new Vector3(pos.x + wh.x, pos.y + wh.y, pos.z) - Vector3.one / 2 };
				tFace.tris = new Vector3[] { new Vector3(0, 2, 1), new Vector3(1, 2, 3) };
				face = tFace;
			}


		}

		public static int[] ConvertRectangleToInt(Rectangle[] rectangle, int startingPoint) {
			if (rectangle == null)
				return new int[0];
			if (rectangle.Length == 0)
				return new int[0];

			int[] temp = new int[rectangle.Length * 6];

			for (int i = 0; i < rectangle.Length; i++) {
				temp[0 + i * 6] = rectangle[i].one + i * 3 + startingPoint + (i != 0 ? i : 0);
				temp[1 + i * 6] = rectangle[i].two + i * 3 + startingPoint + (i != 0 ? i : 0);
				temp[2 + i * 6] = rectangle[i].three + i * 3 + startingPoint + (i != 0 ? i : 0);
				temp[3 + i * 6] = rectangle[i].four + i * 3 + startingPoint + (i != 0 ? i : 0);
				temp[4 + i * 6] = rectangle[i].five + i * 3 + startingPoint + (i != 0 ? i : 0);
				temp[5 + i * 6] = rectangle[i].six + i * 3 + startingPoint + (i != 0 ? i : 0);
			}

			return temp;


		}
		public static Vector3[] ConvertRectangleToVector3(Rectangle[] rectangle) {
			if (rectangle == null)
				return new Vector3[0];
			if (rectangle.Length == 0)
				return new Vector3[0];

			Vector3[] temp = new Vector3[rectangle.Length * 4];

			for (int i = 0; i < rectangle.Length; i++) {
				temp[0 + i * 4] = rectangle[i].face.verts[0];//Quaternion.AngleAxis(45, Vector3.up) * rectangle[i].width + (Quaternion.AngleAxis(45,Vector3.right) * rectangle[i].width);
				temp[1 + i * 4] = rectangle[i].face.verts[1]; ;//Quaternion.AngleAxis(45, Vector3.up) * rectangle[i].height + (Quaternion.AngleAxis(45, Vector3.right) * rectangle[i].height);
				temp[2 + i * 4] = rectangle[i].face.verts[2]; ;//Quaternion.AngleAxis(45, Vector3.up) * rectangle[i].length + (Quaternion.AngleAxis(45, Vector3.right) * rectangle[i].length);
				temp[3 + i * 4] = rectangle[i].face.verts[3]; ;//Quaternion.AngleAxis(45, Vector3.up) * rectangle[i].length + (Quaternion.AngleAxis(45, Vector3.right) * rectangle[i].length + Quaternion.AngleAxis(45, Vector3.right) * rectangle[i].height);
			}

			return temp;


		}

		public static int[] ConvertTriangleToInt(Triangle[] triangles) {
			if (triangles == null)
				return new int[0];
			if (triangles.Length == 0)
				return new int[0];
			int[] temp = new int[triangles.Length * 3];

			for (int i = 0; i < triangles.Length; i++) {
				temp[0 + i * 3] = triangles[i].one + i * 3;
				temp[1 + i * 3] = triangles[i].two + i * 3 + (i != 0 ? -1 : 0);
				temp[2 + i * 3] = triangles[i].three + i * 3 + (i != 0 ? +1 : 0); ;
			}

			return temp;


		}
		public static Vector3[] ConvertTriangleToVector3(Triangle[] triangles) {
			if (triangles == null)
				return new Vector3[0];
			if (triangles.Length == 0)
				return new Vector3[0];

			Vector3[] temp = new Vector3[triangles.Length * 3];

			for (int i = 0; i < triangles.Length; i++) {
				temp[0 + i * 3] = triangles[i].width;
				temp[1 + i * 3] = triangles[i].height;
				temp[2 + i * 3] = triangles[i].length;
			}

			return temp;


		}

		public static array[] CombineArrays<array>(array[] one, array[] two) {
			array[] tArray = new array[one.Length + two.Length];

			int i = 0;
			for (; i < one.Length; i++) {
				tArray[i] = one[i];
			}
			for (; i < tArray.Length; i++) {
				tArray[i] = two[i - one.Length];
			}


			return tArray;
		}

	}

	public static class Maths {
		public static float PixelToRect(float y) {
			return (Screen.height - y);
		}
	}

	public class MeshCreator {
		protected Vector3[] verts;
		protected int[] tris;
		public Meshwork.Triangle[] triangles = new Meshwork.Triangle[0];
		public Meshwork.Rectangle[] rectangles = new Meshwork.Rectangle[0];
		protected Vector2[] uvs;
		protected Vector3[] norm;

		public Vector3[] Vertices {
			get {
				return Meshwork.CombineArrays(Meshwork.ConvertTriangleToVector3(triangles), Meshwork.ConvertRectangleToVector3(rectangles));
			}
		}
		public int[] Triangles {
			get {
				return Meshwork.CombineArrays(Meshwork.ConvertTriangleToInt(triangles), Meshwork.ConvertRectangleToInt(rectangles, triangles.Length * 3));
			}
		}
		public Vector2[] UVs {
			get {
				return uvs;
			}
		}
		public Vector3[] Normals {
			get {
				return norm;
			}
		}
	}

	[Serializable]
	public class FloorUpMeshCreator : MeshCreator {




		public FloorUpMeshCreator() {

			verts = new Vector3[6];

			verts[0] = new Vector3(0, 0, 0) - Vector3.one / 2;
			verts[1] = new Vector3(1, 0, 0) - Vector3.one / 2;
			verts[2] = new Vector3(1, 1, 0) - Vector3.one / 2;

			verts[3] = new Vector3(0, 0, 1) - Vector3.one / 2;
			verts[4] = new Vector3(1, 0, 1) - Vector3.one / 2;
			verts[5] = new Vector3(1, 1, 1) - Vector3.one / 2;


			triangles = new Meshwork.Triangle[2];

			triangles[0] = new Meshwork.Triangle(Vector3.zero, Vector2.one);
			triangles[1] = new Meshwork.Triangle(Vector3.forward, Vector2.one);

			rectangles = new Meshwork.Rectangle[2];

			rectangles[0] = new Meshwork.Rectangle(Vector3.one * .5f, Vector2.one);

			rectangles[0].face.verts = new Vector3[] { new Vector3(0.5f, -.5f, -.5f), new Vector3(0.5f, -.5f, .5f), new Vector3(-0.5f, .5f, -.5f), new Vector3(-0.5f, .5f, .5f) };

			rectangles[1] = new Meshwork.Rectangle(new Vector3(0,-.5f - 9.5f,0), new Vector2(1,10));

			//rectangles[0].face.verts = new Vector3[] { new Vector3(0.5f, -.5f, -.5f), new Vector3(0.5f, -.5f, .5f), new Vector3(-0.5f, .5f, -.5f), new Vector3(-0.5f, .5f, .5f) };

			//triangles[0] = new Triangle(0, 2, 1);
			//triangles[1] = new Triangle(3, 4, 5);
			//triangles[2] = new Triangle(3, 2, 0);
			//triangles[3] = new Triangle(3, 5, 2);


			norm = new Vector3[6];
			


		}
	}

	[Serializable]
	public class FloorDownMeshCreator : MeshCreator {

		public FloorDownMeshCreator() {

			verts = new Vector3[6];

			verts[0] = new Vector3(0, 0, 0) - Vector3.one / 2;
			verts[1] = new Vector3(1, 0, 0) - Vector3.one / 2;
			verts[2] = new Vector3(1, 1, 0) - Vector3.one / 2;

			verts[3] = new Vector3(0, 0, 1) - Vector3.one / 2;
			verts[4] = new Vector3(1, 0, 1) - Vector3.one / 2;
			verts[5] = new Vector3(1, 1, 1) - Vector3.one / 2;


			triangles = new Meshwork.Triangle[2];

			triangles[0] = new Meshwork.Triangle(Vector3.forward + Vector3.right, Vector2.left + Vector2.up);
			triangles[1] = new Meshwork.Triangle(Vector3.zero + Vector3.right, Vector2.left + Vector2.up);

			rectangles = new Meshwork.Rectangle[2];

			rectangles[0] = new Meshwork.Rectangle(Vector3.one * .5f, Vector2.one);

			rectangles[0].face.verts = new Vector3[] { new Vector3(-0.5f, -.5f, -.5f), new Vector3(0.5f, .5f, -.5f), new Vector3(-0.5f, -.5f, .5f), new Vector3(0.5f, .5f, .5f) };

			rectangles[1] = new Meshwork.Rectangle(new Vector3(0, -.5f - 9.5f, 0), new Vector2(1, 10));

			//rectangles[0].face.verts = new Vector3[] { new Vector3(0.5f, -.5f, -.5f), new Vector3(0.5f, -.5f, .5f), new Vector3(-0.5f, .5f, -.5f), new Vector3(-0.5f, .5f, .5f) };

			//triangles[0] = new Triangle(0, 2, 1);
			//triangles[1] = new Triangle(3, 4, 5);
			//triangles[2] = new Triangle(3, 2, 0);
			//triangles[3] = new Triangle(3, 5, 2);


			norm = new Vector3[6];



		}
	}

	[Serializable]
	public class FloorNormalMeshCreator : MeshCreator {

		public FloorNormalMeshCreator() {

			verts = new Vector3[6];

			verts[0] = new Vector3(0, 0, 0) - Vector3.one / 2;
			verts[1] = new Vector3(1, 0, 0) - Vector3.one / 2;
			verts[2] = new Vector3(1, 1, 0) - Vector3.one / 2;

			verts[3] = new Vector3(0, 0, 1) - Vector3.one / 2;
			verts[4] = new Vector3(1, 0, 1) - Vector3.one / 2;
			verts[5] = new Vector3(1, 1, 1) - Vector3.one / 2;


			rectangles = new Meshwork.Rectangle[4];

			rectangles[0] = new Meshwork.Rectangle(new Vector3(0,0, 1), Vector2.one);
			rectangles[1] = new Meshwork.Rectangle(new Vector3(0,0, 0), Vector2.one);
			rectangles[2] = new Meshwork.Rectangle(new Vector3(0, -.5f - 9.5f, 0), new Vector2(1, 10));

			rectangles[3] = new Meshwork.Rectangle(Vector3.one * .5f, Vector2.one);
			//rectangles[3].face.verts = new Vector3[] { new Vector3(-.5f,.5f,-.5f), new Vector3(.5f,.5f,-.5f), new Vector3(.5f,.5f,.5f), new Vector3(-.5f,.5f,.5f) };

			rectangles[3].face.verts = new Vector3[] { new Vector3(-0.5f, .5f, -.5f), new Vector3(0.5f, .5f, -.5f), new Vector3(-0.5f, .5f, .5f), new Vector3(0.5f, .5f, .5f) };



			//rectangles[0].face.verts = new Vector3[] { new Vector3(0.5f, -.5f, -.5f), new Vector3(0.5f, -.5f, .5f), new Vector3(-0.5f, .5f, -.5f), new Vector3(-0.5f, .5f, .5f) };

			//triangles[0] = new Triangle(0, 2, 1);
			//triangles[1] = new Triangle(3, 4, 5);
			//triangles[2] = new Triangle(3, 2, 0);
			//triangles[3] = new Triangle(3, 5, 2);


			norm = new Vector3[6];



		}
	}

















}
