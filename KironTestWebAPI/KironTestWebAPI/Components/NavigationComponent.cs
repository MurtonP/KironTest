using KironTestWebAPI.Models;
using KironTestWebAPI.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using DataLayer;

namespace KironTestWebAPI.Components
{
    public class NavigationComponent
    {
        private readonly IConfiguration _configuration;
        private readonly SqlDBConnectionFactory _connectionFactory;

        public NavigationComponent(IConfiguration configuration)
        {
            _configuration = configuration;

            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connectionFactory = new SqlDBConnectionFactory(connectionString);
        }

        public List<Navigations> GetAllNavigation()
        {
            var navigation = new List<Navigations>();

            var result = _connectionFactory.CallStoredProc("GetNavigationAll").Result;
            
            Navigations tmp = new Navigations() { Text = "" };
            foreach (var item in result)
            {
                tmp = new Navigations() { Text = "" };
                tmp.ID = item.ID;
                tmp.Text = item.Text;
                tmp.ParentID = item.ParentID;
                navigation.Add(tmp);
            }

            return navigation;
            //}
        }

        public async Task<List<Navigations>> GetNavigationByParentId(int param = 1)
        {
            List<Navigations> navigationById = new List<Navigations>();

            var storedProcedureName = "GetNavigationByParentId";
            var parameter = new Dictionary<string, object> { { "Param1", param} };
            var results = await _connectionFactory.CallStoredProc(storedProcedureName, parameter);

            foreach (var item in results)
            {
                navigationById.Add(new Navigations() { ID = item.ID, Text = item.Text, ParentID = item.ParentID });
            }

            return navigationById;
        }

        public string NavigationRecursive(List<Navigations> navigations)
        {
            List<NavigationRecursive> navigationsList = new List<NavigationRecursive>();
            var allNavigations = _connectionFactory.CallStoredProc("GetNavigationAll").Result;
            
            foreach (var item in allNavigations)
            {
                navigationsList.Add(new NavigationRecursive() { ID = item.ID, Text = item.Text, ParentID = item.ParentID });
            }
            var navDictionary = navigationsList.ToDictionary(c => c.ID);

            foreach (var navigation in navigationsList)
            {
                if (navigation.ParentID != -1 && navDictionary.TryGetValue(navigation.ParentID, out var parent))
                {
                    parent.Children.Add(navigation);
                }
            }

            var roots = navDictionary.Values.Where(n => n.ParentID == -1).ToList();

            var result = JsonSerializer.Serialize(roots.Select(MapToDTO), new JsonSerializerOptions { WriteIndented = true });

            return result;
        }

        NavigationRecursiveDTO MapToDTO(NavigationRecursive navRecursive)
        {
            var dto = new NavigationRecursiveDTO
            {
                Text = navRecursive.Text
            };

            if (navRecursive.Children.Any())
            {
                dto.Children = navRecursive.Children.Select(MapToDTO).ToList();
            }
            return dto;
        }
    }
}
