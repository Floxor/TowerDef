using UnityEngine;
using System.Collections;

public static class PersonalMethods {

	#region Transform

	public static Transform ClearChildren(this Transform transform) {
		foreach(Transform child in transform) {
			if(Application.isPlaying) {
				GameObject.Destroy(child.gameObject);
			} else {
				GameObject.DestroyImmediate(child.gameObject);
			}
		}
		return transform;
	}

	public static Transform ClearChildrenExceptZero(this Transform transform) {
		foreach(Transform child in transform) {
			if(transform.GetChild(0) != child) {
				GameObject.Destroy(child.gameObject);
			}
		}
		return transform;
	}

	public static Transform FindDeepChild(this Transform p_parent, string p_name) {
		Transform result = p_parent.Find(p_name);
		if(result != null)
			return result;
		foreach(Transform child in p_parent) {
			result = child.FindDeepChild(p_name);
			if(result != null) {
				return result;
			}
		}
		return null;
	}


	public static IEnumerator MyLerp(Transform p_origin, Vector3 p_dest, float p_time) {
		float currentLerpTime = 0;
		float perc = 0;
		while(currentLerpTime <= p_time) {
			currentLerpTime += Time.deltaTime;
			perc = currentLerpTime / p_time;
			p_origin.position = Vector3.Lerp(p_origin.position, p_dest, perc);
			yield return new WaitForEndOfFrame();
		}
	}
	public static IEnumerator MyLerp(Transform p_origin, Quaternion p_dest, float p_time) {
		float currentLerpTime = 0;
		float perc = 0;
		while(currentLerpTime <= p_time) {
			currentLerpTime += Time.deltaTime;
			perc = currentLerpTime / p_time;
			p_origin.rotation = Quaternion.Lerp(p_origin.rotation, p_dest, perc);
			yield return new WaitForEndOfFrame();
		}
	}

	public static IEnumerator MySlerp(Transform p_origin, Vector3 p_dest, float p_time) {
		float currentLerpTime = 0;
		float perc = 0;
		while(currentLerpTime <= p_time) {
			currentLerpTime += Time.deltaTime;
			perc = currentLerpTime / p_time;
			p_origin.position = Vector3.Slerp(p_origin.position, p_dest, perc);
			yield return new WaitForEndOfFrame();
		}
	}

	public static IEnumerator MySlerp(Transform p_origin, Quaternion p_dest, float p_time) {
		float currentLerpTime = 0;
		float perc = 0;
		while(currentLerpTime <= p_time) {
			currentLerpTime += Time.deltaTime;
			perc = currentLerpTime / p_time;
			p_origin.rotation = Quaternion.Slerp(p_origin.rotation, p_dest, perc);
			yield return new WaitForEndOfFrame();
		}
	}

	public static IEnumerator MyCoolDown(GameObject p_obj, float p_time) {
		yield return new WaitForSeconds(p_time);
		p_obj.SetActive(true);
	}

	#endregion
}