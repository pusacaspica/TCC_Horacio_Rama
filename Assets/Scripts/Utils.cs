﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static IEnumerator cooldown (float duration, System.Action callback) {
        yield return new WaitForSeconds(duration);
        callback();
    }
}