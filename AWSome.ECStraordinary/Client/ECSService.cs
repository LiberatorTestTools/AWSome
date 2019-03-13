using Amazon.ECS;
using Amazon.ECS.Model;
using Amazon.Runtime;
using Liberator.AWSome.ECStraordinary.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Liberator.AWSome.ECStraordinary.Client
{
    /// <summary>
    /// The connector for the Amazon Elastic Container Service
    /// </summary>
    public partial class ECSConnector
    {
        /// <summary>
        /// The credentials for the current user
        /// </summary>
        public AWSCredentials UserAWSCredentials = Preferences.GetAWSCredentials();

        /// <summary>
        /// The configuration for the Elastic Container Service
        /// </summary>
        public AmazonECSConfig ECSConfig = Preferences.GetECSConfig();

        /// <summary>
        /// Runs a task on an ECS cluster
        /// </summary>
        /// <param name="runTaskRequest">Request used to run the task</param>
        /// <param name="tags">tags associated with task</param>
        /// <returns>The run task response</returns>
        public AmazonWebServiceResponse RunTask(RunTaskRequest runTaskRequest, [Optional]List<Tag> tags)
        {
            using (AmazonECSClient ECSClient = new AmazonECSClient(UserAWSCredentials, ECSConfig))
            {
                try
                {
                    if (tags != null)
                    {
                        TagResourceRequest tagResourceRequest = new TagResourceRequest
                        {
                            Tags = tags
                        };
                        Task<TagResourceResponse> task = ECSClient.TagResourceAsync(tagResourceRequest);
                        task.Wait();
                    }
                    Task<RunTaskResponse> runTask = ECSClient.RunTaskAsync(runTaskRequest);
                    runTask.Wait();
                    return runTask.Result;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption($"Run task with task definition {runTaskRequest.TaskDefinition} failed", e);
                }
            };
        }

        /// <summary>
        /// Lists the Clusters found on the client
        /// </summary>
        /// <param name="describeClustersRequest">Request sent to get a list of clusters and their properties</param>
        /// <returns>A list of Clusters found on the client</returns>
        public List<Cluster> ListECSClusters(DescribeClustersRequest describeClustersRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DescribeClustersResponse> task = ECSClient.DescribeClustersAsync(describeClustersRequest);
                    task.Wait();
                    DescribeClustersResponse response = task.Result;
                    List<Cluster> clusters = response.Clusters;
                    foreach (Cluster cluster in clusters)
                    {
                        Console.WriteLine("Found cluster name {0} with status {1}", cluster.ClusterName, cluster.Status);
                    }
                    return clusters;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption("Cluster listing has failed.", e);
                }
            }
        }

        /// <summary>
        /// Creates a new cluster
        /// </summary>
        /// <param name="createClustersRequest">Request sent to create a new cluster</param>
        /// <param name="tags">tags associated with task</param>
        /// <returns>The Cluster that has been created</returns>
        public Cluster CreateNewCluster(CreateClusterRequest createClustersRequest, [Optional] List<Tag> tags)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    if (tags != null)
                    {
                        createClustersRequest.Tags = tags;
                    }
                    Task<CreateClusterResponse> task = ECSClient.CreateClusterAsync(createClustersRequest);
                    task.Wait();
                    CreateClusterResponse response = task.Result;
                    Cluster cluster = response.Cluster;
                    return cluster;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption("Cluster creation has failed.", e);
                }
            }
        }

        /// <summary>
        /// Lists the tasks found on the cluster
        /// </summary>
        /// <param name="describsTasksRequest">Request sent to get a list of tasks and their properties</param>
        /// <returns>List of tasks</returns>
        public List<Amazon.ECS.Model.Task> ListClusterTasks(DescribeTasksRequest describsTasksRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DescribeTasksResponse> describeTask = ECSClient.DescribeTasksAsync(describsTasksRequest);
                    describeTask.Wait();
                    DescribeTasksResponse response = describeTask.Result;
                    List<Amazon.ECS.Model.Task> tasks = response.Tasks;
                    foreach (var task in tasks)
                    {
                        Console.WriteLine("Found task name {0} with last status of {1}", task.Group, task.LastStatus);
                    }
                    return tasks;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption("Task listing has failed.", e);
                }
            }
        }

        /// <summary>
        /// Creates a new service for the cluster
        /// </summary>
        /// <param name="createServiceRequest">Request sent to create a service</param>
        /// <param name="tags">tags associated with task</param>
        /// <returns>The create service response</returns>
        public CreateServiceResponse CreateService(CreateServiceRequest createServiceRequest, [Optional] List<Tag> tags)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    if (tags != null)
                    {
                        createServiceRequest.Tags = tags;
                    }
                    Task<CreateServiceResponse> task = ECSClient.CreateServiceAsync(createServiceRequest);
                    task.Wait();
                    CreateServiceResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption("Creating service response has failed.", e);
                }
            }
        }

        /// <summary>
        /// Deletes a cluster
        /// </summary>
        /// <param name="deleteClusterRequest">Request to delete a cluster</param>
        /// <returns>The delete cluster response</returns>
        public DeleteClusterResponse DeleteCluster(DeleteClusterRequest deleteClusterRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DeleteClusterResponse> task = ECSClient.DeleteClusterAsync(deleteClusterRequest);
                    task.Wait();
                    DeleteClusterResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption($"Deleting cluster with name {deleteClusterRequest.Cluster} has failed.", e);
                }
            }
        }

        /// <summary>
        /// Deletes a service from a cluster
        /// </summary>
        /// <param name="deleteServiceRequest">Request to delete a service</param>
        /// <returns>The delete service response</returns>
        public DeleteServiceResponse DeleteService(DeleteServiceRequest deleteServiceRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DeleteServiceResponse> task = ECSClient.DeleteServiceAsync(deleteServiceRequest);
                    task.Wait();
                    DeleteServiceResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption($"Deleting service with name {deleteServiceRequest.Service} from cluster {deleteServiceRequest.Cluster} has failed.", e);
                }
            }
        }


        /// <summary>
        /// Deregisters a container instance
        /// </summary>
        /// <param name="deregisterContainerInstanceRequest">Request to deregister a container instance</param>
        /// <returns>The deregister container instance response</returns>
        public DeregisterContainerInstanceResponse DeregisterContainerInstance(DeregisterContainerInstanceRequest deregisterContainerInstanceRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DeregisterContainerInstanceResponse> task = ECSClient.DeregisterContainerInstanceAsync(deregisterContainerInstanceRequest);
                    task.Wait();
                    DeregisterContainerInstanceResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption($"Deregistering container with ID {deregisterContainerInstanceRequest.ContainerInstance} from cluster {deregisterContainerInstanceRequest.Cluster} has failed.", e);
                }
            }
        }



        /// <summary>
        /// Deregisters a task definition
        /// </summary>
        /// <param name="deregisterTaskDefinitionRequest">Request to deregister a task definition</param>
        /// <returns>The deregister task definition resposne</returns>
        public DeregisterTaskDefinitionResponse DeregisterTaskDefinition(DeregisterTaskDefinitionRequest deregisterTaskDefinitionRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DeregisterTaskDefinitionResponse> task = ECSClient.DeregisterTaskDefinitionAsync(deregisterTaskDefinitionRequest);
                    task.Wait();
                    DeregisterTaskDefinitionResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption($"Deregistering task definition with ARN {deregisterTaskDefinitionRequest.TaskDefinition} has failed.", e);
                }
            }
        }


        /// <summary>
        /// Lists the container instances
        /// </summary>
        /// <param name="describeContainerInstancesRequest">A request to get the list of container instances</param>
        /// <returns>A list of container instances</returns>
        public List<ContainerInstance> ListContainerInstances(DescribeContainerInstancesRequest describeContainerInstancesRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DescribeContainerInstancesResponse> task  = ECSClient.DescribeContainerInstancesAsync(describeContainerInstancesRequest);
                    task.Wait();
                    DescribeContainerInstancesResponse response = task.Result;
                    List<ContainerInstance> containerInstances = response.ContainerInstances;
                    foreach (var instance in containerInstances)
                    {
                        Console.WriteLine("Found instance id {0} with last status of {1}", instance.Ec2InstanceId, instance.Status);
                    }
                    return containerInstances;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption("Listing container instances has failed.", e);
                }
            }
        }

        /// <summary>
        /// Gets the task definition
        /// </summary>
        /// <param name="describeTaskDefinitionRequest">Request to get a task definition</param>
        /// <returns>The describe task definition response</returns>
        public DescribeTaskDefinitionResponse GetTaskDefinition(DescribeTaskDefinitionRequest describeTaskDefinitionRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DescribeTaskDefinitionResponse> task = ECSClient.DescribeTaskDefinitionAsync(describeTaskDefinitionRequest);
                    task.Wait();
                    DescribeTaskDefinitionResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption("Getting task definition has failed.", e);
                }
            }
        }


        /// <summary>
        /// Gets the description of services
        /// </summary>
        /// <param name="describeServicesRequest">Request to describe the services</param>
        /// <returns>The describe service response</returns>
        public DescribeServicesResponse DescribeServices(DescribeServicesRequest describeServicesRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<DescribeServicesResponse> task = ECSClient.DescribeServicesAsync(describeServicesRequest);
                    task.Wait();
                    DescribeServicesResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption("Describing services has failed.", e);
                }
            }
        }


        /// <summary>
        /// Starts a task
        /// </summary>
        /// <param name="startTaskRequest">Request to start a task</param>
        /// <returns>The start task response</returns>
        public StartTaskResponse StartTask(StartTaskRequest startTaskRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<StartTaskResponse> task = ECSClient.StartTaskAsync(startTaskRequest);
                    task.Wait();
                    StartTaskResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption($"Starting a task in cluster {startTaskRequest.Cluster} has failed.", e);
                }
            }
        }


        /// <summary>
        /// Stopping a task
        /// </summary>
        /// <param name="stopTaskRequest">Request to stop a task</param>
        /// <returns>A stop task response</returns>
        public StopTaskResponse StopTask(StopTaskRequest stopTaskRequest)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<StopTaskResponse> task = ECSClient.StopTaskAsync(stopTaskRequest);
                    task.Wait();
                    StopTaskResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption($"Stopping task {stopTaskRequest.Task} has failed.", e);
                }
            }
        }


        /// <summary>
        /// Tags a resource
        /// </summary>
        /// <param name="tagResourceRequest">Request to tag a resource</param>
        /// <param name="tags">List of tags</param>
        /// <returns>A tag resource response</returns>
        public TagResourceResponse TagResource(TagResourceRequest tagResourceRequest, List<Tag> tags)
        {
            using (AmazonECSClient ECSClient = Preferences.GetECSClient())
            {
                try
                {
                    Task<TagResourceResponse> task = ECSClient.TagResourceAsync(tagResourceRequest);
                    task.Wait();
                    TagResourceResponse response = task.Result;
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSeption($"Adding tags to resource with Resource Arn {tagResourceRequest.ResourceArn} has failed.", e);
                }
            }
        }
    }
}
