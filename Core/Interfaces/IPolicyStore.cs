namespace Core.Services;

public interface IPolicyStore
{ 
    Task<IEnumerable<Policy>> GetAllAsync();
}