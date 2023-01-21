namespace Taxually.TechnicalTest
{
    public class TaxuallyHttpClient
    {
        private readonly HttpClient _httpClient = default!;

        public string BaseURL
        {
            get { return this._httpClient.BaseAddress?.ToString() ?? ""; }
            set { this._httpClient.BaseAddress = new Uri(value); }
        }

        //For Unit Testing
        public TaxuallyHttpClient() { }

        public TaxuallyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task PostAsync<TRequest>(TRequest request)
        {
            // Actual HTTP call removed for purposes of this exercise
            
            //Use _httpClient.PostAsync          

            return Task.CompletedTask;
        }
    }
}
