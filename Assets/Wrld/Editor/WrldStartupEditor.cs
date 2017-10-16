using Wrld;
using Wrld.Scripts.Utilities;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WrldMap))]
public class WrldStartupEditor : Editor
{
    private const string WrldApiKeyTag = "WRLD_API_KEY";

    SerializedProperty m_apiKey;
    SerializedProperty m_streamingCamera;
    SerializedProperty m_latitudeDegrees;
    SerializedProperty m_longitudeDegrees;
    SerializedProperty m_distanceToInterest;
    SerializedProperty m_headingDegrees;
    SerializedProperty m_coordSystem;
    SerializedProperty m_streamingLodBasedOnDistance;
    SerializedProperty m_materialDirectory;
    SerializedProperty m_overrideLandmarkMaterial;
    SerializedProperty m_useBuiltInCameraControls;
    SerializedProperty m_terrainCollisions;
    SerializedProperty m_roadCollisions;
    SerializedProperty m_buildingCollisions;

    bool m_cachedKeyHasBeenRead;

    void OnEnable()
    {
        m_apiKey = serializedObject.FindProperty("m_apiKey");
        m_streamingCamera = serializedObject.FindProperty("m_streamingCamera");
        m_latitudeDegrees = serializedObject.FindProperty("m_latitudeDegrees");
        m_longitudeDegrees = serializedObject.FindProperty("m_longitudeDegrees");
        m_distanceToInterest = serializedObject.FindProperty("m_distanceToInterest");
        m_headingDegrees = serializedObject.FindProperty("m_headingDegrees");
        m_coordSystem = serializedObject.FindProperty("m_coordSystem");
        m_streamingLodBasedOnDistance = serializedObject.FindProperty("m_streamingLodBasedOnDistance");
        m_materialDirectory = serializedObject.FindProperty("m_materialDirectory");
        m_overrideLandmarkMaterial = serializedObject.FindProperty("m_overrideLandmarkMaterial");
        m_useBuiltInCameraControls = serializedObject.FindProperty("m_useBuiltInCameraControls");
        m_terrainCollisions = serializedObject.FindProperty("m_terrainCollisions");
        m_roadCollisions = serializedObject.FindProperty("m_roadCollisions");
        m_buildingCollisions = serializedObject.FindProperty("m_buildingCollisions");
        m_cachedKeyHasBeenRead = false;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_apiKey, new GUIContent("API Key"));
        bool apiKeyChanged = EditorGUI.EndChangeCheck();

        if (apiKeyChanged)
        {
            m_apiKey.stringValue = m_apiKey.stringValue.Trim();
        }

        bool hasApiKey = APIKeyHelpers.AppearsValid(m_apiKey.stringValue);

        if (!hasApiKey)
        {
            if (!m_cachedKeyHasBeenRead)
            {
                var cachedAPIKey = PlayerPrefs.GetString(WrldApiKeyTag);
                hasApiKey = APIKeyHelpers.AppearsValid(cachedAPIKey);

                if (hasApiKey)
                {
                    m_apiKey.stringValue = cachedAPIKey;
                }

                m_cachedKeyHasBeenRead = true;
            }
        }
        
        if (hasApiKey)
        {
            if (apiKeyChanged)
            {
                PlayerPrefs.SetString(WrldApiKeyTag, m_apiKey.stringValue);
            }

            if (QualitySettings.shadowDistance < Wrld.Constants.RecommendedShadowDistance)
            {
                EditorGUILayout.HelpBox("Your Shadow Distance setting is below the recommended value. Shadows may not display correctly.", MessageType.Warning);

                if (GUILayout.Button("Use recommended value"))
                {
                    QualitySettings.shadowDistance = Wrld.Constants.RecommendedShadowDistance;
                }
            }

            EditorGUILayout.PropertyField(m_streamingCamera, new GUIContent("Camera"));
            EditorGUILayout.PropertyField(m_useBuiltInCameraControls, new GUIContent("Use Built-in Camera Controls"));

            EditorGUILayout.PropertyField(m_latitudeDegrees, new GUIContent("Start Latitude"));
            EditorGUILayout.PropertyField(m_longitudeDegrees, new GUIContent("Start Longitude"));

            if (m_useBuiltInCameraControls.boolValue)
            {
                EditorGUILayout.PropertyField(m_distanceToInterest, new GUIContent("Starting Distance To Interest Point"));
                EditorGUILayout.PropertyField(m_headingDegrees, new GUIContent("Start Heading"));
            }

            EditorGUILayout.PropertyField(m_coordSystem, new GUIContent("World Space"));
            EditorGUILayout.PropertyField(m_streamingLodBasedOnDistance, new GUIContent("LOD Based On Distance"));

            EditorGUILayout.PropertyField(m_terrainCollisions, new GUIContent("Terrain Collisions"));
            EditorGUILayout.PropertyField(m_roadCollisions, new GUIContent("Road Collisions"));
            EditorGUILayout.PropertyField(m_buildingCollisions, new GUIContent("Building Collisions"));

            EditorGUILayout.PropertyField(m_materialDirectory, new GUIContent("Material Directory"));
            EditorGUILayout.PropertyField(m_overrideLandmarkMaterial, new GUIContent("Landmark Override Material"));

            if (GUILayout.Button("Provide Feedback"))
            {
                Application.OpenURL("https://docs.google.com/a/eegeo.com/forms/d/e/1FAIpQLSe_QJz3sqn7Xs-yiMw94JTdQiZ4Nq-50FYMRD21th0ZHMPEmQ/viewform");
            }
        }
        else
        {
            EditorGUILayout.HelpBox("You must have an API key!", MessageType.Error);

            if (GUILayout.Button("Get an API key from wrld3d.com"))
            {
                Application.OpenURL("https://wrld3d.com/register?utm_source=UnitySDK&utm_medium=UnityEditor");
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}


