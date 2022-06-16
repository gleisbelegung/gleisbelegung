using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Godot;
using Ionic.Zip;
using Octokit;
using Semver;

namespace Gleisbelegung.App
{
    public class Updater
    {
        private const string UpdaterFolderName = "Updater";
        private const string GithubOwnerName = "gleisbelegung";
        private const string GithubRepoName = "gleisbelegung";
        private const string DownloadedApplicationFileName = "Gleisbelegung.zip";
        private const string DataGleisbelegungFolderName = "data_Gleisbelegung";

        public static bool HasUpdateCapabilities()
        {
            var osName = OS.GetName();
            return !OS.HasFeature("editor") && osName == "Windows" || osName == "OSX" || osName == "X11";
        }

        public static void UpdateApplication()
        {
            System.Threading.Thread.Sleep(1000); // to assure that the previous process has enough time to close the application
            var latestRelease = GetLatestRelease();
            var assets = latestRelease.Assets;

            var osName = OS.GetName();
            // possible values are: "Android", "iOS", "HTML5", "OSX", "Server", "Windows", "UWP", "X11".

            ReleaseAsset asset = null;
            if (osName == "OSX")
            {
                asset = assets.FirstOrDefault(a => a.Name.Contains("Gleisbelegung.zip"));
            }
            else if (osName == "X11")
            {
                asset = assets.FirstOrDefault(a => a.Name.Contains("Linux.zip"));
            }
            else
            {
                asset = assets.FirstOrDefault(a => a.Name.Contains(osName));
            }

            var downloadUrl = asset.BrowserDownloadUrl;

            var executable = OS.GetExecutablePath();


            try
            {
                var webClient = new HttpClient();
                webClient.DefaultRequestHeaders.Add("user-agent", GetGithubUserAgent());
                DownloadFileTaskAsync(webClient, new Uri(downloadUrl), DownloadedApplicationFileName);

            }
            catch (System.Exception)
            {
                GD.Print("Download failed");
            }


            try
            {
                using (ZipFile zip = ZipFile.Read(DownloadedApplicationFileName))
                {
                    zip.ExtractAll("..", ExtractExistingFileAction.OverwriteSilently);
                }
            }
            catch (System.Exception) { }

            System.IO.File.Delete(DownloadedApplicationFileName);

            Process.Start(new ProcessStartInfo
            {
                FileName = OS.GetExecutablePath().Replace("/" + UpdaterFolderName, string.Empty),
                WorkingDirectory = "..",

            });

            System.Environment.Exit(0);
        }

        public static void DownloadFileTaskAsync(HttpClient client, Uri uri, string FileName)
        {
            using (var s = client.GetStreamAsync(uri).Result)
            {
                using (var fs = new FileStream(FileName, System.IO.FileMode.OpenOrCreate))
                {
                    s.CopyToAsync(fs).Wait();
                }
            }
        }

        public static bool IsUpdater()
        {
            var executable = OS.GetExecutablePath();
            return executable.Contains(UpdaterFolderName);
        }

        public static bool NeedsUpdate()
        {
            var newestRelease = GetLatestRelease();
            var newestVersionString = newestRelease.TagName.Replace("v", string.Empty);
            var currentVersionString = Updater.GetCurrentVersion();

            var newestVersion = SemVersion.Parse(newestVersionString, SemVersionStyles.Strict);
            var currentVersion = SemVersion.Parse(currentVersionString, SemVersionStyles.Strict);

            return newestVersion > currentVersion;
        }

        public static string GetCurrentVersion()
        {
            Godot.File versionFile = new Godot.File();
            versionFile.Open("res://version.txt", Godot.File.ModeFlags.Read);
            var currentVersionString = versionFile.GetLine();
            versionFile.Close();

            return currentVersionString;
        }

        public static string GetChangelogAsText()
        {
            var latestRelease = GetLatestRelease();
            return latestRelease.Body;
        }

        public static void PrepareAndExecuteUpdater()
        {
            var executable = OS.GetExecutablePath();
            var directoryPath = System.IO.Path.GetDirectoryName(executable);

            var updaterDirectoryPath = System.IO.Path.Combine(directoryPath, UpdaterFolderName);
            if (System.IO.Directory.Exists(updaterDirectoryPath))
            {
                System.IO.Directory.Delete(updaterDirectoryPath, true);
            }
            CopyFilesRecursively(new DirectoryInfo(directoryPath), new DirectoryInfo(updaterDirectoryPath));

            var updaterFileName = System.IO.Path.Combine(updaterDirectoryPath, System.IO.Path.GetFileName(executable));
            Process.Start(new ProcessStartInfo
            {
                FileName = updaterFileName,
                WorkingDirectory = UpdaterFolderName
            });

            System.Environment.Exit(0);
        }

        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
        }


        private static Release GetLatestRelease()
        {
            // todo: this is pad. Might not be necessary with godot 4.0
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;

            var client = new GitHubClient(new ProductHeaderValue(GetGithubUserAgent()));
            var releaseClient = client.Repository.Release;
            var allReleases = releaseClient.GetAll(GithubOwnerName, GithubRepoName).Result;
            var newestRelease = allReleases.Where(r => !r.Draft && !r.Prerelease).OrderByDescending(r => r.PublishedAt).FirstOrDefault();

            return newestRelease;
        }

        private static string GetGithubUserAgent()
        {
            return $"{GithubOwnerName}-{GithubRepoName}";
        }
    }
}