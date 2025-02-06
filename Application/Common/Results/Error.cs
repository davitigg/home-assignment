namespace Application.Common.Results
{
    public sealed record Error(string Code, string Description)
    {
        internal static Error None => new(ErrorTypeConstant.None, string.Empty);
        internal static Error UserNameAlreadyExists => new(ErrorTypeConstant.Conflict, "User name already exists");
        internal static Error UserNotFound => new(ErrorTypeConstant.NotFound, "User not found");
    }
}
