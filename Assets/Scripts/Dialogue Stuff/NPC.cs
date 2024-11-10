using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractible
{
    [SerializeField] private SpriteRenderer interactSprite;

    private Transform playerTransform;
    private OmniDirectionalMovement johnMovement;

    private const float interactDistance = 2f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        johnMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<OmniDirectionalMovement>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && IsWithinInteractDistance() && !johnMovement.inBattle)
        {
            Interact();
        }

        if (interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(false);
        }
        else if (!interactSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(true);
        }
    }

    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(playerTransform.position, this.gameObject.transform.position) < interactDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
