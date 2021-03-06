﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaneSpawner : MonoBehaviour {
	public GameObject prefab;
	public float m_maxTilt = 30f;
	public float m_smooth = 5f;

	float sigmoid(float x, float x_offset) {
		return (float)(1 / (1 + Math.Exp((double)(-x + x_offset))));
	}

	// Use this for initialization
	void Start () {
			StartCoroutine("SpawnPlane");
	}
	
	// Update is called once per frame
	void Update () {
		float x_tilt = 0;
		float z_tilt = 0;

		// Work on Forward / Backwards Tilt
		if(Input.GetKey("w")) {
			x_tilt = m_maxTilt * sigmoid(m_maxTilt/m_maxTilt * 6, 5);
		}
		else if(Input.GetKey("s")) {
			x_tilt = -1 * m_maxTilt * sigmoid(m_maxTilt/m_maxTilt * 6, 5);
		}

		// Work on Left / Right Tilt
		if(Input.GetKey("a")) {
			z_tilt = m_maxTilt * sigmoid(m_maxTilt/m_maxTilt * 6, 5);
		}
		else if(Input.GetKey("d")) {
			z_tilt = -1 * m_maxTilt * sigmoid(m_maxTilt/m_maxTilt * 6, 5);
		}
		
		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			Quaternion.Euler(x_tilt, transform.rotation.y, z_tilt),
			Time.deltaTime * m_smooth);	
	}

	IEnumerator SpawnPlane() {
		while(true)
		{
			Instantiate(prefab, transform.position - new Vector3(0, 0.057547f, 0), transform.rotation);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
