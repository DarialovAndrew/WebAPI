using NUnit.Framework;
using RestSharp;
using System;
using System.IO;

namespace WebAPITesting
{
    public class Tests
    {

        [Test]
        public void FileUploadTest()
        {
            var client = new RestClient("https://content.dropboxapi.com/2/files/upload");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer vwirrhfNo_wAAAAAAAAAAV8R5Yu_-tK7Qlf1w9sIcpKBXLwnLzSrODBsUpsksRRy");
            request.AddHeader("Dropbox-API-Arg", "{\"mode\":\"add\",\"autorename\":false,\"mute\":false,\"path\":\"/plane.jpg\"}");
            request.AddHeader("Content-Type", "application/octet-stream");

            byte[] data = File.ReadAllBytes("../../../plane.jpg");

            request.AddParameter("application/octet-stream", data, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(200, (int)response.StatusCode, "Couldn't upload file");


        }

        [Test]
        public void GetFileMetadataTest()
        {
            var client = new RestClient("https://api.dropboxapi.com/2/files/get_metadata");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer vwirrhfNo_wAAAAAAAAAAV8R5Yu_-tK7Qlf1w9sIcpKBXLwnLzSrODBsUpsksRRy");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"path\": \"/car.jpg\",\r\n    \"include_media_info\": false,\r\n    \"include_deleted\": false,\r\n    \"include_has_explicit_shared_members\": false\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(200, (int)response.StatusCode, "Couldn't read file metadata");
        }

        [Test]
        public void DeleteFileTest()
        {
            var uploadClient = new RestClient("https://content.dropboxapi.com/2/files/upload");
            uploadClient.Timeout = -1;
            var uploadRequest = new RestRequest(Method.POST);
            uploadRequest.AddHeader("Authorization", "Bearer vwirrhfNo_wAAAAAAAAAAV8R5Yu_-tK7Qlf1w9sIcpKBXLwnLzSrODBsUpsksRRy");
            uploadRequest.AddHeader("Dropbox-API-Arg", "{\"mode\":\"add\",\"autorename\":false,\"mute\":false,\"path\":\"/aircraft_carrier.jpg\"}");
            uploadRequest.AddHeader("Content-Type", "application/octet-stream");

            byte[] data = File.ReadAllBytes("../../../aircraft_carrier.jpg");

            uploadRequest.AddParameter("application/octet-stream", data, ParameterType.RequestBody);
            IRestResponse uploadResponse = uploadClient.Execute(uploadRequest);

            var client = new RestClient("https://api.dropboxapi.com/2/files/delete_v2");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer vwirrhfNo_wAAAAAAAAAAV8R5Yu_-tK7Qlf1w9sIcpKBXLwnLzSrODBsUpsksRRy");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"path\": \"/aircraft_carrier.jpg\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(200, (int)response.StatusCode, "Couldn't delete file");
        }
    }
}