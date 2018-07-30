using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLP.Discovery.ImagePackager.Core.DataAccess
{
    public interface IStudentRepositoryService
    {
        List<StudentInfo> LoadStudentPhotos();
    }

    public class StudentRepositoryService : IStudentRepositoryService
    {
        private readonly IConnectionSource _connectionSource;

        public StudentRepositoryService(IConnectionSource connectionSource)
        {
            _connectionSource = connectionSource;
        }

        public List<StudentInfo> LoadStudentPhotos()
        {
            List<StudentInfo> students = new List<StudentInfo>();

            using (SqlConnection connection = new SqlConnection(_connectionSource.GetConnectionString(Constants.Discovery)))
            {
                SqlCommand command = new SqlCommand(SqlStatements.GET_STUDENTUSI_PROFILE_THUMBNAIL, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        students.Add(new StudentInfo()
                        {
                            StudentUSI = System.Convert.ToInt32(reader["StudentUSI"]),
                            FileName = reader["ProfileThumbnail"].ToString()
                        });
                    }
                }
                reader.Close();
            }

            return students;
        }
    }
}
