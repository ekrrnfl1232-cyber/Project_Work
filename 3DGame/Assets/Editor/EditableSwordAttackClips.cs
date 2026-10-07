using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

// Copies imported FBX clips once; existing editable clips and their events are preserved.
[InitializeOnLoad]
public static class EditableSwordAttackClips
{
    private const string Folder = "Assets/7.Animator/Player";
    private const string ControllerPath = "Assets/Huscarl/Animations/Sword&Shield_AC.controller";

    static EditableSwordAttackClips()
    {
        EditorApplication.delayCall += PrepareOnce;
    }

    private static void PrepareOnce()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
            return;

        if (Enumerable.Range(1, 3).All(i => AssetDatabase.LoadAssetAtPath<AnimationClip>(ClipPath(i)) != null))
            return;

        try { CreateAndConnect(); }
        catch (Exception exception) { Debug.LogException(exception); }
    }

    [MenuItem("Tools/Player/Prepare Editable SwordAttack 1-3")]
    public static void CreateAndConnect()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            throw new InvalidOperationException("Stop Play Mode before preparing editable clips.");

        AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(ControllerPath);
        if (controller == null)
            throw new InvalidOperationException("Player Animator Controller was not found: " + ControllerPath);

        var sourceClips = new List<AnimationClip>();
        for (int i = 1; i <= 3; i++)
        {
            string sourcePath = "Assets/Huscarl/Animations/Sword&Shield@S&S_SwordAttack" + i + ".fbx";
            AnimationClip clip = AssetDatabase.LoadAllAssetsAtPath(sourcePath)
                .OfType<AnimationClip>()
                .FirstOrDefault(c => c.name.Contains("SwordAttack" + i) && !c.name.StartsWith("__preview__"));
            if (clip == null)
                throw new InvalidOperationException("Source attack clip was not found: " + sourcePath);
            sourceClips.Add(clip);
        }

        if (!AssetDatabase.IsValidFolder(Folder))
            AssetDatabase.CreateFolder("Assets/7.Animator", "Player");

        var replacements = new Dictionary<AnimationClip, AnimationClip>();
        for (int i = 1; i <= 3; i++)
        {
            string path = ClipPath(i);
            AnimationClip editable = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
            if (editable == null)
            {
                if (AssetDatabase.LoadMainAssetAtPath(path) != null)
                    throw new InvalidOperationException("An unrelated asset occupies " + path);
                editable = UnityEngine.Object.Instantiate(sourceClips[i - 1]);
                editable.name = "SwordAttack" + i;
                editable.hideFlags = HideFlags.None;
                AssetDatabase.CreateAsset(editable, path);
            }
            replacements.Add(sourceClips[i - 1], editable);
        }

        int changed = 0;
        foreach (AnimatorControllerLayer layer in controller.layers)
            changed += ReplaceMotions(layer.stateMachine, replacements);

        AssetDatabase.SaveAssets();
        Debug.Log("Editable SwordAttack1-3 clips are ready in " + Folder + ". Animator states updated: " + changed);
    }

    private static int ReplaceMotions(AnimatorStateMachine machine, Dictionary<AnimationClip, AnimationClip> replacements)
    {
        int changed = 0;
        foreach (ChildAnimatorState child in machine.states)
        {
            if (child.state.motion is AnimationClip clip && replacements.TryGetValue(clip, out AnimationClip editable))
            {
                Undo.RecordObject(child.state, "Connect editable sword attack clip");
                child.state.motion = editable;
                EditorUtility.SetDirty(child.state);
                changed++;
            }
        }
        foreach (ChildAnimatorStateMachine child in machine.stateMachines)
            changed += ReplaceMotions(child.stateMachine, replacements);
        return changed;
    }

    private static string ClipPath(int index) => Folder + "/SwordAttack" + index + ".anim";
}
