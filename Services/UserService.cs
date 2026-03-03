using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapibvh2.Services.Context;
using blogapijlmv2.Models.DTO;

namespace blogapijlmv2.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        internal bool AddUser(CreateAccountDTO userToAdd)
        {
            throw new NotImplementedException();
        }
    }
}