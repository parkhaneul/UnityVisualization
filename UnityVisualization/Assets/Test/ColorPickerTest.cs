using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerTest : MonoBehaviour
{
	// Please assign a material that is using position and color.
	public Material material;

	public Rect position = new Rect(16, 16, 128, 24);
	public Color color = Color.red;

	void OnGUI()
	{
		DrawRectangle(position, color);
	}

	void DrawRectangle(Rect position, Color color)
	{
		// We shouldn't draw until we are told to do so
		if (Event.current.type != EventType.Repaint)
			return;

		// Please assign a material that is using position and color.
		if (material == null)
		{
			Debug.LogError("You have forgot to set a material.");
			return;
		}

		material.SetPass(0);

		// Optimization hint: 
		// Consider Graphics.DrawMeshNow
		GL.Color(color);
		GL.Begin(GL.QUADS);
		GL.Vertex3(position.x, position.y, 0);
		GL.Vertex3(position.x + position.width, position.y, 0);
		GL.Vertex3(position.x + position.width, position.y + position.height, 0);
		GL.Vertex3(position.x, position.y + position.height, 0);
		GL.End();
	}
}
