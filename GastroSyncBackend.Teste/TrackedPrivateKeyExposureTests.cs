using System.Diagnostics;
using System.Text;
using Xunit;

namespace GastroSyncBackend.Teste;

public class TrackedPrivateKeyExposureTests
{
    private static readonly byte[][] privateKeyMarkers =
    {
        Encoding.ASCII.GetBytes("-----BEGIN " + "PRIVATE KEY" + "-----"),
        Encoding.ASCII.GetBytes("-----BEGIN RSA " + "PRIVATE KEY" + "-----"),
        Encoding.ASCII.GetBytes("-----BEGIN EC " + "PRIVATE KEY" + "-----"),
        Encoding.ASCII.GetBytes("-----BEGIN ENCRYPTED " + "PRIVATE KEY" + "-----"),
        Encoding.ASCII.GetBytes("-----BEGIN OPENSSH " + "PRIVATE KEY" + "-----"),
        Encoding.ASCII.GetBytes("-----BEGIN DSA " + "PRIVATE KEY" + "-----")
    };

    [Fact]
    public void TrackedFilesDoNotContainPrivateKeyPemMarkers()
    {
        var repositoryRoot = FindRepositoryRoot();
        var privateKeyPaths = GetTrackedPaths(repositoryRoot)
            .Select(path => Path.Combine(repositoryRoot, path))
            .Where(File.Exists)
            .Where(ContainsPrivateKeyMarker)
            .ToArray();

        Assert.Empty(privateKeyPaths);
    }

    [Fact]
    public void EncryptedPrivateKeyPemMarkerIsDetected()
    {
        var marker = Encoding.ASCII.GetBytes("-----BEGIN ENCRYPTED " + "PRIVATE KEY" + "-----");

        Assert.True(ContainsPrivateKeyMarker(marker));
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "GastroSyncBackend.sln")))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new DirectoryNotFoundException("Repository root was not found.");
    }

    private static IEnumerable<string> GetTrackedPaths(string repositoryRoot)
    {
        var startInfo = new ProcessStartInfo("git", "ls-files -z")
        {
            WorkingDirectory = repositoryRoot,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        using var process = Process.Start(startInfo) ?? throw new InvalidOperationException("Git process did not start.");
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException("Git could not enumerate tracked files.");
        }

        return output.Split('\0', StringSplitOptions.RemoveEmptyEntries);
    }

    private static bool ContainsPrivateKeyMarker(string path)
    {
        var contents = File.ReadAllBytes(path);
        return ContainsPrivateKeyMarker(contents);
    }

    private static bool ContainsPrivateKeyMarker(byte[] contents)
    {
        return privateKeyMarkers.Any(marker => contents.AsSpan().IndexOf(marker) >= 0);
    }
}
