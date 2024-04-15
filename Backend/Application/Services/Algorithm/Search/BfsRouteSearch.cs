using Application.Contracts.Algorithm.Search;
using Application.DTOs.Flight;

namespace Application.Services.Algorithm.Search
{
    public class BfsRouteSearch : IBfsRouteSearch
    {
        // Method to find routes using BFS, including round trip option
        public List<FlightDto> FindRoute(Dictionary<string, List<FlightDto>> graph, string origin, string destination)
        {
            var queue = new Queue<List<FlightDto>>();
            // Set of visited airports
            var visited = new HashSet<string>();

            // Start the search with an empty list of flights from the origin airport
            queue.Enqueue(new List<FlightDto>());
            visited.Add(origin);

            while (queue.Count > 0)
            {
                // Get the current route from the queue
                var currentRoute = queue.Dequeue();
                var currentNode = currentRoute.Count > 0 ? currentRoute[^1].Destination : origin;

                // If we have reached the destination
                if (currentNode == destination)
                {
                    return currentRoute;
                }

                // Explore available flights from the current airport
                if (graph.ContainsKey(currentNode))
                {
                    foreach (var flight in graph[currentNode])
                    {
                        if (!visited.Contains(flight.Destination))
                        {
                            // Mark the destination as visited
                            visited.Add(flight.Destination);
                            // Create a new route with the current flight
                            var newRoute = new List<FlightDto>(currentRoute) { flight };
                            // Enqueue the new route
                            queue.Enqueue(newRoute);
                        }
                    }
                }
            }
            return null;
        }
    }
}
