namespace DotnetRestApiReference.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

// Should be used when entity not found when searching by unique identifier
public sealed class NotFoundException : DomainException
{
    // These additional properties are useful for logging and debugging
    public string EntityName { get; }
    public object Key { get; }

    public NotFoundException(string entityName, object key)
        : base($"{entityName} '{key}' not found.") 
        { EntityName = entityName; Key = key; }
}

// Should be used when there is a uniqueness or foreign key conflict.
public sealed class ConflictException : DomainException   
{
    public ConflictException(string message) : base(message) { }
}