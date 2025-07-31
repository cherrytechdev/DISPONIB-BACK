 
using System;
using System.Data;
using System.Data.SqlClient;

namespace ESInfrastructure.Database
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string connectionString;
        private SqlConnection connection;
        private bool disposedValue = false;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public SqlConnection GetOpenConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }

            return connection;
        }

        public void Dispose()
        {

            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {

                if (disposing && connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                this.disposedValue = true;
            } 
        }
    }
}
