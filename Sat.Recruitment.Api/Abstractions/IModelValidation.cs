namespace Sat.Recruitment.Api.Abstractions
{
    public interface IModelValidation<in T>
    {
        string Validate(T source);
    }
}