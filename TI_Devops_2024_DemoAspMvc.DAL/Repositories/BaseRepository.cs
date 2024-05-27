using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_Devops_2024_DemoAspMvc.DAL.Interfaces;

namespace TI_Devops_2024_DemoAspMvc.DAL.Repositories
{
    public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class
    {
        private readonly string _tableName;
        private readonly string _columnIdName;
        protected readonly string _connectionString;

        protected BaseRepository(string tableName, string columnIdName)
        {
            _tableName = tableName;
            _columnIdName = columnIdName;
            _connectionString = @"server=(localdb)\MSSQLLocaldb;database=Devops_BookDB;integrated security=true";
        }

        protected abstract TEntity Convert(IDataRecord r);

        public IEnumerable<TEntity> GetAll()
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $@"SELECT * 
                                 FROM {_tableName}";

            conn.Open();

            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                yield return Convert(r);
            }

            conn.Close();
        }

        public TEntity? GetById(TId id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"SELECT * 
                                 FROM {_tableName} 
                                 WHERE {_columnIdName} = @id";

            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();

            IDataReader r = cmd.ExecuteReader();

            TEntity? e = null;

            if (r.Read())
            {
                e = Convert(r);
            }

            conn.Close();

            return e;
        }

        public int Count()
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*) 
                                 FROM {_tableName}";

            conn.Open();

            int count = (int)cmd.ExecuteScalar();

            conn.Close();

            return count;
        }

        public abstract TId Create(TEntity e);

        public abstract bool Update(TId id, TEntity e);

        public bool Delete(TId id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"DELETE {_tableName} 
                                 WHERE {_columnIdName} = @id";

            cmd.Parameters.AddWithValue("@id",id);

            conn.Open();

            int nbRows = cmd.ExecuteNonQuery();

            conn.Close();

            return nbRows == 1;
        }
    }
}
