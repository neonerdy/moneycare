
using System;
using System.Collections.Generic;
using MoneyCare.Model;
using MoneyCare.Repository.Mapping;
using System.Data.SqlServerCe;
using System.Data;

namespace MoneyCare.Repository
{
    public class CategoryRepository
    {
        private string tableName = "Categories";

        public Category GetById(Guid id)
        {
            Category category = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE ID='" + id + "'";
                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    category = Mapper.MapObject<Category>(rdr, new CategoryMapper());
                }
            }

            return category;
        }


        public Category QueryByName(string name)
        {
            Category category = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE Name LIKE '%" + name + "%'";

                var cmd = new SqlCeCommand(sql, conn);

                using (var rdr = cmd.ExecuteReader())
                {
                    category = Mapper.MapObject<Category>(rdr, new CategoryMapper());
                }
            }

            return category;
        }


        public Category GetByName(string name)
        {
            Category category = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE Name=@Name";
                
                var cmd = new SqlCeCommand(sql, conn);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;

                using (var rdr = cmd.ExecuteReader())
                {
                    category = Mapper.MapObject<Category>(rdr, new CategoryMapper());
                }
            }

            return category;
        }


        public Category GetByNameAndType(string name,string type)
        {
            Category category = null;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE Name=@Name AND Type=@Type";

                var cmd = new SqlCeCommand(sql, conn);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = type;

                using (var rdr = cmd.ExecuteReader())
                {
                    category = Mapper.MapObject<Category>(rdr, new CategoryMapper());
                }
            }

            return category;
        }


                    


        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " ORDER BY Name,[Group]";
                var cmd = new SqlCeCommand(sql, conn);

                using (var rdr = cmd.ExecuteReader())
                {
                    categories = Mapper.MapList<Category>(rdr, new CategoryMapper());
                }
            }

            return categories;
        }



        public List<Category> GetByType(CategoryType categoryType)
        {
            List<Category> categories = new List<Category>();

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM " + tableName + " WHERE Type='" + categoryType.ToString() + "'";
                var cmd = new SqlCeCommand(sql, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    categories = Mapper.MapList<Category>(rdr, new CategoryMapper());
                }
            }

            return categories;
        }



        public void Save(Category category)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO " + tableName + " (ID,Name,Type,[Group],IsBudgeted,Budget) "
                               + "VALUES (@ID,@Name,@Type,@Group,@IsBudgeted,@Budget)";
 
                    var cmd = new SqlCeCommand(sql, conn);

                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = category.Type;
                    cmd.Parameters.Add("@Group", SqlDbType.NVarChar).Value = category.Group;
                    cmd.Parameters.Add("@IsBudgeted", SqlDbType.Bit).Value = category.IsBudgeted;
                    cmd.Parameters.Add("@Budget", SqlDbType.Decimal).Value = category.Budget;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Update(Category category)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "UPDATE " + tableName + " SET Name=@Name,Type=@Type,[Group]=@Group,"
                               + "IsBudgeted=@IsBudgeted,Budget=@Budget WHERE ID=@ID";

                    var cmd = new SqlCeCommand(sql, conn);

                    cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = category.ID;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = category.Type;
                    cmd.Parameters.Add("@Group", SqlDbType.NVarChar).Value = category.Group;
                    cmd.Parameters.Add("@IsBudgeted", SqlDbType.Bit).Value = category.IsBudgeted;
                    cmd.Parameters.Add("@Budget", SqlDbType.Decimal).Value = category.Budget;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void UpdateByName(Category category)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql1 = "UPDATE " + tableName + " SET Name=@Name,Type=@Type,[Group]=@Group,"
                               + "IsBudgeted=@IsBudgeted,Budget=@Budget WHERE ID=@ID";

                    var cmd1 = new SqlCeCommand(sql1, conn);

                    cmd1.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = category.ID;
                    cmd1.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;
                    cmd1.Parameters.Add("@Type", SqlDbType.NVarChar).Value = category.Type;
                    cmd1.Parameters.Add("@Group", SqlDbType.NVarChar).Value = category.Group;
                    cmd1.Parameters.Add("@IsBudgeted", SqlDbType.Bit).Value = category.IsBudgeted;
                    cmd1.Parameters.Add("@Budget", SqlDbType.Decimal).Value = category.Budget;

                    cmd1.ExecuteNonQuery();
                
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Delete(Guid id)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM " + tableName + " WHERE ID='" + id + "'";
                    var cmd = new SqlCeCommand(sql, conn);
                    cmd.ExecuteNonQuery();       
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Delete(string categoryName)
        {
            try
            {
                using (var conn = new SqlCeConnection(Store.ConnectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM " + tableName + " WHERE Name='" + categoryName + "'";
                    var cmd = new SqlCeCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool IsCategoryUsed(Guid id)
        {
            bool isUsed = false;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();
                string sql = "SELECT COUNT(ID) FROM Transactions WHERE ID='" + id + "'";

                var cmd = new SqlCeCommand(sql, conn);
                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    isUsed = true;
                }
                else
                {
                    isUsed = false;
                }
            }

            return isUsed;
        }


        public bool IsCategoryUsed(string name)
        {
            bool isUsed = false;

            using (var conn = new SqlCeConnection(Store.ConnectionString))
            {
                conn.Open();

                string sql = "SELECT COUNT(t.ID) FROM Transactions t "
                        + "INNER JOIN Categories c ON t.CategoryId=c.ID "
                        + "WHERE c.Name='" + name + "'";

                var cmd = new SqlCeCommand(sql, conn);
                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    isUsed = true;
                }
                else
                {
                    isUsed = false;
                }
            }

            return isUsed;
        }



    }
}
