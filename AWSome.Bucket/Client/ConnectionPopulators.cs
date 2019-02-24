using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Liberator.AWSome.Bucket.Client
{
    public partial class Connector
    {
        #region Private Object Population Methods

        /// <summary>
        /// Creates a CopyObjectRequest.
        /// </summary>
        /// <param name="sourceBucket">The name of the bucket containing the object to copy.</param>
        /// <param name="sourceKey">The key of the object to copy.</param>
        /// <param name="sourceVersionId">Specifies a particular version of the source object to copy. By default the latest version is copied.</param>
        /// <param name="destinationBucket">The name of the bucket to contain the copy of the source object.</param>
        /// <param name="destinationKey">The key to be given to the copy of the source object.</param>
        /// <param name="s3StorageClas">The type of storage to use for the object. Defaults to 'STANDARD'.</param>
        /// <param name="s3CannedACL">A canned access control list (CACL) to apply to the object.</param>
        /// <param name="s3Grants">Gets the access control lists (ACLs) for this request.</param>
        /// <param name="requestPayer">Confirms that the requester knows that she or he will be charged for the list objects request.
        /// Bucket owners need not specify this parameter in their requests.</param>
        /// <returns>A request object to be used when copying objects</returns>
        private CopyObjectRequest PopulateCopyObjectRequest(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey,
            [Optional]string sourceVersionId, [Optional] S3StorageClass s3StorageClas, [Optional]S3CannedACL s3CannedACL,
            [Optional]List<S3Grant> s3Grants, [Optional]RequestPayer requestPayer)
        {
            return new CopyObjectRequest
            {
                CannedACL = s3CannedACL ?? null,
                DestinationBucket = destinationBucket ?? null,
                DestinationKey = destinationKey ?? null,
                Grants = s3Grants ?? null,
                RequestPayer = requestPayer ?? null,
                SourceBucket = sourceBucket ?? null,
                SourceKey = sourceKey ?? null,
                SourceVersionId = sourceVersionId ?? null,
                StorageClass = s3StorageClas ?? null
            };
        }


        /// <summary>
        /// Creates a DeleteBucketRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="s3Region">The S3 region which contains the object</param>
        /// <returns>A request to be used when deleting buckets.</returns>
        private DeleteBucketRequest PopulateDeleteBucketRequest(string bucketName, S3Region s3Region)
        {
            return new DeleteBucketRequest
            {
                BucketName = bucketName ?? null,
                BucketRegion = s3Region ?? null
            };
        }


        /// <summary>
        /// Creates a DeleteObjectRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="objectKey">The key identifying the object to delete.</param>
        /// <returns>A request to be used when deleting objects.</returns>
        private DeleteObjectRequest PopulateDeleteObjectRequest(string bucketName, string objectKey)
        {
            return new DeleteObjectRequest
            {
                BucketName = bucketName ?? null,
                BypassGovernanceRetention = false,
                Key = objectKey ?? null,
                MfaCodes = null,
                RequestPayer = null,
                VersionId = null
            };
        }


        /// <summary>
        /// Creates a DeleteObjectsRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="keyVersions">The list of object keys to delete.</param>
        /// <param name="mfaCodes">Used with multi factor authentication devices.</param>
        /// <param name="requestPayer">Confirms that the requester knows that she or he will be charged for the list objects request.</param>
        /// <returns>A request to be used when deleting objects.</returns>
        private DeleteObjectsRequest PopulateDeleteObjectsRequest(string bucketName, List<KeyVersion> keyVersions,
            [Optional]MfaCodes mfaCodes, [Optional]RequestPayer requestPayer)
        {
            return new DeleteObjectsRequest
            {
                BucketName = bucketName ?? null,
                BypassGovernanceRetention = false,
                MfaCodes = mfaCodes ?? null,
                Objects = keyVersions ?? null,
                Quiet = false,
                RequestPayer = requestPayer ?? null
            };
        }


        /// <summary>
        /// Creates a GetObjectMetadataRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="key">The key of the object for which metadata is required.</param>
        /// <param name="versionId">The specific version of the file.</param>
        /// <param name="requestPayer">Confirms that the requester knows that she or he will be charged for the list objects request.</param>
        /// <returns>A request to be used  to get object metadata.</returns>
        private GetObjectMetadataRequest GetObjectMetadataRequest(string bucketName, string key, [Optional]string versionId, [Optional]RequestPayer requestPayer)
        {
            return new GetObjectMetadataRequest
            {
                BucketName = bucketName ?? null,
                Key = key ?? null,
                RequestPayer = requestPayer ?? null,
                VersionId = versionId ?? null
            };
        }


        /// <summary>
        /// Creates a GetObjectRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="key">The key of the object.</param>
        /// <param name="versionId">The specific version of the file.</param>
        /// <param name="requestPayer">Confirms that the requester knows that she or he will be charged for the list objects request.</param>
        /// <returns>A request to be used  to get objects.</returns>
        private GetObjectRequest GetObjectRequest(string bucketName, string key, [Optional]string versionId, [Optional]RequestPayer requestPayer)
        {
            return new GetObjectRequest
            {
                BucketName = bucketName ?? null,
                Key = key ?? null,
                RequestPayer = requestPayer ?? null,
                VersionId = versionId ?? null
            };
        }


        /// <summary>
        /// Creates a ListObjectsRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="delimiter">Character used to group keys.</param>
        /// <param name="prefix">Limits the response to keys that begin with the given prefix.</param>
        /// <param name="maxKeys">Sets the maximum number of keys returned in the response.</param>
        /// <param name="encoding">Asks amazon to encode the object keys.</param>
        /// <param name="requestPayer">Confirms that the requester knows that she or he will be charged for the list objects request.</param>
        /// <returns>A request to be used to list objects.</returns>
        private ListObjectsRequest PopulateListObjectsRequest(string bucketName, string delimiter,
            [Optional]string prefix, [Optional]int maxKeys, [Optional]EncodingType encoding, [Optional]RequestPayer requestPayer)
        {
            return new ListObjectsRequest
            {
                BucketName = bucketName ?? null,
                Delimiter = delimiter ?? null,
                Encoding = encoding ?? null,
                Marker = "",
                MaxKeys = maxKeys,
                Prefix = prefix ?? null,
                RequestPayer = requestPayer ?? null
            };
        }


        /// <summary>
        /// Creates a PutBucketRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="s3Region">The region locality for the bucket.</param>
        /// <param name="s3CannedACL">The canned ACL to apply to the bucket</param>
        /// <param name="s3Grants">Access control lists for this request.</param>
        /// <returns>A request to be used to create a new bucket.</returns>
        private PutBucketRequest PopulatePutBucketRequest(string bucketName, S3Region s3Region, [Optional]S3CannedACL s3CannedACL, [Optional]List<S3Grant> s3Grants)
        {
            return new PutBucketRequest
            {
                BucketName = bucketName ?? null,
                BucketRegion = s3Region ?? null,
                CannedACL = s3CannedACL ?? null,
                Grants = s3Grants ?? null
            };
        }


        /// <summary>
        /// Creates a PutObjectRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="filePath">The full path of the file to be uploaded.</param>
        /// <param name="contentBody">Text content to be uploaded.</param>
        /// <param name="key">Used to identify the object.</param>
        /// <param name="digest">An MD5 digest of the content.</param>
        /// <param name="s3CannedACL">The canned ACL to apply to the bucke.t</param>
        /// <param name="s3StorageClass">The type of storage to use for the object. Defaults to 'STANDARD'.</param>
        /// <param name="stream">Input stream to be used.</param>
        /// <param name="requestPayer">Confirms that the requester knows that she or he will be charged for the list objects request.</param>
        /// <returns>A request to be used to create objects.</returns>
        private PutObjectRequest PopulatePutObjectRequest(string bucketName, string filePath, string contentBody, string key, string digest,
            [Optional]S3CannedACL s3CannedACL, [Optional]S3StorageClass s3StorageClass, [Optional]Stream stream, [Optional]RequestPayer requestPayer)
        {
            return new PutObjectRequest
            {
                AutoCloseStream = true,
                AutoResetStreamPosition = true,
                BucketName = bucketName ?? null,
                CannedACL = s3CannedACL ?? null,
                ContentBody = contentBody ?? null,
                FilePath = filePath ?? null,
                Grants = null,
                InputStream = stream ?? null,
                Key = key ?? null,
                MD5Digest = digest ?? null,
                RequestPayer = requestPayer ?? null,
                StorageClass = s3StorageClass ?? null
            };
        }


        /// <summary>
        /// Creates a RestoreObjectRequest.
        /// </summary>
        /// <param name="bucketName">The name of the bucket containing the object.</param>
        /// <param name="description"></param>
        /// <param name="key">The key of the object for which restoration is required.</param>
        /// <param name="versionId">The specific version of the file.</param>
        /// <param name="requestPayer">Confirms that the requester knows that she or he will be charged for the list objects request.</param>
        /// <returns></returns>
        private RestoreObjectRequest RestoreObjectRequest(string bucketName, string key, [Optional]string versionId, 
            [Optional]string description, [Optional]RequestPayer requestPayer)
        {
            return new RestoreObjectRequest
            {
                BucketName = bucketName ?? null,
                Description = description ?? null,
                Key = key ?? null,
                RequestPayer = requestPayer ?? null,
                VersionId = versionId ?? null
            };
        }
        
        #endregion
    }
}
