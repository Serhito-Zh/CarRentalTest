using System.Reflection;

namespace CarRental.Domain;

/// <summary>
/// Reference for other projects
/// </summary>
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}