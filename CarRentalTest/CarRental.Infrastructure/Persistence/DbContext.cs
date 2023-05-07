using System.Data;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CarRental.Infrastructure.Persistence;

/// <summary>
/// SQLite DB context
/// </summary>
public class DbContext
{
    private readonly IConfiguration _configuration;

    public DbContext(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Create DB connection
    /// </summary>
    /// <returns></returns>
    public IDbConnection CreateConnection()
       => new SQLiteConnection(GetDataBaseDirectory());

    
    /// <summary>
    /// Create DB and seed data for all tables
    /// </summary>
    public async Task InitDataBase()
    {
        try
        {
            using var connection = CreateConnection();

            await _CleanDb();
            await _InitCustomers();
            await _InitCars();
            await _InitBookings();
            await _InitLocations();
            await _InitSeeds();

        #region SQL Commands

        async Task _CleanDb()
        {
            var command = @"
                DROP TABLE IF EXISTS Customers;
                DROP TABLE IF EXISTS Cars;
                DROP TABLE IF EXISTS Bookings;
                DROP TABLE IF EXISTS Locations;
            ";
             
            await connection.ExecuteAsync(command);
        }

        async Task _InitCustomers()
        {
            var command = @"
                CREATE TABLE IF NOT EXISTS 
                Customers (
                    Id UUID BLOB NOT NULL PRIMARY KEY,
                    FirstName NVARCHAR(20) NOT NULL,
                    LastName NVARCHAR(20) NOT NULL,
                    Email TEXT UNIQUE NOT NULL,
                    Rating INTEGER NULL,
                    CreationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastUpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP
                );
            ";
            
            await connection.ExecuteAsync(command);
        }

        async Task _InitCars()
        {
            var command = @"
                CREATE TABLE IF NOT EXISTS 
                Cars (
                    Id UUID BLOB NOT NULL PRIMARY KEY,
                    CustomerEmail TEXT NULL,
                    LocationId UUID BLOB NULL,
                    Number NVARCHAR(10) NOT NULL,
                    Brand NVARCHAR(30) NOT NULL,
                    Model NVARCHAR(30) NOT NULL,
                    Color NVARCHAR(15) NULL,
                    Capacity INTEGER NULL,
                    CreationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastUpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (CustomerEmail)  REFERENCES Customers (Email),
                    FOREIGN KEY (LocationId)  REFERENCES Locations (Id)
                );                
            ";
            
            await connection.ExecuteAsync(command);
        }

        async Task _InitBookings()
        {
            var command = @"
                CREATE TABLE IF NOT EXISTS 
                Bookings (
                    Number INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Id UUID BLOB NOT NULL,                    
                    CustomerEmail TEXT NOT NULL,
                    CarId UUID BLOB NULL,
                    PickUpDate TEXT,
                    DropOffDate TEXT,
                    CreationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastUpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (CustomerEmail)  REFERENCES Customers (Email),
                    FOREIGN KEY (CarId)  REFERENCES Cars (Id)
                );
            ";
            
            await connection.ExecuteAsync(command);
        }
        
        async Task _InitLocations()
        {
            var command = @"
                CREATE TABLE IF NOT EXISTS 
                Locations (
                    Id UUID BLOB NOT NULL PRIMARY KEY,
                    State TEXT,
                    City TEXT,
                    Street TEXT,
                    Latitude NUMERIC,
                    Longitude NUMERIC,
                    CreationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastUpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP
                );
            ";
            
            await connection.ExecuteAsync(command);
        }

        async Task _InitSeeds()
        {
            var command = @"
                    INSERT INTO Customers(Id, FirstName, LastName, Email, Rating)
                    VALUES('8dac6367-a555-4b8c-8429-8c040c5b267a', 'Elijah', 'Wood', 'hairy_leg@gmail.com', 0),
                          ('cf9a83ee-6fcd-45d9-b5c2-73dab896b8c2', 'Ian', 'McKellen', 'gray_oldman@gmail.com', 1),
                          ('60fd345f-e733-46c4-93cf-baede6f02450', 'Christopher', 'Lee', 'white_oldman@gmail.com', 2),
                          ('5af59efd-29e1-4184-9aac-02f4e6800288', 'Orlando', 'Bloom', 'headshot@gmail.com', 0),
                          ('8200ac8d-cdfe-4142-b64a-e89459fb3fd3', 'Andy', 'Serkis', 'my_precious@gmail.com', 1);

                    INSERT INTO Cars(Id, CustomerEmail, LocationId, Number, Brand, Model, Color, Capacity)
                    VALUES('f38486aa-7dd8-45ab-b0d0-b07b78649e3b', 'white_oldman@gmail.com', NULL, 'G435RT', 'Honda', 'Accord', 'White', 4),
                          ('5fa94176-a56c-4386-959f-3fa70eb325cc', NULL, 'f1bdf22e-5306-4fee-afff-0ae17a7386c2', 'R789ZE', 'BMW', 'Z4 ', 'Silver', 2),
                          ('fa60c996-1303-4e0c-9111-76cedb586ed6', NULL, 'a8e1837f-2eb1-40d2-8193-fabca171863d', 'A123BC', 'Honda', 'Civic', 'Black', 5),
                          ('91e832a4-6fe2-43e7-9008-cabfa5d6d4e3', NULL, '76016255-66a7-4463-809f-a97d1dd9267f', 'A007BO', 'Aston Martin', 'DB Mark III', 'Dark green', 2),
                          ('3441272a-86b2-4adc-9d84-365b3ada391c', NULL, 'f44f0b2e-a70b-445f-a475-b39723b8f6ac', 'X000ZC', 'MG Motor', 'MG 3', 'Red', 5);

                    INSERT INTO Bookings(Id, CustomerEmail, CarId, PickUpDate, DropOffDate)
                    VALUES('09b58146-89dc-40a0-b64a-7f941a2c3667', 'white_oldman@gmail.com', '3441272a-86b2-4adc-9d84-365b3ada391c', '2023-08-01T10:01:45.790Z', '2023-08-02T15:00:45.790Z'),
                          ('19b58146-89dc-40a0-b64a-7f941a2c3667', 'hairy_leg@gmail.com', 'f38486aa-7dd8-45ab-b0d0-b07b78649e3b', '2021-02-01T14:12:59.790Z', '2021-02-02T16:00:45.790Z'),
                          ('28b58146-89dc-40a0-b64a-7f941a2c3667', 'white_oldman@gmail.com', '5fa94176-a56c-4386-959f-3fa70eb325cc', '2022-11-11T11:21:42.790Z', '2022-12-12T16:50:45.790Z'),
                          ('39b58146-89dc-40a0-b64a-7f941a2c3667', 'headshot@gmail.com', '91e832a4-6fe2-43e7-9008-cabfa5d6d4e3', '2024-03-02T10:31:45.790Z', '2024-03-02T16:00:45.790Z'),
                          ('49b58146-89dc-40a0-b64a-7f941a2c3667', 'gray_oldman@gmail.com', 'f38486aa-7dd8-45ab-b0d0-b07b78649e3b', '2022-07-05T12:51:55.790Z', '2022-07-10T15:00:45.790Z'),
                          ('59b58146-89dc-40a0-b64a-7f941a2c3667', 'headshot@gmail.com', '91e832a4-6fe2-43e7-9008-cabfa5d6d4e3', '2022-02-03T10:41:45.790Z', '2022-03-01T15:40:45.790Z'),
                          ('69b58146-89dc-40a0-b64a-7f941a2c3667', 'hairy_leg@gmail.com', 'f38486aa-7dd8-45ab-b0d0-b07b78649e3b', '2023-05-05T11:21:45.790Z', '2023-05-08T14:40:45.790Z'),
                          ('79b58146-89dc-40a0-b64a-7f941a2c3667', 'gray_oldman@gmail.com', '3441272a-86b2-4adc-9d84-365b3ada391c', '2023-06-06T16:11:45.790Z', '2023-06-10T16:59:45.790Z'),
                          ('89b58146-89dc-40a0-b64a-7f941a2c3667', 'white_oldman@gmail.com', 'f38486aa-7dd8-45ab-b0d0-b07b78649e3b', '2023-07-07T14:44:45.790Z', '2023-07-07T16:50:45.790Z'),
                          ('99b58146-89dc-40a0-b64a-7f941a2c3667', 'my_precious@gmail.com', '5fa94176-a56c-4386-959f-3fa70eb325cc', '2022-04-04T14:46:45.790Z', '2022-04-08T15:00:45.790Z'),
                          ('10b58146-89dc-40a0-b64a-7f941a2c3667', 'gray_oldman@gmail.com', 'fa60c996-1303-4e0c-9111-76cedb586ed6', '2022-05-05T15:55:55.790Z', '2022-06-09T11:00:55.790Z'),
                          ('11b58146-89dc-40a0-b64a-7f941a2c3667', 'my_precious@gmail.com', 'f38486aa-7dd8-45ab-b0d0-b07b78649e3b', '2023-06-07T12:22:45.790Z', '2023-06-09T16:34:25.790Z'),
                          ('12b58146-89dc-40a0-b64a-7f941a2c3667', 'white_oldman@gmail.com', 'fa60c996-1303-4e0c-9111-76cedb586ed6', '2023-09-09T13:33:45.790Z', '2023-09-10T11:20:45.790Z');


                    INSERT INTO Locations(Id, State, City, Street, Latitude, Longitude)
                    VALUES('f1bdf22e-5306-4fee-afff-0ae17a7386c2', 'Tāmaki-makau-rau', 'Auckland', 'Owens Rd', '-36.879160', '174.768039'),
                          ('a8e1837f-2eb1-40d2-8193-fabca171863d', 'Waikato', 'Hamilton', 'Norton Rd', '-37.783566', '175.260315'),
                          ('76016255-66a7-4463-809f-a97d1dd9267f', 'Canterbury', 'Christchurch', 'Blenheim Rd', '-43.537418','172.599129'),
                          ('f44f0b2e-a70b-445f-a475-b39723b8f6ac', 'Otago', 'Dunedin', 'Melville Street', '-45.884517', '170.497143');
            ";

            await connection.ExecuteAsync(command);
        }

        #endregion
        }
        catch (Exception ex)
        {
            ////TODO: CREATE SPECIFIC EXCEPTION FOR THIS EVENT
            Console.WriteLine(ex);
        }
    }

    private string GetDataBaseDirectory()
    {
        var assesmblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        var rootDirectory = Directory
            .GetParent(Path.GetDirectoryName(assesmblyPath))
            .Parent
            .Parent
            .Parent
            .FullName;
        
        var projectsDirectories = Directory.GetDirectories(rootDirectory)
            .FirstOrDefault(x=>x.Contains("Infrastructure"));
        
        var dbDirectory = Directory.GetDirectories(
                projectsDirectories, "*.*", SearchOption.AllDirectories)
            .FirstOrDefault(x=>x.Contains("DataBase")) + "\\";

        var dbLocation = _configuration.GetConnectionString("CarRentalDBConnection")
            .Replace("@", dbDirectory);

        return dbLocation;
    }
}