using System.Text;
using Newtonsoft.Json;

namespace Proyecto.API.Consumer
{
    public static class Crud<T>
    {
        public static string EndPoint { get; set; }

        public static List<T> GetAll()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(EndPoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }

        public static T GetById(int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{EndPoint}/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static List<T> GetBy(String GestionFlotas, int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{EndPoint}/{GestionFlotas}/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }

            }
        }

        public static T Create(T item)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(
                        EndPoint,
                        new StringContent(
                            JsonConvert.SerializeObject(item),
                            Encoding.UTF8,
                            "application/json"
                        )
                    ).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }

        public static bool Update(int id, T item)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(
                        $"{EndPoint}/{id}",
                        new StringContent(
                            JsonConvert.SerializeObject(item),
                            Encoding.UTF8,
                            "application/json"
                        )
                    ).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }

        public static bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync($"{EndPoint}/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static bool Loggin(T item)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(
                    $"{EndPoint}/Login",
                    new StringContent(
                        JsonConvert.SerializeObject(item),
                        Encoding.UTF8,
                        "application/json"
                    )
                ).Result;

                return response.IsSuccessStatusCode;
            }
        }
        // Método para generar alertas predictivas (Llamada a la API)
        public static List<T> AlertasPredictivas()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync($"{EndPoint}/GenerarMantenimientoPredictivo").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;

                        if (!string.IsNullOrWhiteSpace(json))
                        {

                            return JsonConvert.DeserializeObject<List<T>>(json);
                        }
                        else
                        {

                            return new List<T>();
                        }
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error en la llamada a la API: {ex.Message}");
                return new List<T>();
            }
        }
        public static async Task<List<T>> GetMantenimientosConFiltros(int? tallerId, string? searchTerm,Boolean? pendiente)
        {
            using (var client = new HttpClient())
            {
                var url = $"{EndPoint}/Filtrar"; // URL de la API con el endpoint correspondiente


                // Construir los parámetros de consulta (query parameters)
                var queryString = $"?tallerId={tallerId}&searchTerm={searchTerm}&Pendiente={pendiente}";

                var response = await client.GetAsync(url + queryString); // Hacemos la llamada GET con los parámetros

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(json); // Devolver los resultados deserializados
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static async Task<List<T>> GetTalleresConFiltros(int? tallerId, string? searchTerm, String? Ciudad)
        {
            using (var client = new HttpClient())
            {
                var url = $"{EndPoint}/Filtrar"; // URL de la API con el endpoint correspondiente


                // Construir los parámetros de consulta (query parameters)
                var queryString = $"?tallerId={tallerId}&searchTerm={searchTerm}&Ciudad={Ciudad}";

                var response = await client.GetAsync(url + queryString); // Hacemos la llamada GET con los parámetros

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(json); // Devolver los resultados deserializados
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static async Task<List<T>> GetCamionesConFiltros(string? searchTerm, string? Placa)
        {
            using (var client = new HttpClient())
            {
                var url = $"{EndPoint}/Filtrar"; // URL de la API con el endpoint correspondiente


                // Construir los parámetros de consulta (query parameters)
                var queryString = $"?&searchTerm={searchTerm}&Placa={Placa}";

                var response = await client.GetAsync(url + queryString); // Hacemos la llamada GET con los parámetros

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(json); // Devolver los resultados deserializados
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static async Task<List<T>> GetConductoresConFiltros(string? searchTerm, string? Licencia)
        {
            using (var client = new HttpClient())
            {
                var url = $"{EndPoint}/Filtrar"; // URL de la API con el endpoint correspondiente


                // Construir los parámetros de consulta (query parameters)
                var queryString = $"?&searchTerm={searchTerm}&Licencia={Licencia}";

                var response = await client.GetAsync(url + queryString); // Hacemos la llamada GET con los parámetros

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(json); // Devolver los resultados deserializados
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static async Task<List<T>> GetUsuariosConFiltros( string? searchTerm)
        {
            using (var client = new HttpClient())
            {
                var url = $"{EndPoint}/Filtrar"; // URL de la API con el endpoint correspondiente


                // Construir los parámetros de consulta (query parameters)
                var queryString = $"?searchTerm={searchTerm}&";

                var response = await client.GetAsync(url + queryString); // Hacemos la llamada GET con los parámetros

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(json); // Devolver los resultados deserializados
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static async Task<bool> SimularSensoresAutomáticamente()
        {
            using (var client = new HttpClient())
            {
                var url = $"{EndPoint}/SimularSensoresAutomáticamente";

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error al simular sensores: {response.StatusCode}");
                }
            }
        }
        public static async Task<bool> CancelarSimulacion()
        {
            using (var client = new HttpClient())
            {
                var url = $"{EndPoint}/DetenerSimulacion";

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error al simular sensores: {response.StatusCode}");
                }
            }
        }

    }
}
