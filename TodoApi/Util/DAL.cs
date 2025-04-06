using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TodoApi.Util
{
    public class DAL : IDisposable
    {
        private static string Server = "localhost";
        private static string DataBase = "DBCliente";
        private static string User = "root";
        private static string Password = "";
        private MySqlConnection Connection;

        private string ConnectionString = $"Server={Server};Database={DataBase};Uid={User};Pwd={Password};SslMode=None;charset=utf8;";

        public DAL()
        {
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();

            if (Connection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Falha ao abrir conexão com o banco de dados.");
            }
        }


        public void VerificarBanco()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT DATABASE();", Connection);
            string db = (string)cmd.ExecuteScalar();
            Console.WriteLine($"Banco conectado: {db}");
        }


        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }

        /// <summary>
        /// Executa comandos INSERT, UPDATE, DELETE
        /// </summary>
        /// <param name="sql"></param>
        public void ExecutarComandoSQL(string sql)
        {
            MySqlCommand Command = new MySqlCommand(sql, Connection);
            Command.ExecuteNonQuery();
        }


        public DataTable RetornarDataTable(string sql)
        {
            MySqlCommand Command = new MySqlCommand( sql, Connection);
            MySqlDataAdapter DataAdapter = new MySqlDataAdapter(Command);
            DataTable Dados = new DataTable();
            DataAdapter.Fill(Dados); 
            return Dados;
        }

        public void ExecutarComandoSQL(string sql, Dictionary<string, object> parametros)
        {
            using (var comando = new MySqlCommand(sql, Connection))
            {
                foreach (var param in parametros)
                {
                    comando.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                comando.ExecuteNonQuery();
            }
        }


    }
}
