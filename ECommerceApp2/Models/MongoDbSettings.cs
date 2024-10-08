/*
 * File Name: MongoDbSettings.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Model representing the settings for MongoDB database connection.
 *              Implements the IMongoDbSettings interface and contains properties for 
 *              the connection string and database name.
 *              This model is used to configure the MongoDB database settings in 
 *              the e-commerce application.
 */
namespace ECommerceApp2.Models
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
