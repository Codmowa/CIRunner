using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
namespace PublishRunner
{
    public class RepoChangeListener : IRepoChangeListener
    {
        private readonly string path2Repo;

        public event EventHandler<Repository> NewCommit;

        public string FrendlyBranchName { get; }
        public string CurrentSHA { get; set; }
        public RepoChangeListener(string path2Repo, string frendlyBranchName)
        {
            this.path2Repo = path2Repo;
            FrendlyBranchName = frendlyBranchName;
        }

        public async Task  Start()
        {
            using var workingRepo = new Repository(path2Repo);
            while (true)
            {
                var targetBranch = workingRepo.Branches
                   .Where(b => b.FriendlyName == FrendlyBranchName).FirstOrDefault();
                if (targetBranch.IsCurrentRepositoryHead)
                {
                    var currentCommit = targetBranch.Commits.FirstOrDefault();
                    if (string.IsNullOrEmpty(CurrentSHA))
                    {
                        CurrentSHA = currentCommit.Sha;
                    }
                    else
                    {
                        if (CurrentSHA !=currentCommit.Sha)
                        {
                            CurrentSHA = currentCommit.Sha;
                            NewCommit?.Invoke(this, workingRepo);
                        }
                    }
                    await Task.Delay(10000);
                }
                await Task.Delay(10000);
            }
        }

    }
}
