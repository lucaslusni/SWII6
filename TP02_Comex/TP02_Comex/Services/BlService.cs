using System.Collections.Generic;
using System.Data.SqlClient;
using TP02_Comex.Models;
using Microsoft.Extensions.Configuration;

public class BLService
{
    private readonly string _connectionString;

    public BLService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public List<Bl> ObterTodos()
    {
        var bls = new List<Bl>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM BL"; // Adapte para o nome correto da tabela
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bls.Add(new Bl
                    {
                        ID_BL = (int)reader["ID_BL"], // Altere para o nome correto da coluna
                        Numero = reader["Numero"].ToString(),
                        Consignee = reader["Consignee"].ToString(), // Verifique se a coluna existe
                        Navio = reader["Navio"].ToString()
                    });
                }
            }
        }
        return bls;
    }

    public void AdicionarBL(Bl bl)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO BL (Numero, Consignee, Navio) " + // Adapte a tabela e as colunas
                           "VALUES (@Numero, @Consignee, @Navio)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Numero", bl.Numero);
            command.Parameters.AddWithValue("@Consignee", bl.Consignee);
            command.Parameters.AddWithValue("@Navio", bl.Navio);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public Bl ObterPorId(int id)
    {
        Bl bl = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM BL WHERE ID_BL = @ID_BL"; // Adapte a consulta
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID_BL", id);
            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    bl = new Bl
                    {
                        ID_BL = (int)reader["ID_BL"],
                        Numero = reader["Numero"].ToString(),
                        Consignee = reader["Consignee"].ToString(),
                        Navio = reader["Navio"].ToString()
                    };
                }
            }
        }

        return bl;
    }

    public void AtualizarBL(Bl bl)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE BL SET Numero = @Numero, Consignee = @Consignee, Navio = @Navio " +
                           "WHERE ID_BL = @ID_BL";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID_BL", bl.ID_BL);
            command.Parameters.AddWithValue("@Numero", bl.Numero);
            command.Parameters.AddWithValue("@Consignee", bl.Consignee);
            command.Parameters.AddWithValue("@Navio", bl.Navio);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void ExcluirBL(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM BL WHERE ID_BL = @ID_BL"; // Adapte a consulta
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID_BL", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
