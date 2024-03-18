using AutoMapper;
using DataAcces;
using DataAcces.BusinessExeption;
using DataAcces.Model;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.UserService
{
    public class UserService : IUserService
    {
        #region Private properties        
        private readonly IMapper _mapper;
        private readonly ContextDb context;
        #endregion

        #region Constructor
        public UserService(IMapper mapper, ContextDb _context)
        {
            _mapper = mapper;
            context = _context;
        }
        #endregion        

        #region Methods
        public async Task<List<UserDto>> GetAllAsync()
        {
            try
            {
                List<User> userList = await context.User.ToListAsync();

                //Solucion Paginado 
                //var pageNumber = 1; debe venir por parametro
                //var pageSize = 10; debe venir por parametro
                // var listItems = context.User
                //.Skip((pageNumber - 1) * pageSize)
                //.Take(pageSize)
                //.ToList();

                return _mapper.Map<List<User>, List<UserDto>>(userList);

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw new Exception(err);
            }
        }

        public async Task<UserDto> GetUserID(Guid vUserID)
        {
            try
            {
                var user = _mapper.Map<User, UserDto>(await context.User.Where(x => x.UserId == vUserID).FirstOrDefaultAsync());
                return user;

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw new Exception(err);
            }
        }

        public async Task<object> AddOrUpdateAsync(UserDto data)
        {
            validatorIngreso(data);
            try
            {
                User user = _mapper.Map<User>(data);                

                if (user.UserId != Guid.Empty)
                {
                    if (!Exist(data.UserId))
                    {
                        return new
                        {
                            data = false,
                            status = "error",
                            msg = "Usuario no encontrado"
                        };
                    }
                    var query = _mapper.Map<User, UserDto>(await context.User.Where(x => x.UserId == user.UserId).FirstOrDefaultAsync());

                    user.CreatedOn = query.CreatedOn;
                    user.ModifiedOn = DateTime.Now;
                    context.Update(user);
                }
                else
                {
                    context.Add(user);
                }

                await context.SaveChangesAsync();

                return new
                {
                    data = true,
                    status = "success",
                    msg = "success_save"
                };
            }
            catch (Exception)
            {
                return new
                {
                    data = false,
                    status = "error",
                    msg = "Se genero un error favor revisar los datos y volver a intentar."
                };
                throw;
            }
            
        }

        public async Task<bool> DeleteUserAsync(Guid vUserID)
        {
            var user = context.User.Where(x => x.UserId == vUserID).FirstOrDefault();

            if (user != null)
            {
                context.User.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #endregion

        #region Private Methods
        private bool Exist(Guid UserId)
        {
            var result = context.User.Where(x => x.UserId == UserId).Any();
            return result;
        }

        private void validatorIngreso(UserDto user)
        {
            if (!Regex.IsMatch(user.FirstName, @"^[a-zA-Z]+$"))
            {
                throw new BusinessExeption("El Primer Nombre no bebe tener numeros.");
            }
            if (!Regex.IsMatch(user.SecondName, @"^[a-zA-Z]+$"))
            {
                throw new BusinessExeption("El Segundo Nombre no bebe tener numeros.");
            }
            if (!Regex.IsMatch(user.Surname, @"^[a-zA-Z]+$"))
            {
                throw new BusinessExeption("El Primer Apellido no bebe tener numeros.");
            }
            if (!Regex.IsMatch(user.SecondSurname, @"^[a-zA-Z]+$"))
            {
                throw new BusinessExeption("El Segundo Apellido no bebe tener numeros.");
            }
            if (Regex.IsMatch(user.Salary.ToString(), @"^[a-zA-Z]+$"))
            {
                throw new BusinessExeption("El salario debe tener solo numeros.");
            }
            if (user.Salary <= 0)
            {
                throw new BusinessExeption("El salario debe ser mayor a 0.");
            }
        }
        #endregion

    }
}
