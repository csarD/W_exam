using CustomersApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomersApi.Repositories
{
    public class CustomerDatabaseContext: DbContext
    {
        public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options):base (options) { }
        DbSet<DataEntity> datos { get; set; }
        DbSet<AdminEntity> admin { get; set; }

        public async Task<AdminEntity?> GetAdmin(AdminDto adminDto)
        {
            return await admin.FirstOrDefaultAsync(x => x.Email== adminDto.Email && x.Password==adminDto.Password);
        }
        public async Task<DataEntity> Get(long id)
        {
            return await datos.FirstAsync(x => x.Id == id);
        }
     
        public async Task<DataEntity> Add(CreateDataDto customerDto)
        {
            DataEntity entity = new DataEntity()
            {
                Id=null,
               Data=customerDto.Data,
               
            };
            EntityEntry<DataEntity> response= await datos.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se guardo la infomracion"));
        }
    }
    public class DataEntity
    {
        public long? Id { get; set; }
        public int[]? Data { get; set; }
     

      

    }
    public class AdminEntity
    {

        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        

    }
}
