using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SecuringRestApiAspNetCore.DatabaseContext;
using SecuringRestApiAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecuringRestApiAspNetCore.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelApiDbContext _context;
        //private readonly IMapper _mapper;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DefaultRoomService(
                    HotelApiDbContext context,
                    //IMapper mapper,
                    IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            //_mapper = mapper;
            _mappingConfiguration = mappingConfiguration;
        }


        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(z => z.Id == id);

            if (entity == null)
            {
                return null;
            }

            /*return new Room
            {
                Href = null, //Url.Link(nameof(GetRoomById), new { roomId = entity.Id }),
                Name = entity.Name,
                Rate = entity.Rate / 100.0m
            };*/

            // Благодаря AutoMApper, код выше заменяется на код внизу
            // Тип Room маппится с типом RoomEntity, как прописано в MappingProfile
            //return _mapper.Map<Room>(entity);
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<Room>(entity);
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var query = _context.Rooms.ProjectTo<Room>(_mappingConfiguration);
            return await query.ToArrayAsync();
        }
    }
}
