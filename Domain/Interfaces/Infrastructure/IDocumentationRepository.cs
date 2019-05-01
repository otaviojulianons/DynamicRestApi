namespace Domain.Interfaces.Infrastructure
{
    public interface IDocumentationRepository
    {
         object Get();
         void Update(string json);
    }
}