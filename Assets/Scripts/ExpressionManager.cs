using UnityEngine;

public class ExpressionManager : MonoBehaviour
{
    GameManager gameManager => GameManager.instance;
    GameObject currentExpression;
    
    public enum Expressions
    {
        Exclamation,
        Question
    }

    public void ShowExpression(Expressions expression, float destroyDelay)
    {

        switch (expression)
        {
            default:
            case Expressions.Exclamation:
                currentExpression = Instantiate(gameManager.exclamationPrefab, transform);
                break;
            case Expressions.Question:
                currentExpression = Instantiate(gameManager.questionPrefab, transform);
                break;
        }

        Destroy(currentExpression, destroyDelay);
        Invoke("NullExpression", destroyDelay);
    }

    public void RemoveExpression()
    {
        if (currentExpression != null)
        {
            Destroy(currentExpression);
            currentExpression = null;
        }
    }

    void NullExpression()
    {
        currentExpression = null;
    }
}
