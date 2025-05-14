using System.Data;
using DeviceManagementSystem.Database.Models;
using DeviceManagementSystem.Database.Repositories.RepositoryExceptions;
using DeviceManagementSystem.Objects.Devices;
using Microsoft.Data.SqlClient;

namespace DeviceManagementSystem.Database.Repositories;

public class MSSQLRepository : IRepository
{
    private string _connectionString;
    
    public MSSQLRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IEnumerable<DeviceDTO> GetAllDevices()
    {
        var devices = new List<DeviceDTO>();

        string query = "SELECT Id, Name, IsEnabled FROM Device";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var device = new DeviceDTO(reader.GetString(0),
                            reader.GetString(1), reader.GetBoolean(2));

                        devices.Add(device);
                    }
                }
            }
        }

        return devices;
    }
    
    /// <summary>
    /// Returns device by id from db
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return Device with matching id</returns>
    public Device? GetDeviceById(string id)
    {
        var baseDeviceData = new DeviceDTO(null, null, null);

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            
            var baseCmd = new SqlCommand("SELECT Id, Name, IsEnabled FROM Device WHERE Id = @Id", connection);
            baseCmd.Parameters.AddWithValue("@Id", id);
            
            using (SqlDataReader reader = baseCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        baseDeviceData.Id = reader.GetString(0);
                        baseDeviceData.Name = reader.GetString(1);
                        baseDeviceData.IsOn = reader.GetBoolean(2);
                    }
                }
            }
            
            var embeddedCmd = new SqlCommand(
                "SELECT IpAddress, NetworkName FROM Embedded WHERE DeviceId = @Id", connection);
            embeddedCmd.Parameters.AddWithValue("@Id", id);
            using (var reader = embeddedCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new EmbeddedDevice(baseDeviceData.Id,
                        baseDeviceData.Name,
                        baseDeviceData.IsOn ?? false, //fallback to turned off
                        reader.GetString(0),
                        reader.GetString(1));
                }
            }
            
            var pcCmd = new SqlCommand(
                "SELECT OperationSystem FROM PersonalComputer WHERE DeviceId = @Id", connection);
            pcCmd.Parameters.AddWithValue("@Id", id);
            using (var reader = pcCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new PersonalComputer(baseDeviceData.Id,
                        baseDeviceData.Name,
                        baseDeviceData.IsOn ?? false, //fallback to turned off
                        reader.GetString(0));
                }
            }
            
            var swCmd = new SqlCommand("SELECT BatteryPercentage FROM Smartwatch WHERE DeviceId = @Id", connection);
            swCmd.Parameters.AddWithValue("@Id", id);
            using (var reader = swCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Smartwatch(baseDeviceData.Id,
                        baseDeviceData.Name,
                        baseDeviceData.IsOn ?? false, //fallback to turned off
                        reader.GetInt32(0));
                }
            }

            throw new NotFoundException();
        }
    }

    public bool CreateDevice(Device device)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        
        try
        {
            var command = new SqlCommand
            {
                Connection = connection,
                Transaction = transaction,
                CommandType = CommandType.StoredProcedure
            };

            switch (device)
            {
                case EmbeddedDevice embedded:
                    command.CommandText = "AddEmbedded";
                    command.Parameters.Add(new SqlParameter("@DeviceId", embedded.Id));
                    command.Parameters.Add(new SqlParameter("@Name", embedded.Name));
                    command.Parameters.Add(new SqlParameter("@IsEnabled", embedded.IsOn));
                    command.Parameters.Add(new SqlParameter("@IpAddress", embedded.Ip));
                    command.Parameters.Add(new SqlParameter("@NetworkName", embedded.NetworkName));
                    break;

                case PersonalComputer pc:
                    command.CommandText = "AddPersonalComputer";
                    command.Parameters.Add(new SqlParameter("@DeviceId", pc.Id));
                    command.Parameters.Add(new SqlParameter("@Name", pc.Name));
                    command.Parameters.Add(new SqlParameter("@IsEnabled", pc.IsOn));
                    command.Parameters.Add(new SqlParameter("@OperationSystem", pc.OperatingSystem));
                    break;

                case Smartwatch sw:
                    command.CommandText = "AddSmartwatch";
                    command.Parameters.Add(new SqlParameter("@DeviceId", sw.Id));
                    command.Parameters.Add(new SqlParameter("@Name", sw.Name));
                    command.Parameters.Add(new SqlParameter("@IsEnabled", sw.IsOn));
                    command.Parameters.Add(new SqlParameter("@BatteryPercentage", sw.BatteryPercentage));
                    break;

                default:
                    throw new ArgumentException("Unsupported device type");
            }

            command.ExecuteNonQuery();
            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public bool UpdateDevice(Device updatedDevice)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        
        try
        {
            SqlCommand updateSpecificCommand;

            switch (updatedDevice)
            {
                case EmbeddedDevice embedded:
                    updateSpecificCommand = new SqlCommand(
                        @"UPDATE Embedded 
                        SET IpAddress = @IpAddress, NetworkName = @NetworkName 
                        WHERE DeviceId = @Id AND RowVer = @RowVer", connection, transaction);
                    updateSpecificCommand.Parameters.AddWithValue("@IpAddress", embedded.Ip);
                    updateSpecificCommand.Parameters.AddWithValue("@NetworkName", embedded.NetworkName);
                    break;

                case PersonalComputer pc: 
                    updateSpecificCommand = new SqlCommand(
                    @"UPDATE PersonalComputer 
                      SET OperationSystem = @OperationSystem 
                      WHERE DeviceId = @Id AND RowVer = @RowVer", connection, transaction);
                    updateSpecificCommand.Parameters.AddWithValue("@OperationSystem", pc.OperatingSystem);
                    break;

                case Smartwatch sw:
                    updateSpecificCommand = new SqlCommand(
                        @"UPDATE Smartwatch 
                         SET BatteryPercentage = @BatteryPercentage 
                         WHERE DeviceId = @Id AND RowVer = @RowVer", connection, transaction);
                    updateSpecificCommand.Parameters.AddWithValue("@BatteryPercentage", sw.BatteryPercentage);
                    break;

                default:
                    throw new ArgumentException("Unsupported device type");
            }

            updateSpecificCommand.Parameters.AddWithValue("@Id", updatedDevice.Id);
            updateSpecificCommand.Parameters.AddWithValue("@RowVer", updatedDevice.RowVersion);

            //Update Device table
            var deviceCmd = new SqlCommand(
                @"UPDATE Device 
                SET Name = @Name, IsEnabled = @IsEnabled 
                WHERE Id = @Id AND RowVer = @RowVer", connection, transaction);
            deviceCmd.Parameters.AddWithValue("@Name", updatedDevice.Name);
            deviceCmd.Parameters.AddWithValue("@IsEnabled", updatedDevice.IsOn);
            deviceCmd.Parameters.AddWithValue("@Id", updatedDevice.Id);
            deviceCmd.Parameters.AddWithValue("@RowVer", updatedDevice.RowVersion);
            
            int rowsAffectedDevice = deviceCmd.ExecuteNonQuery();
            int rowsAffectedSpecific = updateSpecificCommand.ExecuteNonQuery();

            if (rowsAffectedDevice == 0 || rowsAffectedSpecific == 0)
            {
                transaction.Rollback();
                return false; 
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    public bool DeleteDeviceById(string id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            string? tableToDeleteFrom = GetDeviceSubtypeTable(connection, transaction, id);

            if (tableToDeleteFrom == null)
                return false;

            //Specific table
            var deleteSpecificCmd = new SqlCommand(
                $"DELETE FROM {tableToDeleteFrom} WHERE DeviceId = @Id",
                connection, transaction);
            deleteSpecificCmd.Parameters.AddWithValue("@Id", id);
            deleteSpecificCmd.ExecuteNonQuery();

            //Device table
            var deleteDeviceCmd = new SqlCommand(
                "DELETE FROM Device WHERE Id = @Id",
                connection, transaction);
            deleteDeviceCmd.Parameters.AddWithValue("@Id", id);
            deleteDeviceCmd.ExecuteNonQuery();

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    private string? GetDeviceSubtypeTable(SqlConnection conn, SqlTransaction tx, string id)
    {
        var tables = new Dictionary<string, string>
        {
            { "Embedded", "SELECT COUNT(*) FROM Embedded WHERE DeviceId = @Id" },
            { "PersonalComputer", "SELECT COUNT(*) FROM PersonalComputer WHERE DeviceId = @Id" },
            { "Smartwatch", "SELECT COUNT(*) FROM Smartwatch WHERE DeviceId = @Id" }
        };

        foreach (var (tableName, query) in tables)
        {
            var cmd = new SqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@Id", id);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
                return tableName;
        }
        throw new NotFoundException();
    }
}