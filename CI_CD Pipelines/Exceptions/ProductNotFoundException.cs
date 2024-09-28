namespace CI_CD_Pipelines.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int id) : base($"This Product with Id {id} doesn't exist")
    {
    }
}