using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using System.IO;

class Build : NukeBuild
{ 
    public static int Main () => Execute<Build>(x => x.Deploy);

    [Solution(GenerateProjects = true)]
    readonly Solution Solution;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean => _ => _
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(s => s
                .SetProject(Solution.NukeApp)
                .SetConfiguration(Configuration));
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetRestore(s => s
                .SetProjectFile(Solution.NukeApp));
        }); 

    //Target Compile => _ => _
    //    .DependsOn(Restore)
    //    .Executes(() =>
    //    {
    //        DotNetTasks.DotNetBuild(s => s
    //            .SetProjectFile(Solution.NukeApp)
    //            .SetConfiguration(Configuration));
    //    });

    Target Tests => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution.NukeAppTests)
                .SetConfiguration(Configuration));
        });

    Target Deploy => _ => _
        .DependsOn(Tests)
        .Executes(() =>
        {
            string dockerFile = Path.Combine(Solution.NukeApp.Directory, "Dockerfile");

            Assert.True(File.Exists(dockerFile), "Dockerfile not found.");

            string imageTag = Solution.NukeApp.GetProperty("DockerfileTag");

            DockerTasks.DockerBuild(s => s
                .SetPath(".")
                .SetFile(dockerFile)
                .SetTag($"{imageTag}:latest"));
        });
}
