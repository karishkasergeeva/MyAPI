using KarinaAPI.Models;
using Npgsql;

namespace KarinaAPI.Data
{
    public class Database
    {
        NpgsqlConnection connection = new NpgsqlConnection(Constants.Connect);

        public async Task InsertHolidaysAsync(List<Holiday> holidays, string countryCode)
        {

            var sql = "insert into public.\"Holidays\"(\"date\", \"localName\", \"name\", \"countryCode\", \"Time\")"
                + $"values (@date,@localName,@name,@countryCode,@Time)";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);

            foreach (var result in holidays)
            {
                command.Parameters.AddWithValue("date", result.date);
                command.Parameters.AddWithValue("localName", result.localName);
                command.Parameters.AddWithValue("name", result.name);
                command.Parameters.AddWithValue("countryCode", result.countryCode);


            }

            command.Parameters.AddWithValue("Time", DateTime.Now);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();

        }
        public async Task InsertUserEventsAsync(UserEvents userEvents, string date, string name, string notes)
        {

            var sql = "insert into public.\"UserEvents\"(\"Date\", \"Name\", \"Notes\", \"Time\")"
                + $"values (@Date,@Name,@Notes,@Time)";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);


            command.Parameters.AddWithValue("Date", date);
            command.Parameters.AddWithValue("Name", name);
            command.Parameters.AddWithValue("Notes", notes);
            command.Parameters.AddWithValue("Time", DateTime.Now);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();

        }
        public async Task UpdateUserEventAsync(UserEventDB userEventDB)
        {

            await connection.OpenAsync();

            var sql = "update public.\"UserEvents\" set \"Date\" = @Date, \"Notes\" = @Notes where \"Name\" = @Name";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("Date", userEventDB.Date);
            command.Parameters.AddWithValue("Name", userEventDB.Name);
            command.Parameters.AddWithValue("Notes", userEventDB.Notes);

            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }
        public async Task DeleteUserEventAsync(string name)
        {
            await connection.OpenAsync();

            var sql = "delete from public.\"UserEvents\" where \"Name\" = @Name";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("Name", name);
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

    }
}
