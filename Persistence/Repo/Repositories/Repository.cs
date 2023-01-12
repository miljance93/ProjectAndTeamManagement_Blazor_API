using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Persistence.Repo.Interfaces;
using System.Linq.Expressions;

namespace Persistence.Repo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync<TInput>(TInput input) where TInput : class
        {
            bool result;

            try
            {
                _context.Set<T>().Add(_mapper.Map<T>(input));
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception)
            {

                result = false;
            }

            return result;
        }

        public async Task<IEnumerable<TOutput>> GetAllAsync<TOutput>()
        {
            return await _context.Set<T>()
                .ProjectTo<TOutput>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<TOutput> GetByIdAsync<TOutput>(Expression<Func<TOutput, bool>> expression) where TOutput : class
        {
            var getById = await _context.Set<T>()
                .ProjectTo<TOutput>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(expression);

            return getById;
        }

        public async Task<bool> UpdateAsync<TInput>(TInput input) where TInput : class
        {
            bool result;

            try
            {
                _context.Set<T>().Update(_mapper.Map<T>(input));
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception)
            {

                result = false;
            }

            return result;
        }
    }
}
