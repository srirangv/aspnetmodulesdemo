namespace Helpers;

public interface IModule
{
    void AddRoutes(IEndpointRouteBuilder app);
}

public static class ModuleExtensions
{
    public static void MapRoutes(this IEndpointRouteBuilder builder, IModule module)
    {
        module.AddRoutes(builder);
    }
}