using IMS_Example.Data.Contexts;
using IMS_Example.Data.Models;

namespace IMS_Example.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext _context;
        private GenericRepository<Devices>? devicesRepository;
        private GenericRepository<Users>? userRepository;
        private GenericRepository<Permission_Use_Menu>? permission_use_menuRepository;
        private GenericRepository<Permission_Group>? permission_groupRepository;
        private bool disposed = false;

        public GenericRepository<Devices> DevicesRepository
        {
            get
            {
                if (devicesRepository == null)
                {
                    devicesRepository = new GenericRepository<Devices>(_context);
                }
                return devicesRepository;
            }
        }

        public GenericRepository<Users> UsersRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new GenericRepository<Users>(_context);
                }
                return userRepository;
            }
        }

        public GenericRepository<Permission_Group> PermissionGroupRepository
        {
            get
            {
                if (permission_groupRepository == null)
                {
                    permission_groupRepository = new GenericRepository<Permission_Group>(_context);
                }
                return permission_groupRepository;
            }
        }

        public GenericRepository<Permission_Use_Menu> PermissionUseMenuRepository
        {
            get
            {
                if (permission_use_menuRepository == null)
                {
                    permission_use_menuRepository = new GenericRepository<Permission_Use_Menu>(_context);
                }
                return permission_use_menuRepository;
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
