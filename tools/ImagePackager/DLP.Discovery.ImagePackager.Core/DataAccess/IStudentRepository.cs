using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLP.Discovery.ImagePackager.Core.CommandLine;

namespace DLP.Discovery.ImagePackager.Core.DataAccess
{
    public interface IStudentRepositoryService
    {
        List<StudentInfo> LoadStudentPhotoInformation(Options options);
    }

    public class StudentInputFileService : IStudentRepositoryService
    {
        private StudentInfo Parse(string inputLine)
        {
            var values = inputLine.Split(',');
            return new StudentInfo()
            {
                StudentUSI =  System.Convert.ToInt32(values[0]),                                    
                FileName = values[1],
                SchoolName =  values[2],
                SchoolId = System.Convert.ToInt32(values[3])
            };
        }

        public List<StudentInfo> LoadStudentPhotoInformation(Options options)
        {
            var info = new List<StudentInfo>();
            if (string.IsNullOrEmpty(options.InputSourceFile))
            {
                throw new Exception("input file was not indicated");
            }

            if (!System.IO.File.Exists(options.InputSourceFile))
            { 
                throw new Exception($"Indicated file [{options.InputSourceFile}] is not present.");
            }

            using (StreamReader sr = new StreamReader(options.InputSourceFile))
            {
                string currentLine;
                while (!string.IsNullOrEmpty(currentLine = sr.ReadLine()))
                { 
                    info.Add(this.Parse(currentLine));
                }
            }

            return info;
        }
    }

    public class StudentRepositoryService : IStudentRepositoryService
    {
        private readonly IConnectionSource _connectionSource;

        public StudentRepositoryService(IConnectionSource connectionSource)
        {
            _connectionSource = connectionSource;
        }

        public List<StudentInfo> LoadStudentPhotoInformation(Options options)
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
