﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstructionList : MonoBehaviour {

    public List<GameObject> list;
    public int maxInstructions = 5;

    [SerializeField]
    private GameObject instPrefab;

    [SerializeField]
    private GameObject toBeAdded;

    public void insertAtPosition(int position) {
        list.Remove(null);
        if (list.Count < maxInstructions) {
            if (position > list.Count) position = list.Count;
            if (toBeAdded != null) {
                var father = GameObject.Find("Node" + position).transform;
                // if (father.childCount > 0) Destroy(father.GetChild(0));
                var go = Instantiate(instPrefab,
                                 Utils.findChild(gameObject, "Node" + position).transform
                                 );
                var node = go.GetComponent<InstructionListNode>();
                node.instructionList = this;
                node.index = position;
                node.instructionType = toBeAdded.GetComponent<DraggedInstruction>().type;

                var image = go.GetComponent<Image>();
                var draggedImage = toBeAdded.GetComponent<Image>();
                var color = draggedImage.color;
                color.a = 1;
                image.color = color;
                image.sprite = draggedImage.sprite;

                var icon = go.transform.GetChild(0).GetComponent<Image>();
                var draggedIcon = toBeAdded.transform.GetChild(0).GetComponent<Image>();
                icon.sprite = draggedIcon.sprite;
                icon.transform.rotation = draggedIcon.transform.rotation;
                node.dragItemIcon = icon.sprite;

                node.rotation = draggedIcon.transform.eulerAngles.z;

                go.transform.localPosition = Vector2.zero;

                list.Insert(position, go);
            }
        }
    }

    public void hoverPositionEnter(int position) {
        if (list.Count < maxInstructions) {
            if (DragDropManager.currentDragItem != null) {
                list.Remove(null);
                if (position > list.Count) position = list.Count;
                list.Insert(position,
                            null
                            );
                toBeAdded = DragDropManager.currentDragItem;
            }
        }
    }

    public void hoverPositionExit() {
        // If player released touch, DO NOT remove nulls from list
        for (int i = 0; i < Input.touches.Length; i++) {
            if (Input.touches[i].phase == TouchPhase.Ended) {
                return;
            }
        }
        list.Remove(null);
    }
}
