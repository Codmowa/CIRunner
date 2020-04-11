using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LibGit2Sharp;
using Microsoft.Extensions.Options;
using System.IO;

namespace PublishRunner
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RunnerTask config;

        public static string CurrentSHA = "";
        public Worker(ILogger<Worker> logger,IOptions<RunnerTask> configs)
        {
            _logger = logger;
            this.config = configs.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //Check Git Status
                var path2Repo = Path.Join(config.Path2Repo, ".git");

                var bash = new CommandRunner("bash", config.Path2Repo);
                var commands = new string[] { "git push", "./publish.sh" };
                foreach (var item in commands)
                {
                    Console.WriteLine(bash.Run(item));
                }
                //var repoListener = new RepoChangeListener(path2Repo, config.Branch);

                //repoListener.NewCommit += StartPublish;
                //await repoListener.Start();
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void StartPublish(object sender, Repository e)
        {

        }
    }
}
