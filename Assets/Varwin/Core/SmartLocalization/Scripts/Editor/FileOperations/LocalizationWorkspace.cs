﻿#if UNITY_EDITOR


//LocalizationWorkspace.cs
//
// Written by Niklas Borglund and Jakob Hillerström
//
namespace SmartLocalization.Editor
{
using UnityEngine;
using UnityEditor;
using System.Globalization;
using SmartLocalization;

/// <summary>
/// Contains and creates all the directory and file information about the workspace
/// </summary>
public static class LocalizationWorkspace
{
	/// <summary>The .resx file ending as a string</summary>
	public static string resXFileEnding 	= ".resx";
	/// <summary>The .txt file ending as a string</summary>
	public static string txtFileEnding		= ".txt";
	/// <summary>The .meta file ending as a string</summary>
	public static string metaFileEnding		= ".meta";
	/// <summary>The .prefab file ending as a string</summary>
	public static string prefabFileEnding	= ".prefab";
	/// <summary>The name of the root language file</summary>
	public static string rootLanguageName	= "Language";

	static string WorkspaceName				= "SmartLocalizationWorkspace";
	static string AutoGeneratedFolderName	= "AutoGenerated";
	static string DataFolderName			= "Data";
	static string LanguageDataFolderName	= "LanguageData";

	static string CultureInfoDataFolderName		= "SmartCultureInfoData";
	static string CultureInfoCollectionFileName = "SmartCultureInfo.xml";
	static string AvailableCulturesFileName		= "AvailableCultures.xml";

	static string ResourcesFolderName		= "Resources";
	static string AudioFileFolderName		= "AudioFiles";
	static string TexturesFolderName		= "Textures";
	static string PrefabsFolderName			= "Prefabs";
	static string TextAssetsFolderName		= "TextAssets";
	static string FontsFolderName			= "Fonts";

	/// <summary>
	/// Array that contains SmartCultureInfo instances of cultures that should be in the main
	/// Smart Localization package(and is not a part of the System.Globalization namespace)
	/// </summary>
	static SmartCultureInfo[] ExtraCultureInfos =
	{
		new SmartCultureInfo("ms", "Malaysia", "Malaysia", false),
		new SmartCultureInfo("tlh", "Klingon", "tlhIngan Hol", false),
		new SmartCultureInfo("tlh-Qaak", "Klingon (pIqaD)", "tlhIngan Hol", false),
		new SmartCultureInfo("cy", "Welsh", "Cymraeg", false),
		new SmartCultureInfo("sr", "Serbian", "српски", false)
	};

#region Creation / Initialization

	/// <summary>
	/// Creates a new workspace. Returns false if a workspace already exists
	/// </summary>
	/// <returns>If the creation was a success.</returns>
	public static bool Create()
	{
		if(Exists())
		{
			return false;
		}

		if(DirectoryUtility.Create(WorkspaceFolderPath()))
		{
			if(!DirectoryUtility.Create(AutoGeneratedFolderPath()))
			{
				return false;
			}

			if(!DirectoryUtility.Create(ResourcesFolderFilePath()))
			{
				return false;
			}
			
			if(!DirectoryUtility.Create(DataFolderPath()))
			{
				return false;
			}

			if(!DirectoryUtility.Create(LanguageDataFolderPath()))
			{
				return false;
			}
			
			if(!GenerateCultureInfoCollection())
			{
				return false;
			}
			
			LanguageHandlerEditor.CreateRootResourceFile();

			AssetDatabase.Refresh();
			return true;
		}

		return false;
	}

	/// <summary>
	/// Generates the SmartCultureInfoCollection xml with all the available language infos and keeps any difference in old data 
	/// as well.
	/// </summary>
	/// <returns>If the operation was successful</returns>
	public static bool GenerateCultureInfoCollection(SmartCultureInfoCollection previousCollection)
	{
		if(!DirectoryUtility.Create(CultureInfoDataFolderPath()))
		{
			return false;
		}
		
		SmartCultureInfoCollection availableCultures = new SmartCultureInfoCollection();
		availableCultures.version = SmartCultureInfoCollection.LatestVersion;

		if(previousCollection != null)
		{
			//Add all cultures from the previous collection in case any one of them were custom made
			foreach(var cultureInfo in previousCollection.cultureInfos)
			{
				availableCultures.AddCultureInfo(cultureInfo);
			}
		}

		//Get all the cultures from Microsofts CultureInfo in the System.Globalization namespace
		CultureInfo[] cultureInfos = CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures);
		foreach(var cultureInfo in cultureInfos)
		{
			if(availableCultures.FindCulture(cultureInfo.Name) == null)
			{
				availableCultures.AddCultureInfo(new SmartCultureInfo(cultureInfo.Name, cultureInfo.EnglishName, cultureInfo.NativeName, cultureInfo.TextInfo.IsRightToLeft));
			}
		}

		foreach(var cultureInfo in ExtraCultureInfos)
		{
			if(availableCultures.FindCulture(cultureInfo) == null)
			{
				availableCultures.AddCultureInfo(new SmartCultureInfo(cultureInfo.languageCode, cultureInfo.englishName, cultureInfo.nativeName, cultureInfo.isRightToLeft));
			}
		}

		availableCultures.Serialize(CultureInfoCollectionFilePath());
		
		return true;
	}

	/// <summary>
	/// Generates the SmartCultureInfoCollection xml with all the available language infos
	/// </summary>
	/// <returns></returns>
	public static bool GenerateCultureInfoCollection()
	{
		return GenerateCultureInfoCollection(null);
	}

#endregion	

#region Lookups

	/// <summary>
	/// Checks if there is an existing workspace
	/// </summary>
	/// <returns>If the workspace exists</returns>
	public static bool Exists()
	{
		return DirectoryUtility.ExistsRelative("/" + WorkspaceName);
	}

	/// <summary> Returns the path to the workspace folder</summary>
	public static string WorkspaceFolderPath()
	{
		return Application.dataPath + "/Core" + "/" + WorkspaceName;
	}

	/// <summary> Returns the path to the AutoGenerated folder</summary>
	public static string AutoGeneratedFolderPath()
	{
		return WorkspaceFolderPath() + "/" + AutoGeneratedFolderName;
	}

	/// <summary> Returns the path to the Data folder</summary>
	public static string DataFolderPath()
	{
		return WorkspaceFolderPath() + "/" + DataFolderName;
	}
	
	/// <summary> Returns the path to the LanguageData folder</summary>
	public static string LanguageDataFolderPath()
	{
		return DataFolderPath() + "/" + LanguageDataFolderName;
	}

	/// <summary> Returns the path to the CultureInfoData folder</summary>
	public static string CultureInfoDataFolderPath()
	{
		return DataFolderPath() + "/" + CultureInfoDataFolderName;
	}

	/// <summary> Returns the path to the CultureInfoCollection</summary>
	public static string CultureInfoCollectionFilePath()
	{
		return CultureInfoDataFolderPath() + "/" + CultureInfoCollectionFileName;
	}

	/// <summary> Returns the path to the root language file</summary>
	public static string RootLanguageFilePath()
	{
		return LanguageDataFolderPath() + "/" + rootLanguageName + resXFileEnding;
	}
	
	/// <summary>
	/// Returns the file path to a specified language file
	/// </summary>
	/// <param name="languageCode">The language code of the file to retrieve</param>
	/// <returns>The full path to the language</returns>
	public static string LanguageFilePath(string languageCode)
	{
		return LanguageDataFolderPath() + "/" + rootLanguageName + "." + languageCode + resXFileEnding;
	}

#endregion

#region Lookups -> Runtime folders

	/// <summary> Returns the path to the Resources folder used at runtime</summary>
	public static string ResourcesFolderFilePath()
	{
		return AutoGeneratedFolderPath() + "/"  + ResourcesFolderName;
	}

	/// <summary> Returns the path to the AvailableCultures xml file</summary>
	public static string AvailableCulturesFilePath()
	{
		return ResourcesFolderFilePath() + "/" + AvailableCulturesFileName;
	}
	
	/// <summary>
	/// Returns the base directory path for a specified language, where you can find the localized assets(textures, audio, etc)
	/// </summary>
	/// <param name="languageCode">The language code of the culture</param>
	/// <returns>The directory path for the language</returns>
	public static string LanguageRuntimeFolderPath(string languageCode)
	{
		return ResourcesFolderFilePath() + "/" + languageCode;
	}
	
	/// <summary>
	/// Returns the audio directory path for a specified language, where you can find the localized audio assets
	/// </summary>
	/// <param name="languageCode">The language code of the culture</param>
	/// <returns>The audio directory path for the language</returns>
	public static string LanguageAudioFolderPath(string languageCode)
	{
		return LanguageRuntimeFolderPath(languageCode) + "/" + AudioFileFolderName;
	}
	
	/// <summary>
	/// Returns the textures directory path for a specified language, where you can find the localized texture assets
	/// </summary>
	/// <param name="languageCode">The language code of the culture</param>
	/// <returns>The textures directory path for the language</returns>
	public static string LanguageTexturesFolderPath(string languageCode)
	{
		return LanguageRuntimeFolderPath(languageCode) + "/" + TexturesFolderName;
	}

	public static string LanguageTextAssetsFolderPath(string languageCode)
	{
		return LanguageRuntimeFolderPath(languageCode) + "/" + TextAssetsFolderName;
	}
	
	public static string LanguageFontsFolderPath(string languageCode)
	{
		return LanguageRuntimeFolderPath(languageCode) + "/" + FontsFolderName;
	}
	
	/// <summary>
	/// Returns the prefabs directory path for a specified language, where you can find the localized prefab assets
	/// </summary>
	/// <param name="languageCode">The language code of the culture</param>
	/// <returns>The prefabs directory path for the language</returns>
	public static string LanguagePrefabsFolderPath(string languageCode)
	{
		return LanguageRuntimeFolderPath(languageCode) + "/" + PrefabsFolderName;
	}

	/// <summary>
	/// Gets the relative path to the resource folder used at runtime.
	/// </summary>
	public static string ResourcesFolderFilePathRelative()
	{
		return ResourcesFolderFilePath().Substring(Application.dataPath.Length);
	}
	
	/// <summary>
	/// Gets the relative path to the folder used at runtime for a specific language.
	/// </summary>
	public static string LanguageRuntimeFolderPathRelative(string languageCode)
	{
		return LanguageRuntimeFolderPath(languageCode).Substring(Application.dataPath.Length);
	}
	
	/// <summary>
	/// Gets the relative path to the audio folder used at runtime for a specific language.
	/// </summary>
	public static string LanguageAudioFolderPathRelative(string languageCode)
	{
		return LanguageAudioFolderPath(languageCode).Substring(Application.dataPath.Length);
	}
	
	/// <summary>
	/// Gets the relative path to the textures folder used at runtime for a specific language.
	/// </summary>
	public static string LanguageTexturesFolderPathRelative(string languageCode)
	{
		return LanguageTexturesFolderPath(languageCode).Substring(Application.dataPath.Length);
	}

	/// <summary>
	/// Gets the relative path to the text assets folder used at runtime for a specific language.
	/// </summary>
	public static string LanguageTextAssetsFolderPathRelative(string languageCode)
	{
		return LanguageTextAssetsFolderPath(languageCode).Substring(Application.dataPath.Length);
	}
	
	/// <summary>
	/// Gets the relative path to the fonts folder used at runtime for a specific language.
	/// </summary>
	public static string LanguageFontsFolderPathRelative(string languageCode)
	{
		return LanguageFontsFolderPath(languageCode).Substring(Application.dataPath.Length);
	}
	
	/// <summary>
	/// Gets the relative path to the prefabs folder used at runtime for a specific language.
	/// </summary>
	public static string LanguagePrefabsFolderPathRelative(string languageCode)
	{
		return LanguagePrefabsFolderPath(languageCode).Substring(Application.dataPath.Length);
	}

#endregion
}
} //namespace SmartLocalization.Editor


#endif