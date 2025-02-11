using System.Reflection;

namespace CQRS.Application.Members;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
