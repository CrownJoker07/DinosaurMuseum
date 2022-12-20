using GameFramework;
using UnityEditor;
using UnityEngine;

namespace EasyARProject.Editor.DataTableTools
{
	public sealed class DataTableGeneratorMenu
	{
		[MenuItem("Tools/Generate DataTables/For Mac")]
		private static void GenerateDataTablesMac()
		{
			GenerateDataTables(true);
		}

		[MenuItem("Tools/Generate DataTables/For Win")]
		private static void GenerateDataTablesWin()
		{
			GenerateDataTables(false);
		}

		[MenuItem("Tools/Generate Language/For Mac")]
		private static void GenerateLanguageMac()
		{
			GenerateLanguage(true);
		}

		[MenuItem("Tools/Generate Language/For Win")]
		private static void GenerateLanguageWin()
		{
			GenerateLanguage(false);
		}

		/// <summary>
		/// 生成数据表格
		/// </summary>
		/// <param name="isMac"></param>
		private static void GenerateDataTables(bool isMac)
		{
			//第一步先执行sh
			Debug.Log("step1:Execute ExcelToCsv.bat");

			//excel_output
			string outputPath = Application.dataPath + "/../../Doc/excel_output/csv";
			if (!System.IO.Directory.Exists(outputPath))
			{
				System.IO.Directory.CreateDirectory(outputPath);
			}

			if (isMac)
			{
				ProcessCommand("/bin/sh", "ExcelToCsv.sh", Application.dataPath + "/../../Doc");
			}
			else
			{
				ProcessCommand("ExcelToCsv.bat", "", Application.dataPath + "/../../Doc");
			}

			Debug.Log("step2:General DataTable");
			foreach (string dataTableName in ProcedurePreload.DataTableNames)
			{
				DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
				if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
				{
					Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
					break;
				}

				DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
				DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
			}

			AssetDatabase.Refresh();
		}

		/// <summary>
		/// 生成多语言配置
		/// </summary>
		/// <param name="isMac"></param>
		private static void GenerateLanguage(bool isMac)
		{
			//第一步先执行sh
			Debug.Log("step1:Execute uiText.sh");

			//uiText_output
			string outputPath = Application.dataPath + "/../../Doc/TranslationTool/uiText_output";
			if (!System.IO.Directory.Exists(outputPath))
			{
				System.IO.Directory.CreateDirectory(outputPath);
			}

			string outputPath1 = Application.dataPath + "/../../Doc/excel_output/Language";
			if (!System.IO.Directory.Exists(outputPath1))
			{
				System.IO.Directory.CreateDirectory(outputPath1);
			}

			if (isMac)
			{
				ProcessCommand("/bin/sh", "uiText.sh", Application.dataPath + "/../../Doc/TranslationTool/");
			}
			else
			{
				ProcessCommand("uiText.bat", "", Application.dataPath + "/../../Doc/TranslationTool/");
			}
			AssetDatabase.Refresh();
		}

		/// <summary>
		/// 执行shell bat exe 等
		/// </summary>
		/// <param name="command"></param>
		/// <param name="argument"></param>
		/// <param name="workingDirectory"></param>
		public static void ProcessCommand(string command, string argument, string workingDirectory)
		{
			System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo(command);
			start.Arguments = argument;
			start.CreateNoWindow = false;
			start.ErrorDialog = true;
			start.UseShellExecute = true;
			start.WorkingDirectory = workingDirectory;

			if (start.UseShellExecute)
			{
				start.RedirectStandardOutput = false;
				start.RedirectStandardError = false;
				start.RedirectStandardInput = false;
			}
			else
			{
				start.RedirectStandardOutput = true;
				start.RedirectStandardError = true;
				start.RedirectStandardInput = true;
				start.StandardOutputEncoding = System.Text.UTF8Encoding.UTF8;
				start.StandardErrorEncoding = System.Text.UTF8Encoding.UTF8;
			}

			System.Diagnostics.Process p = System.Diagnostics.Process.Start(start);

			p.WaitForExit();
			p.Close();
		}
	}
}