using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using HomeApi.Configuration;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Имя комнаты</param>
        /// <param name="request"></param>
        /// <returns></returns>
        
        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> EditRoom([FromRoute] string name, [FromBody] EditRoomRequest request)
        {
            var room = await _repository.GetRoomByName(name);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {room.Name} не найдена!");
            await _repository.UpdateRoom(room, new Data.Queries.UpdateRoomQuery(request.NewName, request.NewArea, request.NewGasConnected, request.NewVoltage));
            return StatusCode(201, $"Комната {room.Name} обновлена!");
        }
            /// <summary>
            /// Добавление комнаты
            /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }
    }
}