namespace backend.Interfaces
{
    public interface IValidator<T>
    {
        void AddRule(Func<T, bool> rule, string errorMessage);
        bool Validate(T entity);
    }
}