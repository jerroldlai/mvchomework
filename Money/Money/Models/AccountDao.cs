using Money.Views.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Money.Models
{
    public class AccountDao
    {
        private string ConnectionString { get; set; }

        public AccountDao()
        {
            this.ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        /// <summary>
        /// Inserts the specified f name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public int Insert(MoneyViewModel instance)
        {
            const string sqlStatement = "Insert Into AccountBook(Id,Categoryyy,Amounttt,Dateee,Remarkkk)Values(@Id,@Categoryyy,@Amounttt,@Dateee,@Remarkkk)";
           
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var command = new SqlCommand(sqlStatement, conn))
            {
                command.Parameters.Add(new SqlParameter("Id", System.Guid.NewGuid().ToString()));
                command.Parameters.Add(new SqlParameter("Categoryyy", instance.categoryyy));
                command.Parameters.Add(new SqlParameter("Amounttt", instance.Amounttt));
                command.Parameters.Add(new SqlParameter("Dateee", instance.dateee));
                command.Parameters.Add(new SqlParameter("Remarkkk", instance.remarkkk));

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //TODO 增加LOG
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates the specified f name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public int Update(MoneyViewModel instance)
        {
            const string sqlStatement = "Update AccountBook Set Categoryyy=@Categoryyy, Amounttt=@Amounttt, Dateee=@Dateee, Remarkkk=@Remarkkk Where Id = @Id ";

            using (var conn = new SqlConnection(this.ConnectionString))
            using (var command = new SqlCommand(sqlStatement, conn))
            {
                command.Parameters.Add(new SqlParameter("Id", instance.id));
                command.Parameters.Add(new SqlParameter("Categoryyy", instance.categoryyy));
                command.Parameters.Add(new SqlParameter("Amounttt", instance.Amounttt));
                command.Parameters.Add(new SqlParameter("Dateee", instance.dateee));
                command.Parameters.Add(new SqlParameter("Remarkkk", instance.remarkkk));

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //TODO 增加LOG
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public int Delete(string id)
        {
            const string sqlStatement = "Delete From AccountBook Where Id = @Id  ";

            using (var conn = new SqlConnection(this.ConnectionString))
            using (var command = new SqlCommand(sqlStatement, conn))
            {
                command.Parameters.Add(new SqlParameter("Id", id));

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    //TODO 增加LOG
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets all authors.
        /// </summary>
        /// <returns></returns>
        public List<MoneyViewModel> GetAllLists()
        {
            var result = new List<MoneyViewModel>();

            const string sqlStatement = "Select Id,Categoryyy,Amounttt,Dateee,Remarkkk From AccountBook ";

            using (var conn = new SqlConnection(this.ConnectionString))
            using (var command = new SqlCommand(sqlStatement, conn))
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new MoneyViewModel
                        {
                            id = reader["Id"].ToString(),
                            Amounttt = reader["Amounttt"].ToString(),
                            categoryyy = reader["Categoryyy"].ToString(),
                            dateee=Convert.ToDateTime(Convert.ToDateTime(reader["Dateee"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")),
                            remarkkk = reader["Remarkkk"].ToString(),
                        };
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public MoneyViewModel GetSingleRow(string id)
        {
            var result = new MoneyViewModel();

            const string sqlStatement = "Select Id,Categoryyy,Amounttt,Dateee,Remarkkk From AccountBook Where Id = @Id";

            using (var conn = new SqlConnection(this.ConnectionString))
            using (var comm = new SqlCommand(sqlStatement, conn))
            {
                comm.Parameters.Add(new SqlParameter("Id", id));

                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                using (IDataReader reader = comm.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.id= reader["Id"].ToString();
                        result.Amounttt = reader["Amounttt"].ToString();
                        result.categoryyy = reader["Categoryyy"].ToString();
                        result.dateee = Convert.ToDateTime(Convert.ToDateTime(reader["Dateee"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                        result.remarkkk = reader["Remarkkk"].ToString();
                    }
                }
            }

            return result;
        }
    }
}