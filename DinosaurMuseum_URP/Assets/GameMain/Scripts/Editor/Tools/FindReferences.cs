using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 找自身被哪些人引用
/// </summary>
public class FindReferences
{
	[MenuItem("Assets/VGame/Find References", false, 10)]
	private static void Find()
	{
		EditorSettings.serializationMode = SerializationMode.ForceText;
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (!string.IsNullOrEmpty(path))
		{
			string guid = AssetDatabase.AssetPathToGUID(path);
			List<string> withoutExtensions = new List<string>() { ".prefab", ".unity", ".mat", ".asset" };
			string[] files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
				.Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
			int startIndex = 0;

			EditorApplication.update = delegate ()
			{
				string file = files[startIndex];

				bool isCancel = EditorUtility.DisplayCancelableProgressBar("匹配资源中", file, (float)startIndex / (float)files.Length);

				if (Regex.IsMatch(File.ReadAllText(file), guid))
				{
					Debug.Log(file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(file)));
				}

				startIndex++;
				if (isCancel || startIndex >= files.Length)
				{
					EditorUtility.ClearProgressBar();
					EditorApplication.update = null;
					startIndex = 0;
					Debug.Log("匹配结束");
				}
			};
		}
	}

	[MenuItem("Assets/VGame/Find References", true)]
	private static bool VFind()
	{
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		return (!string.IsNullOrEmpty(path));
	}

	public static string GetRelativeAssetsPath(string path)
	{
		return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
	}

	public static void FindCB(UnityEngine.Object obj, System.Action<List<string>> findFinish)
	{
		EditorSettings.serializationMode = SerializationMode.ForceText;
		string path = AssetDatabase.GetAssetPath(obj);
		if (!string.IsNullOrEmpty(path))
		{
			string guid = AssetDatabase.AssetPathToGUID(path);
			List<string> withoutExtensions = new List<string>() { ".prefab", ".unity", ".mat", ".asset" };
			string[] files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
				.Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
			int startIndex = 0;

			List<string> guids = new List<string>();
			EditorApplication.update = delegate ()
			{
				string file = files[startIndex];

				bool isCancel = EditorUtility.DisplayCancelableProgressBar("匹配资源中", file, (float)startIndex / (float)files.Length);

				if (Regex.IsMatch(File.ReadAllText(file), guid))
				{
					//Debug.Log(file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(file)));
					guids.Add(file);
				}

				startIndex++;
				if (isCancel || startIndex >= files.Length)
				{
					EditorUtility.ClearProgressBar();
					EditorApplication.update = null;
					startIndex = 0;
					Debug.Log("匹配结束");

					findFinish(guids);
				}
			};
		}
	}
}