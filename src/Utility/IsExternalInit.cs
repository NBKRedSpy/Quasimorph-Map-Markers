using System.ComponentModel;

//This is a workaround for C# 9 init-only properties in .NET Framework projects.
namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit { }
}