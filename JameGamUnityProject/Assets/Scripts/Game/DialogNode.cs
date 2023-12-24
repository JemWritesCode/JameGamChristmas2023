using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace JameGam {
  [CreateAssetMenu(menuName = "JameGam/DialogNode")]
  public class DialogNode : ScriptableObject {
    public enum DialogNodeType {
      Conversation
    }

    [field: SerializeField, Header("Node")]
    public DialogNodeType NodeType { get; private set; }

    [field: SerializeField, Header("Conversation"), TextArea]
    public List<string> ConversationTexts { get; private set; }

    [field: NonSerialized]
    public int ConversationTextIndex = 0;

    public bool HasConversationText() {
      return ConversationTextIndex < ConversationTexts.Count;
    }

    public string GetNextConversationText() {
      if (ConversationTextIndex < ConversationTexts.Count) {
        ConversationTextIndex++;
        return ConversationTexts[ConversationTextIndex - 1];
      }

      return string.Empty;
    }

    [field: NonSerialized, Header("Events")]
    public Queue<Action<DialogNode>> OnNodeCompleteCallbacks = new();

    public void OnNodeComplete() {
      while (OnNodeCompleteCallbacks.Count > 0) {
        OnNodeCompleteCallbacks.Dequeue()?.Invoke(this);
      }
    }
  }
}
