using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuggestionBoxApi.Data;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Interfaces;
using SuggestionBoxApi.Models;

namespace SuggestionBoxApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SuggboxContext _context;
        private readonly IMapper _mapper;

        public UserRepository(SuggboxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddUserAsync(UserDto userDto)
        {
            var userToAdd = _mapper.Map<User>(userDto);
            await _context.Users.AddAsync(userToAdd);
        }

        public async Task DeleteUserAsync(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if(userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync($"{id}");
            return _mapper.Map<UserDto>(user);
            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Update(user);
        }
    }
}
