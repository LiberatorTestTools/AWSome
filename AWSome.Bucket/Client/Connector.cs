using Amazon.S3;
using Amazon.S3.Model;
using AWSome.Bucket.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AWSome.Bucket.Client
{
    /// <summary>
    /// Connector for to S3 buckets
    /// </summary>
    public class Connector
    {
        /// <summary>
        /// Lists the S3 buckets found on the client
        /// </summary>
        /// <returns>List of S3 bucket objects</returns>
        public List<S3Bucket> ListS3Buckets()
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    ListBucketsResponse response = s3Client.ListBuckets();
                    List<S3Bucket> buckets = response.Buckets;
                    foreach (S3Bucket bucket in buckets)
                    {
                        Console.WriteLine("Found bucket name {0} created at {1}", bucket.BucketName, bucket.CreationDate);
                    }
                    return buckets;
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket listing has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Creates a new bucket on the client
        /// </summary>
        /// <param name="newBucketName">The name of the new bucket to create</param>
        public void CreateS3Bucket(string newBucketName)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    PutBucketRequest putBucketRequest = new PutBucketRequest
                    {
                        BucketName = newBucketName
                    };

                    PutBucketResponse putBucketResponse = s3Client.PutBucket(putBucketRequest);
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket creation has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }
        }

        /// <summary>
        /// Creates a folder within a named bucket
        /// </summary>
        /// <param name="bucketName">The name of the bucket</param>
        /// <param name="folderName">The name of the new folder to create</param>
        public void CreateFolder(string bucketName, string folderName)
        {
            string delimiter = "/";
            string folderKey = string.Concat(folderName, delimiter);

            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    PutObjectRequest folderRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = folderKey,
                        InputStream = new MemoryStream(new byte[0])
                    };

                    PutObjectResponse folderResponse = s3Client.PutObject(folderRequest);
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Folder creation has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }
        }

        /// <summary>
        /// Checks if a folder exists in a named S3 bucket
        /// </summary>
        /// <param name="bucketName">The name of the bucket</param>
        /// <param name="folderPath">he nameof the folder to check for</param>
        /// <returns>True of the folder exists</returns>
        public bool CheckFolderExistsInS3Bucket(string bucketName, string folderPath)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    ListObjectsRequest findFolderRequest = new ListObjectsRequest
                    {
                        BucketName = bucketName,
                        Delimiter = "/",
                        Prefix = folderPath
                    };

                    ListObjectsResponse findFolderResponse = s3Client.ListObjects(findFolderRequest);
                    List<string> commonPrefixes = findFolderResponse.CommonPrefixes;
                    return commonPrefixes.Any();
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Folder existence check has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// Adds a file to an S3 bucket
        /// </summary>
        /// <param name="bucketName">The name of the bucket</param>
        /// <param name="filePath">The path to the file to upload</param>
        public void PutFileInS3Bucket(string bucketName, string filePath)
        {
            FileInfo filename = new FileInfo(filePath);
            string contents = File.ReadAllText(filename.FullName);

            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    PutObjectRequest putObjectRequest = new PutObjectRequest
                    {
                        ContentBody = contents,
                        BucketName = bucketName,
                        Key = filename.Name
                    };

                    putObjectRequest.Metadata.Add("type", "log");

                    PutObjectResponse putObjectResponse = s3Client.PutObject(putObjectRequest);
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("File creation has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
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
            ListObjectsRequest listObjectsRequest;

            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    listObjectsRequest = new ListObjectsRequest
                    {
                        BucketName = bucketName
                    };

                    ListObjectsResponse listObjectsResponse = s3Client.ListObjects(listObjectsRequest);

                    foreach (S3Object entry in listObjectsResponse.S3Objects)
                    {
                        Console.WriteLine("Found object with key {0}, size {1}, last modification date {2}", entry.Key, entry.Size, entry.LastModified);
                    }
                    return listObjectsResponse.S3Objects;
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Object listing has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lists the objects in a named folder
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the folder</param>
        /// <param name="pathToFolder">The path to the folder to enumerate</param>
        /// <returns>List of S3 bucket objects</returns>
        public List<S3Object> ListObjectsInS3Folder(string bucketName, string pathToFolder)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    ListObjectsRequest listObjectsRequest = new ListObjectsRequest
                    {
                        BucketName = bucketName,
                        Prefix = pathToFolder
                    };

                    ListObjectsResponse listObjectsResponse = s3Client.ListObjects(listObjectsRequest);

                    foreach (S3Object entry in listObjectsResponse.S3Objects)
                    {
                        if (entry.Size > 0)
                        {
                            Console.WriteLine("Found object with key {0}, size {1}, last modification date {2}", entry.Key, entry.Size, entry.LastModified);
                        }
                    }
                    return listObjectsResponse.S3Objects;
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Object listing has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                    return null;
                }

            }
        }

        /// <summary>
        /// Adds a local file to the S3 bucket
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the folder</param>
        /// <param name="folderName">The name of the folder to upload to</param>
        /// <param name="filePath">The local oath to the file being uploaded</param>
        public void AddFileToS3Folder(string bucketName, string folderName, string filePath)
        {
            string delimiter = "/";
            FileInfo filename = new FileInfo(filePath);
            string contents = File.ReadAllText(filename.FullName);

            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    PutObjectRequest putObjectRequest = new PutObjectRequest
                    {
                        ContentBody = contents,
                        BucketName = string.Concat(bucketName, delimiter, folderName),
                        Key = filename.Name
                    };
                    PutObjectResponse putObjectResponse = s3Client.PutObject(putObjectRequest);
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("File creation within folder has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }
        }

        /// <summary>
        /// Deletes an item from an S3 bucket
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the item</param>
        /// <param name="filePath">The path to the file to be deleted</param>
        public void DeleteItemFromS3Bucket(string bucketName, string filePath)
        {
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
                    {
                        BucketName = bucketName,
                        Key = filePath
                    };

                    DeleteObjectResponse deleteObjectResponse = s3Client.DeleteObject(deleteObjectRequest);
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Object deletion has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }
        }

        /// <summary>
        /// Deletes a subfolder from an S3 folder
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the folder</param>
        /// <param name="folderName">The path of the folder</param>
        /// <param name="subFolder">The name of the subfolder to delete</param>
        public void DeleteSubFolderFromS3Bucket(string bucketName, string folderName, string subFolder)
        {
            string delimiter = "/";
            using (IAmazonS3 s3Client = Preferences.GetAmazonS3Client())
            {
                try
                {
                    DeleteObjectRequest deleteFolderRequest = new DeleteObjectRequest
                    {
                        BucketName = bucketName,
                        Key = string.Concat(folderName, delimiter, subFolder, delimiter)
                    };
                    DeleteObjectResponse deleteObjectResponse = s3Client.DeleteObject(deleteFolderRequest);
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Folder deletion has failed.");
                    Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }
        }
    }
}
