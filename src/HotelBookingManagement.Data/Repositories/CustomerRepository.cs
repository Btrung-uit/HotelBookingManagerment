using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Repositories;

/// <summary>
/// Cài đặt truy xuất dữ liệu cho bảng dbo.Customers bằng ADO.NET thuần
/// </summary>
public class CustomerRepository : ICustomerRepository
{
	public IEnumerable<Customer> GetAll()
	{
		var list = new List<Customer>();
		const string query = @"
			SELECT CustomerId, FullName, Phone, Email, IdentityNumber, Address, DateOfBirth, CreatedAt
			FROM dbo.Customers
			ORDER BY CustomerId ASC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			list.Add(MapReaderToCustomer(reader));
		}

		return list;
	}

	public Customer? GetById(int customerId)
	{
		const string query = @"
			SELECT CustomerId, FullName, Phone, Email, IdentityNumber, Address, DateOfBirth, CreatedAt
			FROM dbo.Customers
			WHERE CustomerId = @CustomerId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;

		connection.Open();
		using var reader = command.ExecuteReader();
		return reader.Read() ? MapReaderToCustomer(reader) : null;
	}

	public Customer? GetByPhone(string phone)
	{
		const string query = @"
			SELECT CustomerId, FullName, Phone, Email, IdentityNumber, Address, DateOfBirth, CreatedAt
			FROM dbo.Customers
			WHERE Phone = @Phone;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = phone;

		connection.Open();
		using var reader = command.ExecuteReader();
		return reader.Read() ? MapReaderToCustomer(reader) : null;
	}

	public Customer? GetByIdentityNumber(string identityNumber)
	{
		const string query = @"
			SELECT CustomerId, FullName, Phone, Email, IdentityNumber, Address, DateOfBirth, CreatedAt
			FROM dbo.Customers
			WHERE IdentityNumber = @IdentityNumber;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@IdentityNumber", SqlDbType.VarChar, 20).Value = identityNumber;

		connection.Open();
		using var reader = command.ExecuteReader();
		return reader.Read() ? MapReaderToCustomer(reader) : null;
	}

	public IEnumerable<Customer> Search(string keyword)
	{
		var list = new List<Customer>();
		const string query = @"
			SELECT CustomerId, FullName, Phone, Email, IdentityNumber, Address, DateOfBirth, CreatedAt
			FROM dbo.Customers
			WHERE FullName LIKE @Pattern
			   OR Phone LIKE @Pattern
			   OR IdentityNumber LIKE @Pattern
			ORDER BY CustomerId ASC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 100).Value = $"%{keyword.Trim()}%";

		connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			list.Add(MapReaderToCustomer(reader));
		}

		return list;
	}

	public int Add(Customer customer)
	{
		const string query = @"
			INSERT INTO dbo.Customers (FullName, Phone, Email, IdentityNumber, Address, DateOfBirth, CreatedAt)
			VALUES (@FullName, @Phone, @Email, @IdentityNumber, @Address, @DateOfBirth, @CreatedAt);
			SELECT CAST(SCOPE_IDENTITY() AS int);";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = customer.FullName.Trim();
		command.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = customer.Phone.Trim();
		command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object?)customer.Email?.Trim() ?? DBNull.Value;
		command.Parameters.Add("@IdentityNumber", SqlDbType.VarChar, 20).Value = customer.IdentityNumber.Trim();
		command.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = (object?)customer.Address?.Trim() ?? DBNull.Value;
		command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = (object?)customer.DateOfBirth ?? DBNull.Value;
		command.Parameters.Add("@CreatedAt", SqlDbType.DateTime2).Value = customer.CreatedAt;

		connection.Open();
		var result = command.ExecuteScalar();
		return result != null && result != DBNull.Value ? Convert.ToInt32(result) : -1;
	}

	public bool Update(Customer customer)
	{
		const string query = @"
			UPDATE dbo.Customers
			SET FullName = @FullName,
			    Phone = @Phone,
			    Email = @Email,
			    IdentityNumber = @IdentityNumber,
			    Address = @Address,
			    DateOfBirth = @DateOfBirth
			WHERE CustomerId = @CustomerId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customer.CustomerId;
		command.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = customer.FullName.Trim();
		command.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = customer.Phone.Trim();
		command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object?)customer.Email?.Trim() ?? DBNull.Value;
		command.Parameters.Add("@IdentityNumber", SqlDbType.VarChar, 20).Value = customer.IdentityNumber.Trim();
		command.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = (object?)customer.Address?.Trim() ?? DBNull.Value;
		command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = (object?)customer.DateOfBirth ?? DBNull.Value;

		connection.Open();
		return command.ExecuteNonQuery() > 0;
	}

	public bool Delete(int customerId)
	{
		const string query = @"DELETE FROM dbo.Customers WHERE CustomerId = @CustomerId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;

		connection.Open();
		return command.ExecuteNonQuery() > 0;
	}

	public bool ExistsByPhone(string phone, int? excludeCustomerId = null)
	{
		const string query = @"
			SELECT COUNT(1)
			FROM dbo.Customers
			WHERE Phone = @Phone
			  AND (@ExcludeId IS NULL OR CustomerId != @ExcludeId);";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = phone.Trim();
		command.Parameters.Add("@ExcludeId", SqlDbType.Int).Value = (object?)excludeCustomerId ?? DBNull.Value;

		connection.Open();
		var count = Convert.ToInt32(command.ExecuteScalar());
		return count > 0;
	}

	public bool ExistsByIdentityNumber(string identityNumber, int? excludeCustomerId = null)
	{
		const string query = @"
			SELECT COUNT(1)
			FROM dbo.Customers
			WHERE IdentityNumber = @IdentityNumber
			  AND (@ExcludeId IS NULL OR CustomerId != @ExcludeId);";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@IdentityNumber", SqlDbType.VarChar, 20).Value = identityNumber.Trim();
		command.Parameters.Add("@ExcludeId", SqlDbType.Int).Value = (object?)excludeCustomerId ?? DBNull.Value;

		connection.Open();
		var count = Convert.ToInt32(command.ExecuteScalar());
		return count > 0;
	}

	private static Customer MapReaderToCustomer(SqlDataReader reader)
	{
		return new Customer
		{
			CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
			FullName = reader.GetString(reader.GetOrdinal("FullName")),
			Phone = reader.GetString(reader.GetOrdinal("Phone")),
			Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
			IdentityNumber = reader.GetString(reader.GetOrdinal("IdentityNumber")),
			Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
			DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
			CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
		};
	}
}
