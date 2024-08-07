using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    List<Interaction> _interactions = new();

    public Interaction GetInteraction()
    {
        return _interactions.Count == 0 ? null : 
            _interactions.OrderBy(interaction => Vector3.Distance(interaction.transform.position, transform.position)).First();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interaction = collision.GetComponent<Interaction>();

        if (interaction != null)
            _interactions.Add(interaction);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var interaction = collision.GetComponent<Interaction>();

        if (interaction != null && _interactions.Contains(interaction))
            _interactions.Remove(interaction);
    }
}