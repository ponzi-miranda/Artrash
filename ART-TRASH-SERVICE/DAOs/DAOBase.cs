using WebImportaciones.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebImportaciones.DAOs
{
    public class DAOBase<T> where T : DTOBase, new()
    {
        public bool USA_ID_AUTOMATICO = true;

        public int Delete(int id)
        {
            return DeleteAll("Id=" + id);
        }

        public int DeleteAll(string where = "")
        {
            int affectedRows = 0;
            string tabla = GetTableName();

            string query = $"DELETE FROM [{tabla}]";
            if (!string.IsNullOrEmpty(where)) query += " WHERE " + where;

            using (SqlConnection conn = new SqlConnection(DAOHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    affectedRows = cmd.ExecuteNonQuery();
                }
            }

            return affectedRows;
        }

        public List<T> ReadAll(string where = "")
        {
            string tabla = GetTableName();
            List<T> lista = new List<T>();
            DataTable dt = new DataTable();

            string query = $"SELECT * FROM [{tabla}]";
            if (!string.IsNullOrEmpty(where)) query += " WHERE " + where;

            return ReadAllQuery(query);
        }

        public List<T> ReadAllQuery(string query)
        {
            List<T> lista = new List<T>();
            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter(query, DAOHelper.ConnectionString))
            {
                da.Fill(dt);
            }

            foreach (DataRow dr in dt.Rows)
            {
                T dto = new T();
                DAOHelper.FillObject(dr, dto);
                lista.Add(dto);
            }

            return lista;
        }
        public T Read(int id)
        {
            var list = ReadAll("Id=" + id);
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        public string GetTableName()
        {
            //El nombre de la tabla lo obtengo en base al nombre del DTO
            string tabla = typeof(T).Name;
            return tabla.Remove(tabla.Length - 3);
        }

        public void Update(T dto)
        {
            string tabla = GetTableName();

            using (SqlConnection conn = new SqlConnection(DAOHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    PropertyInfo[] props =
                        dto.GetType().GetProperties(
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                    string campos = string.Empty;
                    //string parametros = string.Empty;

                    foreach (PropertyInfo propiedad in props)
                    {
                        //Si usamos Id automático, se excluye del Query.
                        if (propiedad.Name == "Id")
                            continue;

                        campos += $"{propiedad.Name}=@{propiedad.Name},";

                        object valor = propiedad.GetValue(dto, null);

                        //Si el valor es "null", le asigno un DBNull para que SQL lo entienda.
                        if (valor == null) valor = DBNull.Value;

                        cmd.Parameters.AddWithValue("@" + propiedad.Name, valor);
                    }
                    campos = campos.TrimEnd(',');

                    string query = $"UPDATE [{tabla}] SET {campos} WHERE Id={dto.Id}";

                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    int affectedRows = cmd.ExecuteNonQuery();
                }
            }
        }

        public void Insert(T dto)
        {
            string tabla = GetTableName();

            using (SqlConnection conn = new SqlConnection(DAOHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    PropertyInfo[] props =
                        dto.GetType().GetProperties(
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                    string campos = string.Empty;
                    string parametros = string.Empty;

                    foreach (PropertyInfo propiedad in props)
                    {
                        //Si usamos Id automático, se excluye del Query.
                        if (USA_ID_AUTOMATICO && propiedad.Name == "Id")
                            continue;

                        campos += propiedad.Name + ',';
                        parametros += "@" + propiedad.Name + ',';

                        object valor;
                        valor = propiedad.GetValue(dto, null);

                        //Si el valor es "null", le asigno un DBNull para que SQL lo entienda.
                        if (valor == null)
                        {
                            valor = DBNull.Value;
                        }

                        cmd.Parameters.AddWithValue("@" + propiedad.Name, valor);
                    }

                    //No lo necesitamos porque es auto-generado.
                    if (!USA_ID_AUTOMATICO)
                        dto.Id = DAOHelper.ObtenerProximoId(tabla);

                    campos = campos.TrimEnd(',');
                    parametros = parametros.TrimEnd(',');

                    string query = $"INSERT INTO [{tabla}] ({campos}) VALUES ({parametros})";

                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    int affectedRows = cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDataTable(string query)
        {
            using (SqlConnection conn = new SqlConnection(DAOHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = conn;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

    }
}