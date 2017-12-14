using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Editor.GroupActions;
using UnityEditor;
using UnityEngine.UI;

public class JoystickTool : EditorWindow
{
//  private string[] _buttons = new string[] {"A", "B", "X", "Y", "LB", "RB", "Select", "Start"};
//  private string[] _axis = new string[] {"H_1", "V_1", "H_2", "V_2", "H_3", "V_3", "H_4", "V_4"};

  //private const string PLAYER_PREF_FILE_PATH = "JoystickTool.filePath";

  private ActionGroupManager _actionGroupManager;

  private Vector2 _scrollPosition;

  private SerializedObject _inputManager;
  //private SerializedProperty _currentInput;
  private bool _modified;
  private HashSet<int> _selectedIndexes = new HashSet<int>();

  [MenuItem("Lim/Joystick tool")]
  public static void GetJoystickTool()
  {
    JoystickTool w = (JoystickTool) EditorWindow.GetWindow<JoystickTool>();
    w.Show();
  }

  private void InitInputList()
  {
    if (_inputManager == null)
    {
      var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
      _inputManager = new SerializedObject(inputManager);
      _modified = false;
    }
  }

  private void InitActionGroups()
  {
    if (_actionGroupManager == null)
    {
      _actionGroupManager = new ActionGroupManager();
      _actionGroupManager.Init();
    }
  }


  void OnGUI()
  {
    InitInputList();
    InitActionGroups();

    SerializedProperty axisArray = _inputManager.FindProperty("m_Axes");


    EditorGUILayout.BeginHorizontal();

    if (_inputManager != null && GUILayout.Button("load Default inputs"))
    {
      AssetDatabase.CopyAsset("ProjectSettings/InputManager.asset", "ProjectSettings/InputManager.backup");
      AssetDatabase.CopyAsset("Assets/Plugins/Input/Default/InputManager.asset", "ProjectSettings/InputManager.asset");
      AssetDatabase.SaveAssets();
      _inputManager = null;
      InitInputList();

    }

    if (_inputManager != null && _modified && GUILayout.Button("save"))
    {
      _inputManager.ApplyModifiedProperties();
      AssetDatabase.SaveAssets();
      _modified = false;
    }


    EditorGUILayout.EndHorizontal();

    //GroupActionMenu();


    if (_inputManager != null)
    {
      EditorGUILayout.BeginHorizontal();

      EditorGUILayout.BeginVertical(GUILayout.Width(250));
        if (_inputManager != null && GUILayout.Button("+"))
        {
          axisArray.InsertArrayElementAtIndex(axisArray.arraySize);
          _modified = true;
        }
        DisplayInputList(axisArray);
      EditorGUILayout.EndVertical();

      EditorGUILayout.BeginVertical();
      if (_selectedIndexes.Count == 1)
      {
        DisplaySelection(axisArray);
      }else if (_selectedIndexes.Count > 1)
      {
        DisplayMultiSelection(axisArray);
      }

      if(_selectedIndexes.Count >= 1)
        _modified|=_actionGroupManager.OnGUI(_selectedIndexes,axisArray);

      EditorGUILayout.EndVertical();

      EditorGUILayout.EndHorizontal();
    }
  }


  private void DisplayInputList(SerializedProperty axisArray)
  {
    _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
    for (int i = 0; i < axisArray.arraySize; i++)
    {
      var axis = axisArray.GetArrayElementAtIndex(i);

      var name = axis.FindPropertyRelative("m_Name").stringValue;

      EditorGUILayout.BeginHorizontal();
      if (GUILayout.Toggle(_selectedIndexes.Contains(i), "",GUILayout.Width(20)))
      {
        _selectedIndexes.Add(i);
      }
      else
      {
        _selectedIndexes.Remove(i);
      }

      if (GUILayout.Button(name))
      {
        _selectedIndexes.Clear();
        _selectedIndexes.Add(i);
      }
      if (i != 0 && GUILayout.Button("^", GUILayout.Width(20)))
      {
        axisArray.MoveArrayElement(i, i - 1);
        _modified = true;
      }
      if (i != axisArray.arraySize - 1 && GUILayout.Button("v", GUILayout.Width(20)))
      {
        axisArray.MoveArrayElement(i, i + 1);
        _modified = true;
      }
      if (GUILayout.Button("-", GUILayout.Width(20)))
      {
        if (EditorUtility.DisplayDialog("Delete Input " + name, "Do you really want to delete " + name + "?", "yes",
          "no"))
        {
          axisArray.DeleteArrayElementAtIndex(i);
          _modified = true;
        }
      }
      EditorGUILayout.EndHorizontal();
    }
    EditorGUILayout.EndScrollView();
  }

  private void DisplayMultiSelection(SerializedProperty axisArray)
  {
    EditorGUILayout.BeginVertical("box");
    foreach (var index in _selectedIndexes)
    {
      var currentInput = axisArray.GetArrayElementAtIndex(index);
      EditorGUILayout.LabelField(currentInput.FindPropertyRelative("m_Name").stringValue);
    }


    EditorGUILayout.EndVertical();
  }


  private void DisplaySelection(SerializedProperty axisArray)
  {
    EditorGUILayout.BeginVertical("box");
    var first = _selectedIndexes.First();
    var currentInput = first<axisArray.arraySize?axisArray.GetArrayElementAtIndex(_selectedIndexes.First()):null;
    if (currentInput != null)
    {
      //Debug.Log(_currentInput.FindPropertyRelative("m_Name").stringValue);

      var enumerator = currentInput.Copy().GetEnumerator();

      while (enumerator.MoveNext())
      {
        var serializedProperty = enumerator.Current as SerializedProperty;

        if (serializedProperty != null)
        {
          if (serializedProperty.propertyType == SerializedPropertyType.String)
          {
            var newValue = EditorGUILayout.TextField(serializedProperty.name, serializedProperty.stringValue);
            if (newValue != serializedProperty.stringValue)
            {
              serializedProperty.stringValue = newValue;
              _modified = true;
            }
          }
          else if (serializedProperty.propertyType == SerializedPropertyType.Float)
          {
            var newValue = EditorGUILayout.FloatField(serializedProperty.name, serializedProperty.floatValue);
            if (newValue != serializedProperty.floatValue)
            {
              serializedProperty.floatValue = newValue;
              _modified = true;
            }
          }
          else if (serializedProperty.propertyType == SerializedPropertyType.Boolean)
          {
            var newValue = EditorGUILayout.Toggle(serializedProperty.name, serializedProperty.boolValue);
            if (newValue != serializedProperty.boolValue)
            {
              serializedProperty.boolValue = newValue;
              _modified = true;
            }
          }
          else if (serializedProperty.propertyType == SerializedPropertyType.Enum)
          {
            var newValue = EditorGUILayout.Popup(serializedProperty.name, serializedProperty.enumValueIndex,
              serializedProperty.enumNames);
            if (newValue != serializedProperty.enumValueIndex)
            {
              serializedProperty.enumValueIndex = newValue;
              _modified = true;
            }
          }
          else
          {
            Debug.Log("6" + serializedProperty.propertyType + " " + serializedProperty.name);
          }
        }
      }
    }
    EditorGUILayout.EndVertical();
  }
}