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
    /// 
    /// </summary>
    public partial class ECSConnector
    {
        /// <summary>
        /// 
        /// </summary>
        public AWSCredentials UserAWSCredentials = Preferences.GetAWSCredentials();

        /// <summary>
        /// 
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
                        TagResourceRequest tagResourceRequest = new TagResourceRequest();
                        tagResourceRequest.Tags = tags;
                        ECSClient.TagResource(tagResourceRequest);
                    }
                    return ECSClient.RunTask(runTaskRequest);
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException($"Run task with task definition {runTaskRequest.TaskDefinition} failed", e);
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
                    DescribeClustersResponse response = ECSClient.DescribeClusters(describeClustersRequest);
                    List<Cluster> clusters = response.Clusters;
                    foreach (Cluster cluster in clusters)
                    {
                        Console.WriteLine("Found cluster name {0} with status {1}", cluster.ClusterName, cluster.Status);
                    }
                    return clusters;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException("Cluster listing has failed.", e);
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
                    CreateClusterResponse response = ECSClient.CreateCluster(createClustersRequest);
                    Cluster cluster = response.Cluster;
                    return cluster;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException("Cluster creation has failed.", e);
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
                    DescribeTasksResponse response = ECSClient.DescribeTasks(describsTasksRequest);
                    List<Amazon.ECS.Model.Task> tasks = response.Tasks;
                    foreach (var task in tasks)
                    {
                        Console.WriteLine("Found task name {0} with last status of {1}", task.Group, task.LastStatus);
                    }
                    return tasks;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException("Task listing has failed.", e);
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
                    CreateServiceResponse response = ECSClient.CreateService(createServiceRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException("Creating service response has failed.", e);
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
                    DeleteClusterResponse response = ECSClient.DeleteCluster(deleteClusterRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException($"Deleting cluster with name {deleteClusterRequest.Cluster} has failed.", e);
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
                    DeleteServiceResponse response = ECSClient.DeleteService(deleteServiceRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException($"Deleting service with name {deleteServiceRequest.Service} from cluster {deleteServiceRequest.Cluster} has failed.", e);
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
                    DeregisterContainerInstanceResponse response = ECSClient.DeregisterContainerInstance(deregisterContainerInstanceRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException($"Deregistering container with ID {deregisterContainerInstanceRequest.ContainerInstance} from cluster {deregisterContainerInstanceRequest.Cluster} has failed.", e);
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
                    DeregisterTaskDefinitionResponse response = ECSClient.DeregisterTaskDefinition(deregisterTaskDefinitionRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException($"Deregistering task definition with ARN {deregisterTaskDefinitionRequest.TaskDefinition} has failed.", e);
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
                    DescribeContainerInstancesResponse response = ECSClient.DescribeContainerInstances(describeContainerInstancesRequest);
                    List<ContainerInstance> containerInstances = response.ContainerInstances;
                    foreach (var instance in containerInstances)
                    {
                        Console.WriteLine("Found instance id {0} with last status of {1}", instance.Ec2InstanceId, instance.Status);
                    }
                    return containerInstances;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException("Listing container instances has failed.", e);
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
                    DescribeTaskDefinitionResponse response = ECSClient.DescribeTaskDefinition(describeTaskDefinitionRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException("Getting task definition has failed.", e);
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
                    DescribeServicesResponse response = ECSClient.DescribeServices(describeServicesRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException("Describing services has failed.", e);
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
                    StartTaskResponse response = ECSClient.StartTask(startTaskRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException($"Starting a task in cluster {startTaskRequest.Cluster} has failed.", e);
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
                    StopTaskResponse response = ECSClient.StopTask(stopTaskRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException($"Stopping task {stopTaskRequest.Task} has failed.", e);
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
                    TagResourceResponse response = ECSClient.TagResource(tagResourceRequest);
                    return response;
                }
                catch (AmazonECSException e)
                {
                    throw new ECSException($"Adding tags to resource with Resource Arn {tagResourceRequest.ResourceArn} has failed.", e);
                }
            }
        }
    }
}
