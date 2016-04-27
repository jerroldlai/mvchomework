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
                command.Parameters.Add(new SqlParameter("Categoryyy", instance.category));
                command.Parameters.Add(new SqlParameter("Amounttt", instance.Money));
                command.Parameters.Add(new SqlParameter("Dateee", instance.Date));
                command.Parameters.Add(new SqlParameter("Remarkkk", instance.Remark));

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
                command.Parameters.Add(new SqlParameter("Categoryyy", instance.category));
                command.Parameters.Add(new SqlParameter("Amounttt", instance.Money));
                command.Parameters.Add(new SqlParameter("Dateee", instance.Date));
                command.Parameters.Add(new SqlParameter("Remarkkk", instance.Remark));

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
                            Money = reader["Amounttt"].ToString(),
                            category = Category_Name(reader["Categoryyy"].ToString()),
                            Date = Convert.ToDateTime(reader["Dateee"]),
                            Remark = reader["Remarkkk"].ToString(),
                        };
                        result.Add(item);
                    }
                }
            }
            return result;
        }
        private string Category_Name(string c_id)
        {
            if (c_id == "0")
                return "收入";
            else if (c_id == "1")
                return "支出";
            else
                return "N/A";
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
                        result.Money = reader["Amounttt"].ToString();
                        result.category = reader["Categoryyy"].ToString();
                        result.Date = Convert.ToDateTime(Convert.ToDateTime(reader["Dateee"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                        result.Remark = reader["Remarkkk"].ToString();
                    }
                }
            }

            return result;
        }
    }
}