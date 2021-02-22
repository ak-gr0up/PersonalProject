using System;
using System.Collections.Generic;
using RestSharp;
using IO.Swagger.Client;
using MedicalCommon;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IParticipantApi
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Participant</returns>
        Participant ParticipantDeleteParticipant (string id);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Participant</returns>
        Participant ParticipantGetParticipant (string id);
        /// <summary>
        ///  
        /// </summary>
        /// <returns>List&lt;Participant&gt;</returns>
        List<Participant> ParticipantGetParticipantAll ();
        /// <summary>
        ///  
        /// </summary>
        /// <param name="Participant"></param>
        /// <returns>Participant</returns>
        Participant ParticipantPostParticipant (Participant Participant);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Participant"></param>
        /// <returns>System.IO.Stream</returns>
        System.IO.Stream ParticipantPutParticipant (string id, Participant Participant);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ParticipantApi : IParticipantApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ParticipantApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ParticipantApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>Participant</returns>            
        public Participant ParticipantDeleteParticipant (string id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling ParticipantDeleteParticipant");
            
    
            var path = "/api/Participant/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantDeleteParticipant: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantDeleteParticipant: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Participant) ApiClient.Deserialize(response.Content, typeof(Participant), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>Participant</returns>            
        public Participant ParticipantGetParticipant (string id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling ParticipantGetParticipant");
            
    
            var path = "/api/Participant/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantGetParticipant: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantGetParticipant: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Participant) ApiClient.Deserialize(response.Content, typeof(Participant), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <returns>List&lt;Participant&gt;</returns>            
        public List<Participant> ParticipantGetParticipantAll ()
        {
            var path = "/api/Participant";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantGetParticipantAll: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantGetParticipantAll: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Participant>) ApiClient.Deserialize(response.Content, typeof(List<Participant>), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="Participant"></param> 
        /// <returns>Participant</returns>            
        public Participant ParticipantPostParticipant (Participant Participant)
        {
            
            // verify the required parameter 'Participant' is set
            if (Participant == null) throw new ApiException(400, "Missing required parameter 'Participant' when calling ParticipantPostParticipant");
            
    
            var path = "/api/Participant";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(Participant); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantPostParticipant: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantPostParticipant: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Participant) ApiClient.Deserialize(response.Content, typeof(Participant), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="Participant"></param> 
        /// <returns>System.IO.Stream</returns>            
        public System.IO.Stream ParticipantPutParticipant (string id, Participant Participant)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling ParticipantPutParticipant");
            
            // verify the required parameter 'Participant' is set
            if (Participant == null) throw new ApiException(400, "Missing required parameter 'Participant' when calling ParticipantPutParticipant");
            
    
            var path = "/api/Participant/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(Participant); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantPutParticipant: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ParticipantPutParticipant: " + response.ErrorMessage, response.ErrorMessage);
    
            return (System.IO.Stream) ApiClient.Deserialize(response.Content, typeof(System.IO.Stream), response.Headers);
        }
    
    }
}
