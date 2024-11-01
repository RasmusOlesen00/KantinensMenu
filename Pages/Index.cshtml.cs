using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Azure.Data.Tables;
using IBAS_kantine.Model;
using System.Collections.Generic;
using Azure.Storage.Blobs;

namespace IBAS_kantine.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<MenuItemDTO> MenuItems { get; set; } 

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            MenuItems = new List<MenuItemDTO>(); 
        }

        public void OnGet()
        {
            // get Blob connection string
            var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGEBLOB_CONNECTIONSTRING");

            // Create a BlobServiceClient object 
            var blobServiceClient = new BlobServiceClient(connectionString);
            var tableName = "Menu2";
           
            TableClient tableClient = new TableClient(connectionString, tableName);

            Pageable<TableEntity> entities = tableClient.Query<TableEntity>();

            foreach (TableEntity entity in entities)
            {
                var dto = new MenuItemDTO
                {
                    Dag = entity.RowKey,
                    KoldRet = entity.GetString("KoldRet"),
                    VarmRet = entity.GetString("VarmRet"),
                    Uge = Int32.Parse(entity.PartitionKey)
                };
                MenuItems.Add(dto); 
            }
        }
    }
}