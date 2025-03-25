using NuetApiModelsSMG.Models;
using System.Net.Http.Headers;

namespace MvcCoreEmpleadosMultioplesRutas.Services
{
    public class ServiceEmpleados
    {
        private string ApiUrl;

        private MediaTypeWithQualityHeaderValue Header;

        public ServiceEmpleados(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiEmpleados");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "api/empleados";
            List<Empleado> data = await this.CallApiAsync<List<Empleado>>(request); 
            return data;
            
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            string request = "api/empleados/oficios";
            List<string> data = await this.CallApiAsync<List<string>>(request);
            return data;
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleados/empleadosoficio/" + oficio;
                List<Empleado> data = await this.CallApiAsync<List<Empleado>>(request);
                return data;

            }
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();  
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
    }
}
