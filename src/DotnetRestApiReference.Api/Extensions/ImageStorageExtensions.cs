using Microsoft.Extensions.FileProviders;

namespace DotnetRestApiReference.Api.Extensions;

/*
This extension method serves static images from the configured physical path.
*/
internal static class ImageStorageExtensions
{
    public static WebApplication UseImageStorage(this WebApplication app)
    {
        var basePath = app.Configuration["Storage:BasePath"];
        if (basePath is null)
            throw new Exception("Storage:BasePath is not set");

        var imagesPath = app.Configuration["Storage:ImagesPath"];
        if (imagesPath is null)
            throw new Exception("Storage:ImagesPath is not set");

        var physicalPath = Path.GetFullPath(
            Path.Combine(app.Environment.ContentRootPath, basePath, imagesPath));
        if (!Directory.Exists(physicalPath))
            throw new Exception($"Storage:PhysicalPath {physicalPath} does not exist");

        // Map physical path to route /images
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(physicalPath),
            RequestPath = "/images"
        });

        return app;
    }
}
