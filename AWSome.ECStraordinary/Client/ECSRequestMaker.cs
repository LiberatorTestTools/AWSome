using Amazon.ECS;
using Amazon.ECS.Model;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AWSome.ECStraordinary.Client
{
    public partial class ECSConnector
    {
        #region Private Object Population Methods

        /// <summary>
        /// Creates a RunTaskRequest
        /// </summary>
        /// <param name="clusterName">The name of the cluster on which to run your task</param>
        /// <param name="groupName">The name of the task group to associate with the task</param>
        /// <param name="launchType">The launch type on which to run your task</param>
        /// <param name="networkConfiguration">The network configuration for the task. This parameter is required for task definitions that use the awsvpc network mode to receive their own elastic network interface, and it is not supported for other network modes.</param>
        /// <param name="taskOverride">A list of container overrides in JSON that specify the name of the container and the overrides it should receive</param>
        /// <param name="placementConstraints">An array of placement constraint objects to use for the task. You can specify up to 10 constraints per task (including constraints in the task definition and those specified at runtime).</param>
        /// <param name="placementStrategy">The placement strategy objects to use for the task. You can specify a maximum of five strategy rules per task.</param>
        /// <param name="platformVersion">The platform version the task should run. A platform version is only specified for tasks using the Fargate launch type. If one is not specified, the LATEST platform version is used by default.</param>
        /// <param name="propagateTags">Specifies whether to propogate the tags from the task definition or the service to the task.  If no value is specified, the tags are not propagated.</param>
        /// <param name="startedBy">An optional tag specified when a task is started</param>
        /// <param name="tags">A list of tags to associate with the task</param>
        /// <param name="taskDefinition">The family and revision (family:revision) or full ARN of the task definition to run</param>
        /// <param name="noOfInstantiations">The number of instantiations of the specified task to place on your cluster. You can specify up to 10 tasks per call.</param>
        /// <param name="enableECSManagedTags">Specifies whether to enable Amazon ECS Managed tags for the task</param>
        /// <returns>A new RunTaskRequest</returns>
        public RunTaskRequest PopulateRunTaskRequest(string clusterName, string groupName, string taskDefinition, [Optional]LaunchType launchType, [Optional]NetworkConfiguration networkConfiguration,
            [Optional]TaskOverride taskOverride, [Optional]List<PlacementConstraint> placementConstraints, [Optional]List<PlacementStrategy> placementStrategy,
            [Optional]string platformVersion, [Optional]PropagateTags propagateTags, [Optional]string startedBy, [Optional]List<Tag> tags,
            int noOfInstantiations = 1, bool enableECSManagedTags = true)
        {
            return new RunTaskRequest
            {
                Cluster = clusterName ?? null,
                Count = noOfInstantiations,
                EnableECSManagedTags = enableECSManagedTags,
                Group = groupName ?? null,
                LaunchType = launchType ?? null,
                NetworkConfiguration = networkConfiguration ?? null,
                Overrides = taskOverride ?? null,
                PlacementConstraints = placementConstraints ?? null,
                PlacementStrategy = placementStrategy ?? null,
                PlatformVersion = platformVersion ?? null,
                PropagateTags = propagateTags ?? null,
                StartedBy = startedBy ?? null,
                Tags = tags ?? null,
                TaskDefinition = taskDefinition
            };
        }


        /// <summary>
        /// Creates a DescribeClustersRequest
        /// </summary>
        /// <param name="clusters">A list of up to 100 cluster names or ARNs</param>
        /// <param name="include">Additional information about your clusters to be separated by launch type e.g. runningEC2TasksCount</param>
        /// <returns>A new DescribeClustersRequest</returns>
        public DescribeClustersRequest PopulateDescribeClustersRequest(List<string> clusters, [Optional]List<string> include)
        {
            return new DescribeClustersRequest
            {
                Clusters = clusters,
                Include = include ?? null
            };
        }


        /// <summary>
        /// Creates a CreateClusterRequest
        /// </summary>
        /// <param name="clusterName">The name of your cluster</param>
        /// <param name="tags">A list of tags to associate with the cluster</param>
        /// <returns>A new CreateClusterRequest</returns>
        public CreateClusterRequest PopulateCreateClusterRequest(string clusterName, [Optional]List<Tag> tags)
        {
            return new CreateClusterRequest
            {
                ClusterName = clusterName,
                Tags = tags ?? null
            };
        }



        /// <summary>
        /// Creates a DeleteClusterRequest
        /// </summary>
        /// <param name="clusterName"></param>
        /// <returns>A new DeleteClusterRequest</returns>
        public DeleteClusterRequest PopulateDeleteClusterRequest(string clusterName)
        {
            return new DeleteClusterRequest
            {
                Cluster = clusterName
            };
        }


        /// <summary>
        /// Creates a CreateServiceRequest
        /// </summary>
        /// <param name="clientToken">Unique, case-sensitive identifier that you provide to ensure the idempotency of the request</param>
        /// <param name="clusterName">The name of your cluster</param>
        /// <param name="deploymentController">The deployment controller to use for the service.</param>
        /// <param name="serviceName">The name of your service. Up to 255 letters (uppercase and lowercase), numbers, hyphens, and underscores are allowed. Service names must be unique within a cluster, but you can have similarly named services in multiple clusters within a Region or across multiple Regions.</param>
        /// <param name="launchType">The launch type on which to run your service. </param>
        /// <param name="networkConfiguration">The network configuration for the service. </param>
        /// <param name="role">The name or full Amazon Resource Name (ARN) of the IAM role that allows Amazon ECS to make calls to your load balancer on your behalf. </param>
        /// <param name="schedulingStrategy">The scheduling strategy to use for the service. For more information, see Services.</param>
        /// <param name="serviceRegistries">The details of the service discovery registries to assign to this service.</param>
        /// <param name="taskDefinition"></param>
        /// <param name="placementConstraints">An array of placement constraint objects to use for tasks in your service. You can specify a maximum of 10 constraints per task.</param>
        /// <param name="placementStrategy">The placement strategy objects to use for tasks in your service. You can specify a maximum of five strategy rules per service.</param>
        /// <param name="deploymentConfiguration"></param>
        /// <param name="loadBalancers">A load balancer object representing the load balancer to use with your service. If the service is using the ECS deployment controller, you are limited to one load balancer or target group.</param>
        /// <param name="platformVersion">The platform version on which your tasks in the service are running. A platform version is only specified for tasks using the Fargate launch type. If one is not specified, the LATEST platform version is used by default. </param>
        /// <param name="propagateTags">Specifies whether to propagate the tags from the task definition or the service to the tasks. If no value is specified, the tags are not propagated. </param>
        /// <param name="tags">A list of tags to associate with the service</param>
        /// <param name="enableECSManagedTags">Specifies whether to enable Amazon ECS managed tags for the tasks within the service. </param>
        /// <param name="healthCheckGracePeriodSeconds">The period of time, in seconds, that the Amazon ECS service scheduler should ignore unhealthy Elastic Load Balancing target health checks after a task has first started. This is only valid if your service is configured to use a load balancer. Default is set to 60 seconds.</param>
        /// <param name="desiredCount">The number of instantiations of the specified task definition to place and keep running on your cluster.</param>
        /// <returns>A new CreateServiceRequest</returns>
        public CreateServiceRequest PopulateCreateServiceRequest(string clientToken, string clusterName, DeploymentController deploymentController, string serviceName,
            LaunchType launchType, string role, SchedulingStrategy schedulingStrategy, List<ServiceRegistry> serviceRegistries, [Optional] string taskDefinition,
            [Optional]List<PlacementConstraint> placementConstraints, [Optional]List<PlacementStrategy> placementStrategy, [Optional]DeploymentConfiguration deploymentConfiguration,
            [Optional]List<LoadBalancer> loadBalancers, [Optional]string platformVersion, [Optional]PropagateTags propagateTags, [Optional]List<Tag> tags, [Optional]NetworkConfiguration networkConfiguration,
            bool enableECSManagedTags = true, int healthCheckGracePeriodSeconds = 60, int desiredCount = 1)
        {
            return new CreateServiceRequest
            {
                ClientToken = clientToken ?? null,
                Cluster = clusterName,
                DeploymentConfiguration = deploymentConfiguration ?? null,
                DeploymentController = deploymentController ?? null,
                DesiredCount = desiredCount,
                EnableECSManagedTags = enableECSManagedTags,
                HealthCheckGracePeriodSeconds = healthCheckGracePeriodSeconds,
                LaunchType = launchType ?? null,
                LoadBalancers = loadBalancers ?? null,
                NetworkConfiguration = networkConfiguration ?? null,
                PlacementConstraints = placementConstraints ?? null,
                PlacementStrategy = placementStrategy ?? null,
                PlatformVersion = platformVersion ?? null,
                PropagateTags = propagateTags ?? null,
                Role = role ?? null,
                SchedulingStrategy = schedulingStrategy ?? null,
                ServiceName = serviceName,
                ServiceRegistries = serviceRegistries ?? null,
                Tags = tags ?? null,
                TaskDefinition = taskDefinition ?? null
            };
        }


        /// <summary>
        /// Creates a DeleteServiceRequest
        /// </summary>
        /// <param name="clusterName">The short name or full Amazon Resource Name (ARN) of the cluster that hosts the service to delete. </param>
        /// <param name="serviceName">The name of the service to delete.</param>
        /// <param name="force">If true, allows you to delete a service even if it has not been scaled down to zero tasks. It is only necessary to use this if the service is using the REPLICA scheduling strategy.</param>
        /// <returns>A new DeleteServiceRequest</returns>
        public DeleteServiceRequest PopulateDeleteServiceRequest(string clusterName, string serviceName, bool force = false)
        {
            return new DeleteServiceRequest
            {
                Cluster = clusterName,
                Force = force,
                Service = serviceName
            };
        }


        /// <summary>
        /// Creates a DeregisterContainerInstanceRequest
        /// </summary>
        /// <param name="clusterName">The short name or full Amazon Resource Name (ARN) of the cluster that hosts the container instance to deregister.</param>
        /// <param name="containerInstance">The container instance ID or full ARN of the container instance to deregister. </param>
        /// <param name="force">Forces the deregistration of the container instance. The default is set to false.</param>
        /// <returns></returns>
        public DeregisterContainerInstanceRequest PopulateDeregisterContainerInstanceRequest(string clusterName, string containerInstance, bool force = false)
        {
            return new DeregisterContainerInstanceRequest
            {
                Cluster = clusterName,
                ContainerInstance = containerInstance,
                Force = force
            };
        }



        /// <summary>
        /// Creates a DeregisterTaskDefinitionRequest
        /// </summary>
        /// <param name="taskDefinition">The family and revision (family:revision) or full Amazon Resource Name (ARN) of the task definition to deregister. You must specify a revision.</param>
        /// <returns>A new DeregisterTaskDefinitionRequest</returns>
        public DeregisterTaskDefinitionRequest PopulateDeregisterTaskDefinitionRequest(string taskDefinition)
        {
            return new DeregisterTaskDefinitionRequest
            {
                TaskDefinition = taskDefinition
            };
        }



        /// <summary>
        /// Creates a DescribeContainerInstancesRequest
        /// </summary>
        /// <param name="clusterName">The short name or full Amazon Resource Name (ARN) of the cluster that hosts the container instances to describe.</param>
        /// <param name="containerInstances">A list of up to 100 container instance IDs or full Amazon Resource Name (ARN) entries.</param>
        /// <param name="include">Specifies whether you want to see the resource tags for the container instance. If TAGS is specified, the tags are included in the response. If this field is omitted, tags are not included in the response.</param>
        /// <returns>A new DescribeContainerInstancesRequest</returns>
        public DescribeContainerInstancesRequest PopulateDescribeContainerInstancesRequest(string clusterName, List<string> containerInstances, [Optional]List<string> include)
        {
            return new DescribeContainerInstancesRequest
            {
                Cluster = clusterName,
                ContainerInstances = containerInstances,
                Include = include ?? null
            };
        }



        /// <summary>
        /// Creates a DescribeServicesRequest
        /// </summary>
        /// <param name="clusterName">The short name or full Amazon Resource Name (ARN)the cluster that hosts the service to describe. </param>
        /// <param name="services">A list of services to describe. You may specify up to 10 services to describe in a single operation.</param>
        /// <param name="include">Specifies whether you want to see the resource tags for the service. If TAGS is specified, the tags are included in the response. If this field is omitted, tags are not included in the response.</param>
        /// <returns>A new DescribeServicesRequest</returns>
        public DescribeServicesRequest PopulateDescribeServicesRequest(string clusterName, List<string> services, [Optional]List<string> include)
        {
            return new DescribeServicesRequest
            {
                Cluster = clusterName,
                Services = services,
                Include = include
            };
        }



        /// <summary>
        /// Creates a DescribeTaskDefinitionRequest
        /// </summary>
        /// <param name="taskDefinition">The family for the latest ACTIVE revision, family and revision (family:revision) for a specific revision in the family, or full Amazon Resource Name (ARN) of the task definition to describe.</param>
        /// <param name="include">Specifies whether to see the resource tags for the task definition. If TAGS is specified, the tags are included in the response. If this field is omitted, tags are not included in the response.</param>
        /// <returns>A new DescribeTaskDefinitionRequest</returns>
        public DescribeTaskDefinitionRequest PopulateDescribeTaskDefinitionRequest(string taskDefinition, [Optional]List<string> include)
        {
            return new DescribeTaskDefinitionRequest
            {
                TaskDefinition = taskDefinition,
                Include = include ?? null
            };
        }



        /// <summary>
        /// Creates a DescribeTasksRequest
        /// </summary>
        /// <param name="clusterName">The name of your cluster</param>
        /// <param name="tasks">A list of up to 100 task IDs or ARNs</param>
        /// <param name="include">Additional information about your clusters to be separated by launch type e.g. runningEC2TasksCount</param>
        /// <returns>A new DescribeTasksRequest</returns>
        public DescribeTasksRequest PopulateDescribeTasksRequest(string clusterName, List<string> tasks, [Optional]List<string> include)
        {
            return new DescribeTasksRequest
            {
                Cluster = clusterName,
                Include = include ?? null,
                Tasks = tasks
            };
        }



        /// <summary>
        /// Creates a StartTaskRequest
        /// </summary>
        /// <param name="clusterName">The short name or full Amazon Resource Name (ARN) of the cluster on which to start your task. </param>
        /// <param name="containerInstances">The container instance IDs or full ARN entries for the container instances on which you would like to place your task. You can specify up to 10 container instances.</param>
        /// <param name="groupName">The name of the task group to associate with the task. The default value is the family name of the task definition (for example, family:my-family-name).</param>
        /// <param name="tags">A list of tags to associate with the task</param>
        /// <param name="networkConfiguration">The VPC subnet and security group configuration for tasks that receive their own elastic network interface by using the awsvpc networking mode.</param>
        /// <param name="taskDefinition">The family for the latest ACTIVE revision, family and revision (family:revision) for a specific revision in the family, or full Amazon Resource Name (ARN) of the task definition to describe.</param>
        /// <param name="taskOverride">A list of container overrides in JSON format that specify the name of a container in the specified task definition and the overrides it should receive. </param>
        /// <param name="propagateTags">Specifies whether to propagate the tags from the task definition or the service to the task. If no value is specified, the tags are not propagated.</param>
        /// <param name="startedBy">An optional tag specified when a task is started. For example, if you automatically trigger a task to run a batch process job, you could apply a unique identifier for that job to your task with the startedBy parameter. </param>
        /// <param name="enableECSManagedTags">Specifies whether to enable Amazon ECS managed tags for the task. Default set to true.</param>
        /// <returns>A new StartTaskRequest</returns>
        public StartTaskRequest PopulateStartTaskRequest(string clusterName, List<string> containerInstances, string groupName, [Optional]List<Tag> tags,
            [Optional]NetworkConfiguration networkConfiguration, [Optional] string taskDefinition, [Optional]TaskOverride taskOverride, [Optional]PropagateTags propagateTags,
            [Optional]string startedBy, bool enableECSManagedTags = true)
        {
            return new StartTaskRequest
            {
                Cluster = clusterName,
                ContainerInstances = containerInstances,
                Group = groupName,
                TaskDefinition = taskDefinition,
                EnableECSManagedTags = enableECSManagedTags,
                NetworkConfiguration = networkConfiguration ?? null,
                Overrides = taskOverride ?? null,
                PropagateTags = propagateTags ?? null,
                StartedBy = startedBy ?? null,
                Tags = tags ?? null
            };
        }


        /// <summary>
        /// Creates a StopTaskRequest
        /// </summary>
        /// <param name="clusterName">The short name or full Amazon Resource Name (ARN) of the cluster on which to start your task. </param>
        /// <param name="reason">An optional message specified when a task is stopped. </param>
        /// <param name="task">The task ID or full ARN entry of the task to stop.</param>
        /// <returns></returns>
        public StopTaskRequest PopulateStopTaskRequest(string clusterName, string task, [Optional] string reason)
        {
            return new StopTaskRequest
            {
                Cluster = clusterName,
                Reason = reason,
                Task = task
            };
        }


        /// <summary>
        /// Creates a TagResourceRequest
        /// </summary>
        /// <param name="resourceArn">The Amazon Resource Name (ARN) of the resource to which to add tags. </param>
        /// <param name="tags">The tags to add to the resource. A tag is an array of key-value pairs. Tag keys can have a maximum character length of 128 characters, and tag values can have a maximum length of 256 characters.</param>
        /// <returns></returns>
        public TagResourceRequest PopulateTagResourceRequest(string resourceArn, List<Tag> tags)
        {
            return new TagResourceRequest
            {
                ResourceArn = resourceArn,
                Tags = tags
            };
        }

        #endregion
    }
}
