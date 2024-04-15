using Application.Contracts.Algorithm.Search;
using Application.DTOs.Flight;

namespace Application.Services.Algorithm.Search
{
    public class DfsRouteSearch : IDfsRouteSearch
    {
        // Método para encontrar rutas utilizando DFS, incluyendo la opción de ida y vuelta
        public List<FlightDto> FindRoute(Dictionary<string, List<FlightDto>> graph, string origin, string destination)
        {
            var queue = new Queue<List<FlightDto>>();
            // Conjunto de aeropuertos visitados
            var visited = new HashSet<string>();

            // Iniciar la búsqueda con una lista de vuelos vacía desde el aeropuerto de origen
            queue.Enqueue(new List<FlightDto>());
            visited.Add(origin);

            while (queue.Count > 0)
            {
                // Obtener la ruta actual de la cola
                var currentRoute = queue.Dequeue();
                var currentNode = currentRoute.Count > 0 ? currentRoute[^1].Destination : origin;

                // Si hemos llegado al destino
                if (currentNode == destination)
                {
                    return currentRoute;
                }

                // Explorar los vuelos disponibles desde el aeropuerto actual
                if (graph.ContainsKey(currentNode))
                {
                    foreach (var flight in graph[currentNode])
                    {
                        if (!visited.Contains(flight.Destination))
                        {
                            // Marcar el destino como visitado
                            visited.Add(flight.Destination);
                            // Crear una nueva ruta con el vuelo actual
                            var newRoute = new List<FlightDto>(currentRoute) { flight };
                            // Encolar la nueva ruta
                            queue.Enqueue(newRoute);
                        }
                    }
                }
            }
            return null;
        }
    }
}
