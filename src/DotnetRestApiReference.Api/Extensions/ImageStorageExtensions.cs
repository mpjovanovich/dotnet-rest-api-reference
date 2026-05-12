using Microsoft.Extensions.FileProviders;

namespace DotnetRestApiReference.Api.Extensions;

/*
This extension method serves static images from the configured physical path.
*/
internal static class ImageStorageExtensions
{
    // Serve up some images to make the API more interesting.
    public static WebApplication UseImageStorage(this WebApplication app)
    {
        var storagePath = app.Configuration["Storage:PhysicalPath"];
        if (storagePath is null)
            throw new Exception("Storage:PhysicalPath is not set");

        var physicalPath = Path.GetFullPath(
            Path.Combine(app.Environment.ContentRootPath, storagePath));
        if (!Directory.Exists(physicalPath))
            throw new Exception($"Storage:PhysicalPath {physicalPath} does not exist");

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(physicalPath),
            RequestPath = "/images"
        });

        return app;
    }
}
