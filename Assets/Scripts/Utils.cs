﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils {

    public enum InstructionType {
        MOVE_U,
        MOVE_L,
        MOVE_R,
        MOVE_D,
        GRAB_U,
        GRAB_L,
        GRAB_R,
        GRAB_D,
        WAIT
    };

    public static char CanCatMove(PuzzleManager.PuzzleObject obstacle) {
        // Is blocked by
        if ((new List<PuzzleManager.PuzzleObject> {
            PuzzleManager.PuzzleObject.ROCK,
            PuzzleManager.PuzzleObject.DOG,
            PuzzleManager.PuzzleObject.BIRD,
            // TODO
        }).Contains(obstacle)) {
            return 'N';
        }

        // Can push
        if ((new List<PuzzleManager.PuzzleObject> {
            // TODO
        }).Contains(obstacle)) {
            return 'P';
        }

        // Can move
        return 'Y';
    }

    public static char CanCatGrab(PuzzleManager.PuzzleObject obstacle) {
        // Can grab
        if ((new List<PuzzleManager.PuzzleObject> {
            PuzzleManager.PuzzleObject.BIRD,
            // TODO
        }).Contains(obstacle)) {
            return 'Y';
        }

        // Can't grab
        return 'N';
    }

    public static char CanDogMove(PuzzleManager.PuzzleObject obstacle) {
        // Is blocked by
        if ((new List<PuzzleManager.PuzzleObject> {
            PuzzleManager.PuzzleObject.CAT,
            PuzzleManager.PuzzleObject.BIRD,
            // TODO
        }).Contains(obstacle)) {
            return 'N';
        }

        // Can push
        if ((new List<PuzzleManager.PuzzleObject> {
            PuzzleManager.PuzzleObject.ROCK,
            // TODO
        }).Contains(obstacle)) {
            return 'P';
        }

        // Can move
        return 'Y';
    }

    public static char CanDogGrab(PuzzleManager.PuzzleObject obstacle) {
        // Can grab
        if ((new List<PuzzleManager.PuzzleObject> {
            PuzzleManager.PuzzleObject.BIRD,
            PuzzleManager.PuzzleObject.CAT,
            // TODO
        }).Contains(obstacle)) {
            return 'Y';
        }

        // Can't grab
        return 'N';
    }

    public static char CanPlaceObject(PuzzleManager.PuzzleObject obj) {
        // Can place
        if ((new List<PuzzleManager.PuzzleObject> {
            PuzzleManager.PuzzleObject.NTH,
            // TODO
        }).Contains(obj)) {
            return 'Y';
        }

        // Can't place
        return 'N';
    }

    private static bool IsCatInUpperCorner(PuzzleManager puzzleManager) {
        return puzzleManager.catPosition[1] == puzzleManager.kindMatrix.GetLength(1) - 1;
    }

    private static bool IsCatInLeftCorner(PuzzleManager puzzleManager) {
        return puzzleManager.catPosition[0] == 0;
    }

    private static bool IsCatInRightCorner(PuzzleManager puzzleManager) {
        return puzzleManager.catPosition[0] == puzzleManager.kindMatrix.GetLength(0) - 1;
    }

    private static bool IsCatInBottomCorner(PuzzleManager puzzleManager) {
        return puzzleManager.catPosition[1] == 0;
    }

    private static bool IsDogInUpperCorner(PuzzleManager puzzleManager) {
        return puzzleManager.dogPosition[1] == puzzleManager.kindMatrix.GetLength(1) - 1;
    }

    private static bool IsDogInLeftCorner(PuzzleManager puzzleManager) {
        return puzzleManager.dogPosition[0] == 0;
    }

    private static bool IsDogInRightCorner(PuzzleManager puzzleManager) {
        return puzzleManager.dogPosition[0] == puzzleManager.kindMatrix.GetLength(0) - 1;
    }

    private static bool IsDogInBottomCorner(PuzzleManager puzzleManager) {
        return puzzleManager.dogPosition[1] == 0;
    }

    private static bool IsThingPushedToUpperCorner(PuzzleManager puzzleManager, int i, int j) {
        return j == puzzleManager.kindMatrix.GetLength(1) - 1
                || puzzleManager.kindMatrix[i, j + 1] != PuzzleManager.PuzzleObject.NTH;
    }

    private static bool IsThingPushedToLeftCorner(PuzzleManager puzzleManager, int i, int j) {
        return i == 0
                || puzzleManager.kindMatrix[i - 1, j] != PuzzleManager.PuzzleObject.NTH;
    }

    private static bool IsThingPushedToRightCorner(PuzzleManager puzzleManager, int i, int j) {
        return i == puzzleManager.kindMatrix.GetLength(0) - 1
                || puzzleManager.kindMatrix[i + 1, j] != PuzzleManager.PuzzleObject.NTH;
    }

    private static bool IsThingPushedToBottomCorner(PuzzleManager puzzleManager, int i, int j) {
        return j == 0
                || puzzleManager.kindMatrix[i, j - 1] != PuzzleManager.PuzzleObject.NTH;
    }

    public static bool IsCatInCorner(PuzzleManager puzzleManager, char direction) {
        switch (direction) {
            case 'U':
                return IsCatInUpperCorner(puzzleManager);
            case 'L':
                return IsCatInLeftCorner(puzzleManager);
            case 'R':
                return IsCatInRightCorner(puzzleManager);
            case 'D':
                return IsCatInBottomCorner(puzzleManager);
        }
        return false;
    }

    public static bool IsDogInCorner(PuzzleManager puzzleManager, char direction) {
        switch (direction) {
            case 'U':
                return IsDogInUpperCorner(puzzleManager);
            case 'L':
                return IsDogInLeftCorner(puzzleManager);
            case 'R':
                return IsDogInRightCorner(puzzleManager);
            case 'D':
                return IsDogInBottomCorner(puzzleManager);
        }
        return false;
    }

    public static bool IsThingPushedToCorner(PuzzleManager puzzleManager, char direction, int i, int j) {
        switch (direction) {
            case 'U':
                return IsThingPushedToUpperCorner(puzzleManager, i, j);
            case 'L':
                return IsThingPushedToLeftCorner(puzzleManager, i, j);
            case 'R':
                return IsThingPushedToRightCorner(puzzleManager, i, j);
            case 'D':
                return IsThingPushedToBottomCorner(puzzleManager, i, j);
        }
        return false;
    }

    public static IEnumerator cooldown (float duration, System.Action callback) {
        yield return new WaitForSeconds(duration);
        callback();
    }

    public static GameObject findChild(GameObject go, string childName) {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>()) {
            if (trans.name == childName) {
                return trans.gameObject;
            }
        }
        return null;
    }

    public static void loadPuzzle(string name) {
        PuzzleManager.stageInfo = Resources.Load<PuzzleStageScriptableObject>("ScriptObjects/Puzzles/" + name);
        SceneManager.LoadScene("Scenes/InGame");
    }
}
