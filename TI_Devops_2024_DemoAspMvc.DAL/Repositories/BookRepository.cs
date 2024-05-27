using System.Data;
using System.Data.SqlClient;
using TI_Devops_2024_DemoAspMvc.DAL.Interfaces;
using TI_Devops_2024_DemoAspMvc.Domain.Entities;

namespace TI_Devops_2024_DemoAspMvc.DAL.Repositories
{
    public class BookRepository : BaseRepository<Book, string>, IBookRepository
    {
        public BookRepository() : base("Book", "ISBN")
        {
        }

        protected override Book Convert(IDataRecord r)
        {
            return new Book()
            {
                ISBN = (string)r["ISBN"],
                Title = (string)r["Title"],
                Description = r["Description"] == DBNull.Value ? null : (string)r["Description"],
                PublishDate = (DateTime)r["Publish_date"],
                AuthorId = (int)r["Author_id"]
            };
        }

        private Book ConvertFull(IDataRecord r)
        {
            Book b = Convert(r);
            b.Author = new Author()
            {
                Id = (int)r["Author_id"],
                Firstname = (string)r["Firstname"],
                Lastname = (string)r["Lastname"],
                PenName = r["Pen_Name"] == DBNull.Value ? null : (string)r["Pen_Name"],
                Birthdate = (DateTime)r["Birthdate"]
            };
            return b;
        }

        public override string Create(Book b)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"INSERT INTO Book 
                                 OUTPUT INSERTED.ISBN
                                 VALUES(@isbn,@title,@description,@publishDate,@authorId)";

            cmd.Parameters.AddWithValue("@isbn", b.ISBN);
            cmd.Parameters.AddWithValue("@title", b.Title);
            cmd.Parameters.AddWithValue("@description", b.Description == null ? DBNull.Value : b.Description);
            cmd.Parameters.AddWithValue("@publishDate", b.PublishDate);
            cmd.Parameters.AddWithValue(@"authorId", b.AuthorId);

            conn.Open();

            string isbn = (string)cmd.ExecuteScalar();

            conn.Close();

            return isbn;
        }

        public override bool Update(string isbn, Book b)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"UPDATE Book 
                                 SET Title = @title 
                                     Description = @description 
                                     Publish_date = @publishDate 
                                     Author_id = @authorId 
                                 WHERE ISBN like @isbn";

            cmd.Parameters.AddWithValue("@title", b.Title);
            cmd.Parameters.AddWithValue("@description", b.Description == null ? DBNull.Value : b.Description);
            cmd.Parameters.AddWithValue("@publishDate", b.PublishDate);
            cmd.Parameters.AddWithValue(@"authorId", b.AuthorId);
            cmd.Parameters.AddWithValue("@isbn", isbn);

            conn.Open();

            int nbRows = cmd.ExecuteNonQuery();

            conn.Close();

            return nbRows == 1;
        }

        public Book? GetFullByISBN(string ISBN)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"SELECT *
                                 FROM Book b JOIN Author a 
                                    ON b.Author_id = a.Id 
                                 WHERE b.ISBN = @isbn ";

            cmd.Parameters.AddWithValue("@isbn", ISBN);

            conn.Open();

            IDataReader r = cmd.ExecuteReader();

            Book? b = null;

            if (r.Read())
            {
                b = ConvertFull(r);
            }

            conn.Close();

            return b;
        }

        public bool ExistByUnicityCriteria(Book book)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*) 
                                 FROM Book 
                                 WHERE Title = @title AND 
                                       Publish_date = @publishDate AND 
                                       Author_Id = @authorId ";

            cmd.Parameters.AddWithValue("@title", book.Title);
            cmd.Parameters.AddWithValue("@publishDate", book.PublishDate);
            cmd.Parameters.AddWithValue("@authorId", book.AuthorId);

            conn.Open();

            int count = (int)cmd.ExecuteScalar();

            conn.Close();

            return count > 0;
        }
    }
}
