using System.Reflection;

/// <summary>
/// Just Tools By RemziStudios
/// </summary>
public static class AssemblyManager
{
    public static Assembly GetReferencedAssembly(this Assembly assembly, string Name)
    {
        var references = assembly.GetReferencedAssemblies();

        if (references.GetLength(0) > 0)
        {
            for (int i = 0; i < references.Length; i++)
            {
                if (references[i].Name == Name)
                {
                    return Assembly.Load(references[i]);
                }
            }
        }

        return null;
    }
}