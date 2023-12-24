using DG.Tweening;

using UnityEngine;

namespace JameGam {
  public class ItemSpawner : MonoBehaviour {
    [field: SerializeField]
    public GameObject ItemToSpawn { get; private set; }

    public void GiveItemToPlayer() {
      transform.DOComplete(withCallbacks: true);

      Vector3 rotation = new(
          Random.Range(8f, 16f) * (Random.value > 0.5 ? 1f : -1f),
          0f,
          Random.Range(8f, 16f) * (Random.value > 0.5 ? 1f : -1f));

      transform.DOPunchRotation(rotation, Random.Range(1f, 2f), Random.Range(6, 10), 1f);

      Debug.Log($"Giving item to player: {ItemToSpawn.name}");
      GameManager.Instance.CurrentPlayer.GetComponent<Pickup>().PickupItemFromSpawner(ItemToSpawn);
    }
  }
}
