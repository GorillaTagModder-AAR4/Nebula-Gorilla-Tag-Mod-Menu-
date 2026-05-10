using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
	public static CoroutineRunner Instance;

	private void Awake()
	{
		Instance = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}
}
