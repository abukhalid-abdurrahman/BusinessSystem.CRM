using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.BusinessObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.User
{
    public class UserContext : DataBaseContext, IEntityRepository<UserEntityModel>, IRemovable<RemovePartnerRequestModel>
    {
        public async Task<int> CreateAsync(UserEntityModel userEntity)
        {
            userEntity.InsertDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var query = $@"INSERT INTO public.users" + 
                            "(role_id, image_id, description_id, login, password, username, removed, insertdate, phonenumber, email)" + 
                            "VALUES (@RoleId, @ImageId, @DescriptionId, @Login, @Password, @UserName, @Removed, @InsertDate, @PhoneNumber, @Email)" +
                            " RETURNING id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return 0;
            var userId = await connection.ExecuteScalarAsync<int?>(query, userEntity);
            await connection.CloseAsync();
            return userId ?? 0;
        }

        public async Task DeleteAsync(int id)
        {
            var query = $@"DELETE FROM public.users
                              WHERE id = @id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { id });
            await connection.CloseAsync();
        }
        
        public async Task RemoveAsync(int id)
        {
            var query = $@"DELETE FROM public.users
                              WHERE id = @id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { id });
            await connection.CloseAsync();
        }

        public async Task<List<UserEntityModel>> GetAllAsync()
        {
            var query = $@"SELECT id AS Id,
                                  role_id AS RoleId, 
                                  image_id AS ImageId, 
                                  description_id AS DescriptionId, 
                                  login AS Login, 
                                  email AS Email,
                                  phonenumber AS PhoneNumber,
                                  password AS Password, 
                                  username AS UserName, 
                                  removed AS Removed, 
                                  insertdate AS InsertDate 
                            FROM public.users";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<UserEntityModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }

        public async Task<UserEntityModel> GetAsync(int id)
        {
            var query = $@"SELECT id AS Id,
                                  role_id AS RoleId, 
                                  image_id AS ImageId, 
                                  description_id AS DescriptionId, 
                                  login AS Login, 
                                  email AS Email,
                                  phonenumber AS PhoneNumber,
                                  password AS Password, 
                                  username AS UserName, 
                                  removed AS Removed, 
                                  insertdate AS InsertDate 
                              FROM public.users
                              WHERE id = @id";


            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var userEntity = await connection.QueryFirstOrDefaultAsync<UserEntityModel>(query, new { id });
            await connection.CloseAsync();
            return userEntity;
        }

        public async Task UpdateAsync(UserEntityModel entityModel)
        {
            var query = $@"UPDATE public.users
	                          SET role_id=@RoleId, 
                                  image_id=@ImageId, 
                                  description_id=@DescriptionId, 
                                  login=@Login, 
                                  email=@Email,
                                  phonenumber=@PhoneNumber,
                                  password=@Password, 
                                  username=@UserName, 
                                  removed=@Removed, 
                                  insertdate=@InsertDate
	                          WHERE id = @Id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, entityModel);
            await connection.CloseAsync();
        }

        public async Task<UserEntityModel> GetUserAsync(string login, string password)
        {
            var query = $@"SELECT id AS Id,
                                  role_id AS RoleId, 
                                  image_id AS ImageId, 
                                  description_id AS DescriptionId, 
                                  login AS Login, 
                                  email AS Email,
                                  phonenumber AS PhoneNumber,
                                  password AS Password, 
                                  username AS UserName, 
                                  removed AS Removed, 
                                  insertdate AS InsertDate 
                              FROM public.users
                              WHERE login=@login AND password=@password AND removed=false";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var user = await connection.QueryFirstOrDefaultAsync<UserEntityModel>(query, new { login, password });
            await connection.CloseAsync();
            return user;
        }

        public async Task RemoveAsync(RemovePartnerRequestModel model)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));
            
            var query = $@"UPDATE public.users
	                          SET removed=@Status
	                          WHERE id=@Id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, model);
            await connection.CloseAsync();
        }
    }
}