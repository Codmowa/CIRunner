using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PublishRunner
{
    interface IRepoChangeListener
    {
        event EventHandler<Repository> NewCommit;
        Task Start();
    }
}
