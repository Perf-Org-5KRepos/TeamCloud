/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using TeamCloud.Data;
using TeamCloud.Model.Internal.Data;
using TeamCloud.Orchestration;

namespace TeamCloud.Orchestrator.Activities
{
    public class ProjectDeleteActivity
    {
        private readonly IProjectsRepository projectsRepository;

        public ProjectDeleteActivity(IProjectsRepository projectsRepository)
        {
            this.projectsRepository = projectsRepository ?? throw new ArgumentNullException(nameof(projectsRepository));
        }

        [FunctionName(nameof(ProjectDeleteActivity))]
        public async Task RunActivity(
            [ActivityTrigger] Project project)
        {
            if (project is null)
                throw new ArgumentNullException(nameof(project));

            _ = await projectsRepository
                .RemoveAsync(project)
                .ConfigureAwait(false);
        }
    }

    internal static class ProjectDeleteExtension
    {
        public static Task<Project> DeleteProjectAsync(this IDurableOrchestrationContext functionContext, Project project)
            => functionContext.CallActivityWithRetryAsync<Project>(nameof(ProjectDeleteActivity), project);
    }
}
