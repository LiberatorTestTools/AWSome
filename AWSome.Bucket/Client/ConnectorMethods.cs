using Amazon.S3;
using Amazon.S3.Model;
using Liberator.AWSome.Bucket.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Liberator.AWSome.Bucket.Client
{
    /// <summary>
    /// Connector for to S3 buckets.
    /// </summary>
    public partial class Connector
    {
        /// <summary>
        /// Lists the S3 buckets found on the client.
        /// </summary>
        /// <returns>List of S3 bucket objects.</returns>
        public List<S3Bucket> GetListOfS3Buckets()
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    Task<ListBucketsResponse> response = s3Client.ListBucketsAsync();
                    response.Wait();
                    List<S3Bucket> buckets = response.Result.Buckets;
                    return buckets;
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("Bucket listing has failed.", e);
                }
            }
        }


        /// <summary>
        /// Copies an object from one bucket to another.
        /// </summary>
        /// <param name="sourceBucket">The name of the bucket containing the object to copy.</param>
        /// <param name="sourceKey">The key of the object to copy.</param>
        /// <param name="sourceVersionId">Specifies a particular version of the source object to copy. By default the latest version is copied.</param>
        /// <param name="destinationBucket">The name of the bucket to contain the copy of the source object.</param>
        /// <param name="destinationKey">The key to be given to the copy of the source object.</param>
        /// <returns>The response from the server.</returns>
        public CopyObjectResponse CopyObject(string sourceBucket, string sourceKey, string sourceVersionId, string destinationBucket, string destinationKey)
        {
            try
            {
                using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
                {
                    CopyObjectRequest copyObjectRequest = PopulateCopyObjectRequest(sourceBucket, sourceKey, sourceVersionId, destinationBucket, destinationKey);
                    Task<CopyObjectResponse> copyObjectResponse = s3Client.CopyObjectAsync(copyObjectRequest);
                    copyObjectResponse.Wait();
                    return copyObjectResponse.Result;
                }
            }
            catch (AmazonS3Exception e)
            {
                throw new BucketException("Unable to copy item.", e);
            }
        }


        /// <summary>
        /// Copies an object from one bucket to another.
        /// </summary>
        /// <param name="copyObjectRequest">A request object detailing the S3 object to copy.</param>
        /// <returns>The response from the server.</returns>
        public CopyObjectResponse CopyObject(CopyObjectRequest copyObjectRequest)
        {
            try
            {
                using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
                {
                    Task<CopyObjectResponse> copyObjectResponse = s3Client.CopyObjectAsync(copyObjectRequest);
                    copyObjectResponse.Wait();
                    return copyObjectResponse.Result;
                }
            }
            catch (AmazonS3Exception e)
            {
                throw new BucketException("Unable to copy item.", e);
            }
        }


        /// <summary>
        /// Creates a new bucket on the client.
        /// </summary>
        /// <param name="newBucketName">The name of the new bucket to create.</param>
        /// <param name="s3Region">[Optional] The S3 region to use.</param>
        /// <param name="bucketRegionName">[Optional] The bucket region locality.</param>
        /// <param name="s3CannedACL">[Optional] The canned access control list to apply to the request.</param>
        /// <param name="s3Grants">[Optional] Gets the access control list.</param>
        /// <returns>The response from the server.</returns>
        public PutBucketResponse CreateS3Bucket(string newBucketName, [Optional] S3Region s3Region, 
            [Optional]string bucketRegionName, [Optional]S3CannedACL s3CannedACL, [Optional]List<S3Grant> s3Grants)
        {
            try
            {
                using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
                {
                    PutBucketRequest putBucketRequest = PopulatePutBucketRequest(newBucketName, s3Region, s3CannedACL, s3Grants);
                    Task<PutBucketResponse> putObjectResponse = s3Client.PutBucketAsync(putBucketRequest);
                    putObjectResponse.Wait();
                    return putObjectResponse.Result;
                }
            }
            catch (AmazonS3Exception e)
            {
                throw new BucketException("Bucket creation has failed.", e);
            }
        }


        /// <summary>
        /// Creates a new bucket on the client.
        /// </summary>
        /// <param name="putBucketRequest">A request object detailing the S3 bucket to create.</param>
        /// <returns>The response from the server.</returns>
        public PutBucketResponse CreateS3Bucket(PutBucketRequest putBucketRequest)
        {
            try
            {
                using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
                {
                    Task<PutBucketResponse> putBucketResponse = s3Client.PutBucketAsync(putBucketRequest);
                    putBucketResponse.Wait();
                    return putBucketResponse.Result;
                }
            }
            catch (AmazonS3Exception e)
            {
                throw new BucketException("Bucket creation has failed.", e);
            }
        }


        /// <summary>
        /// Creates a folder within a named bucket.
        /// </summary>
        /// <param name="bucketName">The name of the bucket.</param>
        /// <param name="folderName">The name of the new folder to create.</param>
        /// <returns>The response from the server.</returns>
        public PutObjectResponse CreateS3Folder(string bucketName, string folderName)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    string folderKey = string.Concat(folderName, "/");
                    PutObjectRequest folderRequest = PopulatePutObjectRequest(bucketName, null, null, folderKey, null, stream: new MemoryStream(new byte[0]));
                    Task<PutObjectResponse> task = s3Client.PutObjectAsync(folderRequest);
                    task.Wait();
                    return task.Result;
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("Folder creation has failed.", e);
                }
            }
        }


        /// <summary>
        /// Checks if a folder exists in a named S3 bucket.
        /// </summary>
        /// <param name="bucketName">The name of the bucket.</param>
        /// <param name="folderPath">he nameof the folder to check for.</param>
        /// <returns>True if the folder exists.</returns>
        public bool CheckFolderExistsInS3Bucket(string bucketName, string folderPath)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    ListObjectsRequest findFolderRequest = PopulateListObjectsRequest(bucketName, "/", folderPath);
                    
                    Task<ListObjectsResponse> findFolderResponse = s3Client.ListObjectsAsync(findFolderRequest);
                    findFolderResponse.Wait();
                    List<string> commonPrefixes = findFolderResponse.Result.CommonPrefixes;
                    return commonPrefixes.Any();
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("Folder existence check has failed.", e);
                }
            }
        }


        /// <summary>
        /// Adds a file to an S3 bucket.
        /// </summary>
        /// <param name="bucketName">The name of the bucket.</param>
        /// <param name="filePath">The path to the file to upload.</param>
        /// <returns>The response from the server.</returns>
        public PutObjectResponse PutFileInS3Bucket(string bucketName, string filePath)
        {
            FileInfo filename = new FileInfo(filePath);
            string contents = File.ReadAllText(filename.FullName);

            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    PutObjectRequest putObjectRequest = PopulatePutObjectRequest(bucketName, null, contents, filename.Name, null);
                    putObjectRequest.Metadata.Add("type", "log");
                    Task<PutObjectResponse> putObjectResponse = s3Client.PutObjectAsync(putObjectRequest);
                    putObjectResponse.Wait();
                    return putObjectResponse.Result;
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("File creation has failed.", e);
                }
            }
        }


        /// <summary>
        /// List objects in a bucket to the console
        /// </summary>
        /// <param name="bucketName">The name of the bucket</param>
        /// <returns>List of S3 bucket objects</returns>
        public List<S3Object> ListObjectsInS3Bucket(string bucketName)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    ListObjectsRequest listObjectsRequest = PopulateListObjectsRequest(bucketName, null);
                    Task<ListObjectsResponse> listObjectsResponse = s3Client.ListObjectsAsync(listObjectsRequest);
                    listObjectsResponse.Wait();
                    return listObjectsResponse.Result.S3Objects;
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("Object listing has failed.", e);
                }
            }
        }


        /// <summary>
        /// Lists the objects in a named folder
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the folder.</param>
        /// <param name="pathToFolder">The path to the folder to enumerate.</param>
        /// <returns>List of S3 bucket objects.</returns>
        public List<S3Object> ListObjectsInS3Folder(string bucketName, string pathToFolder)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    ListObjectsRequest listObjectsRequest = PopulateListObjectsRequest(bucketName, null, pathToFolder);
                    Task<ListObjectsResponse> listObjectsResponse = s3Client.ListObjectsAsync(listObjectsRequest);
                    listObjectsResponse.Wait();
                    return listObjectsResponse.Result.S3Objects;
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("Object listing has failed.", e);
                }
            }
        }


        /// <summary>
        /// Adds a local file to the S3 bucket
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the folder</param>
        /// <param name="folderName">The name of the folder to upload to</param>
        /// <param name="filePath">The local oath to the file being uploaded</param>
        public PutObjectResponse AddTextFileToS3Folder(string bucketName, string folderName, string filePath)
        {
            string delimiter = "/";
            FileInfo filename = new FileInfo(filePath);
            string contents = File.ReadAllText(filename.FullName);

            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    PutObjectRequest putObjectRequest = PopulatePutObjectRequest(string.Concat(bucketName, delimiter, folderName), filePath, contents, filename.Name, null);
                    Task<PutObjectResponse> putObjectResponse = s3Client.PutObjectAsync(putObjectRequest);
                    putObjectResponse.Wait();
                    return putObjectResponse.Result;
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("File creation within folder has failed.", e);
                }
            }
        }


        /// <summary>
        /// Deletes an item from an S3 bucket
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the item</param>
        /// <param name="filePath">The path to the file to be deleted</param>
        public DeleteObjectResponse DeleteItemFromS3Bucket(string bucketName, string filePath)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    DeleteObjectRequest deleteObjectRequest = PopulateDeleteObjectRequest(bucketName, filePath);
                    Task<DeleteObjectResponse> deleteObjectResponse = s3Client.DeleteObjectAsync(deleteObjectRequest);
                    deleteObjectResponse.Wait();
                    return deleteObjectResponse.Result;
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("Object deletion has failed.", e);
                }
            }
        }


        /// <summary>
        /// Deletes a subfolder from an S3 folder
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the folder</param>
        /// <param name="folderName">The path of the folder</param>
        /// <param name="subFolder">The name of the subfolder to delete</param>
        public DeleteObjectResponse DeleteSubFolderFromS3Bucket(string bucketName, string folderName, string subFolder)
        {
            string delimiter = "/";
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    DeleteObjectRequest deleteFolderRequest = PopulateDeleteObjectRequest(bucketName, string.Concat(folderName, delimiter, subFolder, delimiter));
                    Task<DeleteObjectResponse> deleteObjectResponse = s3Client.DeleteObjectAsync(deleteFolderRequest);
                    deleteObjectResponse.Wait();
                    return deleteObjectResponse.Result;
                }
                catch (AmazonS3Exception e)
                {
                    throw new BucketException("Folder deletion has failed.", e);
                }
            }
        }
    }
}
