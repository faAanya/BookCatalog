using Microsoft.AspNetCore.Mvc;

public class FileService
{
    private readonly HttpClient _httpClient = new();
    private readonly string _downloadsPath;

    public FileService(IWebHostEnvironment env)
    {
        _downloadsPath = Path.Combine(env.ContentRootPath, "Downloads");
        if (!Directory.Exists(_downloadsPath))
            Directory.CreateDirectory(_downloadsPath);
    }

    public string GetFilePath(string fileName)
    {
        var filePath = Path.Combine(_downloadsPath, fileName);
        return File.Exists(filePath) ? filePath : null;
    }

    public async Task<string> DownloadFile(string fileUrl, CancellationToken cancellationToken)
    {
        using var response = await _httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();
        var fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);

        GenerateFileName(fileName);

        var filePath = Path.Combine(_downloadsPath, fileName);

        await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await contentStream.CopyToAsync(fileStream, cancellationToken);

        return fileName;
    }

    public static string GenerateFileName(string fileName)
    {

        if (string.IsNullOrWhiteSpace(fileName))
            fileName = $"download_{Guid.NewGuid()}";

        foreach (var c in Path.GetInvalidFileNameChars())
            fileName = fileName.Replace(c, '_');
        return fileName;
    }
}