using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Data.SqlClient;

namespace WebImportaciones.DAOs
{
    public class DAOHelper
    {
        public static string ConnectionString { get; set; }

        public static int ObtenerProximoId(string tabla)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT IsNull(MAX(Id),0) + 1 AS Id FROM [{tabla}]";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        internal static void FillObject(DataRow dr, object obj)
        {
            //Obtengo el Tipo/Clase del objeto.
            int i;
            Type Objeto = obj.GetType();
            PropertyInfo[] myPropertyInfo = Objeto.GetProperties((BindingFlags.Public | BindingFlags.Instance));

            for (i = 0; i < myPropertyInfo.Length; i++)
            {
                PropertyInfo myPropInfo = ((PropertyInfo)myPropertyInfo[i]);

                //Solo voy a tener en cuenta las propiedades que no sean ReadOnly.
                if (myPropInfo.CanWrite)
                {
                    if (dr[myPropInfo.Name] is float && (myPropInfo.PropertyType == typeof(decimal) || myPropInfo.PropertyType == typeof(decimal?)))
                    {
                        //Seteo el valor de la propiedad SOLO si no es nulo.
                        if (dr[myPropInfo.Name] != System.DBNull.Value)
                            myPropInfo.SetValue(obj, Convert.ToDecimal(dr[myPropInfo.Name]), null);
                    }
                    else if (dr[myPropInfo.Name] is TimeSpan && (myPropInfo.PropertyType == typeof(DateTime) || myPropInfo.PropertyType == typeof(DateTime?)))
                    {
                        if (dr[myPropInfo.Name] != System.DBNull.Value)
                            myPropInfo.SetValue(obj, (new DateTime().Add((TimeSpan)dr[myPropInfo.Name])), null);
                    }
                    else
                    {
                        //Seteo el valor de la propiedad SOLO si no es nulo.
                        if (dr[myPropInfo.Name] != System.DBNull.Value)
                            myPropInfo.SetValue(obj, dr[myPropInfo.Name], null);
                    }
                }
            }
        }

        public static System.Data.SqlDbType GetSqlType(Type type)
        {
            if (type == typeof(bool) || type == typeof(bool?))
            {
                return SqlDbType.Bit;
            }
            else if (type == typeof(string))
            {
                return SqlDbType.NVarChar;
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                return SqlDbType.Int;
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                return SqlDbType.BigInt;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return SqlDbType.DateTime;
            }
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
            {
                return SqlDbType.Time;
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                return SqlDbType.Float;
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return SqlDbType.Decimal;
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                return SqlDbType.Real;
            }
            else if (type == typeof(byte[]))
            {
                return SqlDbType.Image;
            }
            else throw new Exception("DAOHelper: Tipo Desconocido.");
        }

        public static DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable("DT");

            using (SqlDataAdapter da = new SqlDataAdapter(query, DAOHelper.ConnectionString))
            {
                da.Fill(dt);
            }

            return dt;
        }
    }
}