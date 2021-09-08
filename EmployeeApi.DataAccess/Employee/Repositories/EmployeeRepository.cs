using Dapper;
using EmployeeApi.DataAccess.Connection;
using EmployeeApi.DataAccess.Employee.Interfaces;
using EmployeeApi.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.DataAccess.Employee.Repositories
{
    public  class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(IConnectionFactory connectionFactory) : base(connectionFactory) { }
        public Task<EmployeeModel> FindEmployeeByRfc(string Rfc)
        {
                return WithConnection(async connection =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Rfc", Rfc, DbType.String, ParameterDirection.Input, 100);
                    var user = await connection.QueryAsync<EmployeeModel>(sql: "[dbo].[spFindEmployeeByRfc]", param: parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    return user.SingleOrDefault();
                });
        }

        public  async Task<List<EmployeeModel>> GetEmployees()
        {
            return await  WithConnection(async q =>
            {
                var result = await q.QueryAsync<EmployeeModel>(sql: "select * from [dbo].[Employee] order by BornDate asc", commandType: CommandType.Text).ConfigureAwait(false);
                return result.ToList();
            });
        }

        public async Task<int> SetEmployee(EmployeeModel employee)
        {
            try
            {
                return await WithConnection(connection =>
                {

                    var parameters = new DynamicParameters();

                    parameters.Add("@Name", employee.Name, DbType.String, ParameterDirection.Input);
                    parameters.Add("@LastName", employee.LastName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@Rfc", employee.Rfc, DbType.String, ParameterDirection.Input, 15);
                    parameters.Add("@BornDate", employee.BornDate, DbType.Date, ParameterDirection.Input);
                    parameters.Add("@Status", employee.Status, DbType.Int32, ParameterDirection.Input);

                    return connection.ExecuteAsync(sql: "[dbo].[spInsertEmployee]", param: parameters, commandType: CommandType.StoredProcedure);
                }).ConfigureAwait(false);
            }
            catch (Exception e)
            {

                return 0;           
            }

        }
    }
}
